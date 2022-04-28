using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moniteur_MatCtrl : MonoBehaviour
{
    public float speed;
    private bool isPowerUp = false;
    private Material mat_moniteur;

    private void Start()
    {
        mainMaterial.SetFloat("_Close", 50f);
        infoMaterial.SetFloat("_Close", 50f);
        interactionMaterial.SetFloat("_Close", 50f);
        StartCoroutine(HandleStart(mainMaterial));
    }

    private IEnumerator HandleStart(Material mat)
    {
        while (mat.GetFloat("_Close") > 0f)
        {
            mat.SetFloat("_Close", Mathf.Max(0, mat.GetFloat("_Close") - Time.deltaTime * speed));
            yield return null;
        }
    }

    public void startGame()
    {
        StartCoroutine(HandleStart(infoMaterial));
        StartCoroutine(HandleStart(interactionMaterial));
        SceneManager.LoadScene("GameScene");
    }
}
