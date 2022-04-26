using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_joueur : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.transform.Rotate(Vector3.forward, 1f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Rotate(Vector3.forward, -1f * Time.deltaTime);

        }

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Astre>().Selectionner();
            Debug.DrawRay(transform.position, transform.up * 1000f, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * 1000f, Color.yellow);
        }
    }
}
