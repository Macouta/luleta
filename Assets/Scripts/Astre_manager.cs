using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using TMPro;

public class Astre_manager : MonoBehaviour
{
    public Astre_affichage astre_affichage;
    public Astre_description astre_description;

    public UnityEvent onWin;

    private List<Astre> liste_astres = new List<Astre>();
    private int astre_actuel = 0;
    private int astres_decouverts = 0;

    void Start()
    {
        GetAllChildrenAstres();
    }

    private void GetAllChildrenAstres()
    {
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            liste_astres.Add(transform.GetChild(i).GetComponent<Astre>());
            liste_astres[i].id = i;
        }
    }

    public Astre getAstreCourant() {
        return liste_astres[astre_actuel];
    }

    public void DecouvrirAstre()
    {
        astres_decouverts += 1;
        if (astres_decouverts >= liste_astres.Count)
        {
            onWin.Invoke();
        }
    }

    public void DefinirAstreActuel(int id)
    {

        if (id == -1)//il n'y a pas d'astre actuel
        {
            EteindreAstreActuel();
        }
        else if (astre_actuel != id)
        {
            astre_actuel = id;
            AfficherAstreActuel();
            DecrireAstreActuel();
        }
        else
        {
            DecrireAstreActuel();
        }
    }

    private void EteindreAstreActuel()
    {
        astre_affichage.EteindreAffichage();
    }
    private void AfficherAstreActuel()
    {
        astre_affichage.AfficherAstre(liste_astres[astre_actuel]);
    }

    private void DecrireAstreActuel()
    {
        float pourcentage = ((float)astres_decouverts / (float)liste_astres.Count) * 100f;
        astre_description.DecrireAstre(liste_astres[astre_actuel], pourcentage);
    }
}