using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Astre_description : MonoBehaviour
{
    public TextMeshProUGUI txt_info, txt_relation, txt_progression;
    public Image image_astre;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecrireAstre(Astre astre, float pourcentage_progression)
    {
        string info =
                    "NOM: " + astre.nom
         + "\r\n" + "TYPE: " + astre.type
         + "\r\n" + "POPULATION: " + astre.population.ToString() + " individus"
         + "\r\n" + "DEFENSE: " + astre.defense.ToString()
         + "\r\n" + "DIAMETRE: " + astre.diametre.ToString() + " km"
         + "\r\n" + "ROTATION: " + astre.periode_rotation.ToString() + " h"
         + "\r\n" + "REVOLUTION: " + astre.periode_revolution.ToString() + " h"
         ;
        
        string relation =
                    "VISITES: " + astre.visites
         + "\r\n" + "ETAT: " + GenererHostilite(astre)
         ;
        string progression =
                    pourcentage_progression.ToString("F2") + " % "
         + "\r\n" + "d'astres découverts";
        ;

        txt_info.text = info;
        txt_relation.text = relation;
        txt_progression.text = progression;
        image_astre.sprite = astre.image.sprite;
        image_astre.color = astre.col_defaut;
    }

    private string GenererHostilite(Astre astre)
    {
        if (astre.inaccessible)
            return "Hostil";
        else if (astre.visites <= 1)
            return "Neutre";
        else if (astre.visites <= 3)
            return "Cordial";
        else
            return "Amical";
    }
}