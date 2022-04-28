using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_joueur : MonoBehaviour
{
    public Astre_manager astre_manager;
    private Astre astre_vise, astre_actuel;
    private bool enHyperEspace = false;
    private float vit_rot = 130f;
    private float dist = 0.75f;
    private float dist_start = 0.25f;

    void Start()
    {

    }

    void Update()
    {
        if (!enHyperEspace)
        {
            // Controles
            //TODO controles pour test seulement
            if (Input.GetKey(KeyCode.Q))
                Tourner(1f);
            if (Input.GetKey(KeyCode.D))
                Tourner(-1f);
            if (Input.GetKey(KeyCode.S))
            {
                if(astre_actuel != null)
                {
                    astre_actuel.Aborder();
                }
            }


                // Ray
                RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * dist_start, transform.up, dist);
            if (hit.collider != null)
            {
                astre_vise = hit.collider.GetComponent<Astre>();
                hit.collider.GetComponent<Astre>().Selectionner();

                //TODO controles pour test seulement
                if (Input.GetKeyDown(KeyCode.Z))
                    SautHyperEspace(astre_vise);

                Debug.DrawRay(transform.position + transform.up * dist_start, transform.up * dist, Color.red);
            }
            else
            {
                if(astre_vise != null)
                {
                    astre_vise.Deselectionner();
                }
                Debug.DrawRay(transform.position + transform.up * dist_start, transform.up * dist, Color.yellow);
            }
        }
    }

    public void Tourner(float direction)
    {
        this.transform.Rotate(Vector3.forward, direction * vit_rot * Time.deltaTime);

    }

    private void SautHyperEspace(Astre astre_destination)
    {
        if (!astre_destination.inaccessible)
        {
            IEnumerator coroutine = SautHyperEspaceAnimation(astre_destination);
            StartCoroutine(coroutine);
        }
    }
    IEnumerator SautHyperEspaceAnimation(Astre astre_destination)
    {
        enHyperEspace = true;
        astre_destination.Arriver();

        //pos
        float duree = 1f;
        float temps_debut = Time.time;
        Vector3 delta_difference = transform.position - astre_destination.transform.position;
        Vector3 pos_debut = astre_manager.transform.position;
        Vector3 pos_arrivee = pos_debut + delta_difference;

        //anim
        while (Time.time < temps_debut + duree)
        {
            astre_manager.transform.position = new Vector3(
                                            Mathf.SmoothStep(pos_debut.x, pos_arrivee.x, (Time.time - temps_debut)),
                                            Mathf.SmoothStep(pos_debut.y, pos_arrivee.y, (Time.time - temps_debut)),
                                            Mathf.SmoothStep(pos_debut.z, pos_arrivee.z, (Time.time - temps_debut))
                                            );
            yield return null;
        }
        astre_actuel = astre_destination;
        enHyperEspace = false;
        yield return null;
    }
}
