using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Astre_manager : MonoBehaviour
{
    public Astre_affichage astre_affichage;
    public Astre_description astre_description;
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
            //TODO succes fin du jeu
            print("SUCCESS");
        }
    }

    public void DefinirAstreActuel(int id)
    {
        if (astre_actuel != id)
        {
            liste_astres[astre_actuel].Deselectionner();
            astre_actuel = id;
            AfficherAstreActuel();
        }
        DecrireAstreActuel();
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


//void Update()
//{
//    // TODO retirer les touches test
//    if (Input.GetKeyDown(KeyCode.Keypad0))
//    {
//        GenererAstres(3, 0);
//        if(astre_affichage != null)
//                astre_affichage.AfficherAstre(liste_astres[astre_actuel]);
//        Debug.Log(astre_actuel);
//    }
//    if (Input.GetKeyDown(KeyCode.LeftArrow))
//    {
//        astre_actuel = Mathf.Max(0, astre_actuel - 1);
//        if(astre_affichage != null)
//                astre_affichage.AfficherAstre(liste_astres[astre_actuel]);
//        Debug.Log(astre_actuel);
//    }
//    if (Input.GetKeyDown(KeyCode.RightArrow))
//    {
//        astre_actuel = Mathf.Min(liste_astres.Count-1, astre_actuel + 1);
//        if(astre_affichage != null)
//            astre_affichage.AfficherAstre(liste_astres[astre_actuel]);
//        Debug.Log(astre_actuel);
//    }
//}

//public void GenererAstres(int quantite, int index_actuel)
//{
//    // Reset
//    foreach(Astre astre in liste_astres)
//        Destroy(astre);
//    liste_astres = new List<Astre>();
//    // Creation
//    for (int i = 0; i < quantite; i++)
//        liste_astres.Add(this.gameObject.AddComponent(typeof(Astre)) as Astre);
//    // Affichage
//    astre_actuel = index_actuel;
//    if(astre_affichage != null)
//        astre_affichage.AfficherAstre(liste_astres[astre_actuel]);
//    // UI
//    foreach (Transform child in transform)
//        print("Foreach loop: " + child);
//    //Instantiate
//}