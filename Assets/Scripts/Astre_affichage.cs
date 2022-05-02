using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
public class Astre_affichage : MonoBehaviour
{
    public Material mat_tellurique, mat_gazeux, mat_satellite, mat_comete, mat_etoile;
    private Renderer ren;
    private Astre astre;
    private float fac_power;
    private IEnumerator coroutine;

    void Start()
    {
        ren = this.GetComponent<Renderer>();
        coroutine = AnimationChangerMat();
        fac_power = 0f;
        ren.material.SetFloat("_power", fac_power);
    }

    void Update()
    {
        if (astre != null)
        {
            this.transform.Rotate(new Vector3(0f, astre.vitesse_rot * Time.deltaTime, 0f));
        }
    }

    public void AfficherAstre(Astre astre_a_afficher)
    {
        astre = astre_a_afficher;
        StopCoroutine(coroutine);
        coroutine = AnimationChangerMat();
        StartCoroutine(coroutine);
    }

    private void ChangerMat()
    {
        //Mat
        if (astre.type == "Planète tellurique") //"Planète tellurique"
        {
            ren.material = mat_tellurique;
        }
        else if (astre.type == "Planète gazeuse") //"Planète gazeuse"
        {
            ren.material = mat_gazeux;
        }
        else if (astre.type == "Satellite") //"Satellite"
        {
            ren.material = mat_satellite;
        }
        else if (astre.type == "Comète") //"Comète"
        {
            ren.material = mat_comete;
        }
        else if (astre.type == "Étoile") //"Étoile"
        {
            ren.material = mat_etoile;
        }

        //ren.material.SetFloat("_power", fac_power);
        ren.material.SetFloat("_scale", astre.scale);
        ren.material.SetVector("_offset", astre.offset);
        ren.material.SetColor("_col_A", astre.col_A);
        ren.material.SetColor("_col_B", astre.col_B);
        ren.material.SetColor("_col_C", astre.col_C);
        ren.material.SetColor("_col_D", astre.col_D);
    }


    private IEnumerator AnimationChangerMat()
    {
        float vitesse = 2f;

        while (fac_power > 0f)
        {
            fac_power = Mathf.Clamp01(fac_power - Time.deltaTime * vitesse);
            ren.material.SetFloat("_power", fac_power);
            yield return null;
        }

        ChangerMat();

        while (fac_power < 1f)
        {
            fac_power = Mathf.Clamp01(fac_power + Time.deltaTime * vitesse);
            ren.material.SetFloat("_power", fac_power);
            yield return null;
        }
    }
}