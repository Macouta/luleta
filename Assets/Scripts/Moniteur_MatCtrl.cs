using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moniteur_MatCtrl : MonoBehaviour
{
    private float speed = 3f;
    private Material mat_moniteur;
    private IEnumerator coroutine;

    private void Update()
    {
        //TODO test
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("bouton q");
            Allumer();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("bouton d");
            Eteindre();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("bouton S");
            Glitcher();
        }
    }
    private void Start()
    {
        mat_moniteur = GetComponent<Renderer>().material;
        mat_moniteur.SetFloat("_Close", 1f);
    }

    //Fonctions publiques
    public void Allumer()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
        coroutine = Allumage();
        StartCoroutine(coroutine);
    }
    public void Eteindre()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = Eteignage();
        StartCoroutine(coroutine);
    }
    public void Glitcher(float duree = 1f)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = Glitchage(duree);
        StartCoroutine(coroutine);
    }

    // Coroutines
    private IEnumerator Allumage()
    {
        while (mat_moniteur.GetFloat("_Close") >= 0f)
        {
            mat_moniteur.SetFloat("_Close", Mathf.Max(0f, mat_moniteur.GetFloat("_Close") - Time.deltaTime * speed));
            yield return null;
        }
    }
    private IEnumerator Eteignage()
    {
        while (mat_moniteur.GetFloat("_Close") <= 1f)
        {
            mat_moniteur.SetFloat("_Close", Mathf.Min(1f, mat_moniteur.GetFloat("_Close") + Time.deltaTime * speed));
            yield return null;
        }
    }
    private IEnumerator Glitchage(float duree)
    {
        float defaut_vitesse = mat_moniteur.GetFloat("_DistortionVitesse");
        float defaut_mult = mat_moniteur.GetFloat("_DistortionMult");
        while (duree > 0f)
        {
            duree -= Time.deltaTime;
            mat_moniteur.SetFloat("_DistortionVitesse", Random.Range(0f, 10f));
            mat_moniteur.SetFloat("_DistortionMult", Random.Range(0f, 10f));
            yield return null;
        }
        mat_moniteur.SetFloat("_DistortionVitesse", defaut_vitesse);
        mat_moniteur.SetFloat("_DistortionMult", defaut_mult);
    }

}
