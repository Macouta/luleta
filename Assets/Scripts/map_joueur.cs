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

    private bool mouseHold;

    public bool MouseHold { get => mouseHold; set => mouseHold = value; }
    public Astre Astre_actuel { get => astre_actuel; set => astre_actuel = value; }

    void Start()
    {

    }

    void Update()
    {

        if(mouseHold) {
            float pos = -Input.GetAxis("Mouse X");
            Tourner(pos);
        }
        // Controles
        //TODO controles pour test seulement
        if (Input.GetKey(KeyCode.Q))
            Tourner(1f);
        if (Input.GetKey(KeyCode.D))
            Tourner(-1f);
        if (Input.GetKey(KeyCode.S))
            AborderAstreActuel();
        if (Input.GetKeyDown(KeyCode.Z))
            SauterHyperEspace(1f);

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
        }
    }

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
    public void SauterHyperEspace(float duration)
    {
        if (isJumpAllowed())
        {
            IEnumerator coroutine = SautHyperEspaceAnimation(astre_vise, duration);
            StartCoroutine(coroutine);
        }
    }

    public bool isJumpAllowed() {
        return astre_vise != null && !astre_vise.inaccessible && !enHyperEspace;
    }
    IEnumerator SautHyperEspaceAnimation(Astre astre_destination, float duree)
    {

        Debug.Log("duree " + duree);
        //depart
        enHyperEspace = true;
        if (astre_actuel == null)
        {
            astre_manager.DefinirAstreActuel(-1);
        }
        else {
            astre_actuel.Deselectionner();
            astre_actuel.Partir();
        }

        //deplacement
        Vector3 delta_difference = transform.position - astre_destination.transform.position;
        Vector3 pos_debut = astre_manager.transform.position;
        Vector3 pos_arrivee = pos_debut + delta_difference;
        float normalizedTime = 0;
        while( normalizedTime <= 1f) {
            astre_manager.transform.position = new Vector3(
                                            Mathf.SmoothStep(pos_debut.x, pos_arrivee.x, normalizedTime),
                                            Mathf.SmoothStep(pos_debut.y, pos_arrivee.y, normalizedTime),
                                            Mathf.SmoothStep(pos_debut.z, pos_arrivee.z, normalizedTime)
                                            );
            normalizedTime += Time.deltaTime / duree;
            yield return null;
        }

        //arrivee
        astre_destination.Arriver();
        astre_actuel = astre_destination;
        astre_actuel.Deselectionner();
        enHyperEspace = false;
        yield return null;
    }
}
