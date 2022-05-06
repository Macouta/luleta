using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
public class Astre_affichage : MonoBehaviour
{
    public Material mat_tellurique, mat_gazeux, mat_satellite, mat_comete, mat_etoile;
    private Renderer ren;
    private Astre astre;
    private Moniteur_MatCtrl moniteur;
    private IEnumerator coroutine;

    void Start()
    {
        moniteur = this.GetComponent<Moniteur_MatCtrl>();
        ren = this.GetComponent<Renderer>();
    }

    void Update()
    {
        if (astre != null)
        {
            this.transform.Rotate(new Vector3(0f, astre.vitesse_rot * Time.deltaTime, 0f));
        }
        else
        {   // il n'y a pas d'astre
            ren.material.SetFloat("_Close", 1f);
        }
    }
    public void EteindreAffichage()
    {
        moniteur.Eteindre();
    }
    public void AfficherAstre(Astre astre_a_afficher)
    {
        astre = astre_a_afficher;
        ChangerMat();
        moniteur.Allumer();
        print("astre affichage : allumer moniteur");
    }


    private void ChangerMat()
    {
        //Mat
        if (astre.type == "Planete tellurique") //"Planète tellurique"
        {
            ren.material = mat_tellurique;
        }
        else if (astre.type == "Planete gazeuse") //"Planète gazeuse"
        {
            ren.material = mat_gazeux;
        }
        else if (astre.type == "Satellite") //"Satellite"
        {
            ren.material = mat_satellite;
        }
        else if (astre.type == "Comete") //"Comète"
        {
            ren.material = mat_comete;
        }
        else if (astre.type == "Etoile") //"Étoile"
        {
            ren.material = mat_etoile;
        }

        ren.material.SetFloat("_Close", 1f);
        ren.material.SetFloat("_scale", astre.scale);
        ren.material.SetVector("_offset", astre.offset);
        ren.material.SetColor("_col_A", astre.col_A);
        ren.material.SetColor("_col_B", astre.col_B);
        ren.material.SetColor("_col_C", astre.col_C);
        ren.material.SetColor("_col_D", astre.col_D);
    }
}