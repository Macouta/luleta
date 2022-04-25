using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Astre_manager : MonoBehaviour
{
    public Astre_affichage astre_affichage;
    public Astre_description astre_description;
    private Astre astre_actuel;
    //private List<Astre> liste_astres = new List<Astre>();
    //private Astre tst1;
    //private int astre_actuel=0;

    void Start()
    {
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

    public void DefinirAstreActuel(Astre astre_selectionne)
    {
        if(astre_actuel == null)
        {
            astre_actuel = astre_selectionne;
            AfficherAstreActuel();
            DecrireAstreActuel();
        }
        if (astre_actuel != astre_selectionne)
        {
            astre_actuel.Deselectionner();
            astre_actuel = astre_selectionne;
            AfficherAstreActuel();
            DecrireAstreActuel();
        }
    }
    private void AfficherAstreActuel()
    {
        astre_affichage.AfficherAstre(astre_actuel);
    }

    private void DecrireAstreActuel()
    {
        astre_description.DecrireAstre(astre_actuel);
    }
}