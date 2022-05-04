using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

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

    private float comestible, argent, degats, energie = 1f;

    private Astre current;
    // Start is called before the first frame update
    void Start()
    {
        // updateAstreCourant();
    }

    public void updateAstreCourant() {
        current = player.Astre_actuel;
        resetLedColor(current);
        Debug.Log(current.type);
        
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

    private void updateBar(ResourceBar up, ResourceBar down) {

        up.value += 0.1f;
        up.value = Mathf.Clamp(up.value, 0, 1);
        down.value -= 0.1f;
        down.value = Mathf.Clamp(down .value, 0, 1);
        up.bar.UpdateBar01(up.value);
        down.bar.UpdateBar01(down.value);
    }

    public void trade() {
        Debug.Log(current.type);
        switch(current.type) {
            case "Planète tellurique":
                updateBar(argent_bar, comestible_bar);
                break;
            case "Planète gazeuse":
                updateBar(comestible_bar, energie_bar);
                break;
            case "Satellite":
            case "Comète":
                updateBar(degats_bar, argent_bar);
                break;
            case "Étoile":
                updateBar(energie_bar, degats_bar);
                break;
        }
    }

    public void onInvadeEnd() {

    }
}
