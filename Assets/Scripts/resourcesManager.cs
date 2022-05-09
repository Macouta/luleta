using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;



public enum ResourceType {
    Comestible, Argent, Degats, Energie //0, 1, 2, 3
}

public class resourcesManager : MonoBehaviour
{
    public ResourceBar comestible_bar;
    public ResourceBar argent_bar;
    public ResourceBar degats_bar;
    public ResourceBar energie_bar;
    public map_joueur player;

    [BoxGroup("Resources lose over jump")]
    [Range(0f, 0.5f)]
    public float comestiblePerte, argentPerte, degatsPerte, energiePerte = 0.1f;

    public Color defaultLedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    public Color upColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    public Color downColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    [Space]
    public UnityEvent onRessourceNone;
    public UnityEvent onRessourceDanger;
    public UnityEvent onRessourceSafe;

    private Dictionary<ResourceType, ResourceBar> resources = new Dictionary<ResourceType, ResourceBar>();

    private Astre current;

    private bool tradeDone = false;
    // Start is called before the first frame update
    void Start()
    {
        resources.Add(energie_bar.type, energie_bar);
        resources.Add(degats_bar.type, degats_bar);
        resources.Add(argent_bar.type, argent_bar);
        resources.Add(comestible_bar.type, comestible_bar);
        // updateAstreCourant();
    }

    public void updateAstreCourant() {
        current = player.Astre_actuel;
        resetLedColor(current);
        tradeDone = false;
        
    }

    public bool isTradeDone() {
        return tradeDone;
    } 

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }

    private void ledColor(ResourceBar a, ResourceBar b) {
        float scaleUp = 0;
        Vector3 scale = a.led.rectTransform.localScale;

        scaleUp = map(Mathf.Abs(a.setNextValue(-1f)), 0f, 0.5f, 0.5f, 1.5f);
        a.led.color = downColor;
        a.led.rectTransform.localScale = new Vector3(scaleUp, scaleUp, scaleUp);

        scaleUp = map(b.setNextValue(1f), 0f, 0.5f, 0.5f, 1.5f);
        b.led.color = upColor;
        b.led.rectTransform.localScale = new Vector3(scaleUp, scaleUp, scaleUp);
    }
    public void resetLedColor(Astre a) {
        comestible_bar.led.transform.Find("bar").gameObject.SetActive(false);
        argent_bar.led.transform.Find("bar").gameObject.SetActive(false);
        degats_bar.led.transform.Find("bar").gameObject.SetActive(false);
        energie_bar.led.transform.Find("bar").gameObject.SetActive(false);
        Debug.Log(a.type);
        
        switch(a.type) {
            case "Planete tellurique":
                ledColor(comestible_bar, argent_bar);
                // comestible_bar.led.color = downColor;
                // comestible_bar.setNextValue(-1f);
                // argent_bar.led.color = upColor;
                // argent_bar.setNextValue(1f);
                break;
            case "Planete gazeuse":
                ledColor(energie_bar, comestible_bar);
                // energie_bar.led.color = downColor;
                // energie_bar.setNextValue(-1f);
                // comestible_bar.led.color = upColor;
                // comestible_bar.setNextValue(1f);
                break;
            case "Satellite":
            case "Comete":
                ledColor(argent_bar, degats_bar);
                // argent_bar.led.color = downColor;
                // argent_bar.setNextValue(-1f);
                // degats_bar.led.color = upColor;
                // degats_bar.setNextValue(1f);
                break;
            case "Etoile":
                ledColor(degats_bar, energie_bar);
                // degats_bar.led.color = downColor;
                // degats_bar.setNextValue(-1f);
                // energie_bar.led.color = upColor;
                // energie_bar.setNextValue(1f);
                break;
            default:
                break;
        }
    }

    public void onJump() {
        energie_bar.updateBar(-energiePerte);
        comestible_bar.updateBar(-comestiblePerte);
        degats_bar.updateBar(-Random.Range(0f ,degatsPerte));
        argent_bar.updateBar(-argentPerte);

        comestible_bar.led.color = defaultLedColor;
        argent_bar.led.color = defaultLedColor;
        degats_bar.led.color = defaultLedColor;
        energie_bar.led.color = defaultLedColor;

        comestible_bar.led.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        argent_bar.led.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        degats_bar.led.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        energie_bar.led.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void trade() {
        switch(current.type) {
            case "Planete tellurique":
                argent_bar.updateBarRng();
                comestible_bar.updateBarRng();
                checkStatus(new List<ResourceBar>() { argent_bar, comestible_bar });
                break;
            case "Planete gazeuse":
                comestible_bar.updateBarRng();
                energie_bar.updateBarRng();
                checkStatus(new List<ResourceBar>() { comestible_bar, energie_bar });
                break;
            case "Satellite":
            case "Comete":
                degats_bar.updateBarRng();
                argent_bar.updateBarRng();
                checkStatus(new List<ResourceBar>() { degats_bar, argent_bar });
                break;
            case "Etoile":
                energie_bar.updateBarRng();
                degats_bar.updateBarRng();
                checkStatus(new List<ResourceBar>() { energie_bar, degats_bar });
                break;
        }


        comestible_bar.led.transform.Find("bar").gameObject.SetActive(true);
        argent_bar.led.transform.Find("bar").gameObject.SetActive(true);
        degats_bar.led.transform.Find("bar").gameObject.SetActive(true);
        energie_bar.led.transform.Find("bar").gameObject.SetActive(true);
        comestible_bar.led.color = defaultLedColor;
        argent_bar.led.color = defaultLedColor;
        degats_bar.led.color = defaultLedColor;
        energie_bar.led.color = defaultLedColor;
        tradeDone = true;
    }

    private void checkStatus(List<ResourceBar> resourcesToCheck)
    {
        // get lowest status 
        Status lowestStatus = Status.High;
        for(int i = 0; i < resourcesToCheck.Count; i++){
            if (resourcesToCheck[i].status < lowestStatus)
                lowestStatus = resourcesToCheck[i].status;
        }
        // invoke events
        if (lowestStatus == Status.None)
            onRessourceNone.Invoke();
        else if (lowestStatus == Status.Low)
            onRessourceDanger.Invoke();
        else
            onRessourceSafe.Invoke();
    }

    public void onInvadeEnd(ResourceType res, float value) {
        Debug.Log(res);
        resources[res].updateBar(value);
    }
}
