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

    public void resetLedColor(Astre a) {
        comestible_bar.led.transform.Find("bar").gameObject.SetActive(false);
        argent_bar.led.transform.Find("bar").gameObject.SetActive(false);
        degats_bar.led.transform.Find("bar").gameObject.SetActive(false);
        energie_bar.led.transform.Find("bar").gameObject.SetActive(false);
        Debug.Log(a.type);
        switch(a.type) {
            case "Planete tellurique":
                comestible_bar.led.color = downColor;
                argent_bar.led.color = upColor;
                break;
            case "Planete gazeuse":
                energie_bar.led.color = downColor;
                comestible_bar.led.color = upColor;
                break;
            case "Satellite":
            case "Comete":
                argent_bar.led.color = downColor;
                degats_bar.led.color = upColor;
                break;
            case "Etoile":
                degats_bar.led.color = downColor;
                energie_bar.led.color = upColor;
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
    }

    public void trade() {
        switch(current.type) {
            case "Planete tellurique":
                argent_bar.updateBar(0.1f);
                comestible_bar.updateBar(-0.1f);
                checkStatus(new List<ResourceBar>() { argent_bar, comestible_bar });
                break;
            case "Planete gazeuse":
                comestible_bar.updateBar(0.1f);
                energie_bar.updateBar(-0.1f);
                checkStatus(new List<ResourceBar>() { comestible_bar, energie_bar });
                break;
            case "Satellite":
            case "Comete":
                degats_bar.updateBar(0.1f);
                argent_bar.updateBar(-0.1f);
                checkStatus(new List<ResourceBar>() { degats_bar, argent_bar });
                break;
            case "Etoile":
                energie_bar.updateBar(0.1f);
                degats_bar.updateBar(-0.1f);
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
