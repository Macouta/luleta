using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector_animation : MonoBehaviour
{
    private Vector3 rotation;

    private void Start()
    {
        rotation = new Vector3(0f, 0f, 1f);
    }
    void Update()
    {
        this.transform.Rotate(rotation * Time.deltaTime);
    }

    public void PlayWin()
    {
        this.GetComponent<Animator>().Play("pojector_Success");
    }
    public void PlayLoose()
    {
        this.GetComponent<Animator>().Play("projector_GameOver");
    }
}
