using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;


public enum ResourceType {
    Comestible, Argent, Degats, Energie 
}

public class resourcesManager : MonoBehaviour
{

    public ResourceBar comestible_bar;
    public ResourceBar argent_bar;
    public ResourceBar degats_bar;
    public ResourceBar energie_bar;
    public map_joueur player;


    public Color defaultLedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    public Color upColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    public Color downColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    private Dictionary<ResourceType, ResourceBar> resources = new Dictionary<ResourceType, ResourceBar>();

    private Astre current;
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
        
    }

    public void resetLedColor(Astre a) {
        comestible_bar.led.color = defaultLedColor;
        argent_bar.led.color = defaultLedColor;
        degats_bar.led.color = defaultLedColor;
        energie_bar.led.color = defaultLedColor;

        switch(a.type) {
            case "Planète tellurique":
                comestible_bar.led.color = downColor;
                argent_bar.led.color = upColor;
                break;
            case "Planète gazeuse":
                energie_bar.led.color = downColor;
                comestible_bar.led.color = upColor;
                break;
            case "Satellite":
            case "Comète":
                argent_bar.led.color = downColor;
                degats_bar.led.color = upColor;
                break;
            case "Étoile":
                degats_bar.led.color = downColor;
                energie_bar.led.color = upColor;
                break;
            default:
                break;
        }
    }

    public void trade() {
        switch(current.type) {
            case "Planète tellurique":
                argent_bar.updateBar(0.1f);
                comestible_bar.updateBar(-0.1f);
                break;
            case "Planète gazeuse":
                comestible_bar.updateBar(0.1f);
                energie_bar.updateBar(-0.1f);
                break;
            case "Satellite":
            case "Comète":
                degats_bar.updateBar(0.1f);
                argent_bar.updateBar(-0.1f);
                break;
            case "Étoile":
                energie_bar.updateBar(0.1f);
                degats_bar.updateBar(-0.1f);
                break;
        }
    }

    public void onInvadeEnd(ResourceType res, float value) {
        Debug.Log(res);
        resources[res].updateBar(value);
    }
}
