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
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.transform.Rotate(Vector3.forward, 1f * Time.deltaTime);
=======
        // Controles
        //TODO controles pour test seulement
        if (Input.GetKey(KeyCode.Q))
            Tourner(1f);
        if (Input.GetKey(KeyCode.D))
            Tourner(-1f);
        if (Input.GetKey(KeyCode.S))
            AborderAstreActuel();
        if (Input.GetKeyDown(KeyCode.Z))
            SauterHyperEspace();

        if (!enHyperEspace)
        {
            // Ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * dist_start, transform.up, dist);
            if (hit.collider != null)
            {
                astre_vise = hit.collider.GetComponent<Astre>();
                hit.collider.GetComponent<Astre>().Selectionner();

                Debug.DrawRay(transform.position + transform.up * dist_start, transform.up * dist, Color.red);
            }
            else
            {
                if(astre_vise != null)
                {
                    astre_vise.Deselectionner();
                }
                astre_vise = null;
                Debug.DrawRay(transform.position + transform.up * dist_start, transform.up * dist, Color.yellow);
            }
>>>>>>> remotes/origin/planetes
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Rotate(Vector3.forward, -1f * Time.deltaTime);

<<<<<<< HEAD
        }

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Astre>().Selectionner();
            Debug.DrawRay(transform.position, transform.up * 1000f, Color.red);
=======
    public void Tourner(float direction)
    {
        if (!enHyperEspace)
        {
            this.transform.Rotate(Vector3.forward, direction * vit_rot * Time.deltaTime);
        }
    }
    public void AborderAstreActuel()
    {
        if (astre_actuel != null && !enHyperEspace)
        {
            astre_actuel.Aborder();
        }
    }
    public void SauterHyperEspace()
    {
        if (astre_vise!=null && !astre_vise.inaccessible && !enHyperEspace)
        {
            IEnumerator coroutine = SautHyperEspaceAnimation(astre_vise);
            StartCoroutine(coroutine);
>>>>>>> remotes/origin/planetes
        }
        else
        {
            Debug.DrawRay(transform.position, transform.up * 1000f, Color.yellow);
        }
    }
}
