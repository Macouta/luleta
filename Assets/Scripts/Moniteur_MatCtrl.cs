using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moniteur_MatCtrl : MonoBehaviour
{
    private float speed = 1f;
    public Renderer ren;
    private IEnumerator coroutine;

    private void Update()
    {

    }
    private void Start()
    {
        ren = GetComponent<Renderer>();
        ren.material.SetFloat("_Close", 1f);
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
        while (ren.material.GetFloat("_Close") >= 0f)
        {
            ren.material.SetFloat("_Close", Mathf.Max(0f, ren.material.GetFloat("_Close") - Time.deltaTime * speed));
            yield return null;
        }
    }
    private IEnumerator Eteignage()
    {
        while (ren.material.GetFloat("_Close") <= 1f)
        {
            ren.material.SetFloat("_Close", Mathf.Min(1f, ren.material.GetFloat("_Close") + Time.deltaTime * speed));
            yield return null;
        }
    }
    private IEnumerator Glitchage(float duree)
    {
        // float defaut_vitesse = ren.material.GetFloat("_DistortionVitesse");
        // float defaut_vitesse = ren.material.GetFloat("_DistortionVitesse");
        while (duree > 0f)
        {
            duree -= Time.deltaTime;
            ren.material.SetFloat("_DistortionVitesse", Random.Range(0f, 10f));
            ren.material.SetFloat("_DistortionMult", Random.Range(0f, 10f));
            yield return null;
        }
        ren.material.SetFloat("_DistortionVitesse", 1f);
        ren.material.SetFloat("_DistortionMult", 0f);
    }

}
