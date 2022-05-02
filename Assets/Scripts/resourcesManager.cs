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
    public Astre_manager astreManager;

    private float comestible, argent, degats, energie = 1f;

    private Astre current;
    // Start is called before the first frame update
    void Start()
    {
        updateAstreCourant();
    }

    public void updateAstreCourant() {
        current = astreManager.getAstreCourant();
    }

    public void trade() {
        switch(current.type) {
            case "Plan�te tellurique":
                comestible -= 0.1f;
                argent += 0.1f;
                comestible_bar.bar.UpdateBar01(comestible);
                argent_bar.bar.UpdateBar01(argent);
                break;
            case "Plan�te gazeuse":
                energie -= 0.1f;
                comestible += 0.1f;
                energie_bar.bar.UpdateBar01(energie);
                comestible_bar.bar.UpdateBar01(comestible);
                break;
            case "Satellite":
                argent -= 0.1f;
                degats += 0.1f;
                argent_bar.bar.UpdateBar01(argent);
                degats_bar.bar.UpdateBar01(degats);
                break;
            case "Com�te":
                argent -= 0.1f;
                degats += 0.1f;
                argent_bar.bar.UpdateBar01(argent);
                degats_bar.bar.UpdateBar01(degats);
                break;
            case "�toile":
                degats += 0.1f;
                energie += 0.1f;
                degats_bar.bar.UpdateBar01(degats);
                energie_bar.bar.UpdateBar01(energie);
                break;
        }
    }
}
