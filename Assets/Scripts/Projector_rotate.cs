using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector_rotate : MonoBehaviour
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
}
