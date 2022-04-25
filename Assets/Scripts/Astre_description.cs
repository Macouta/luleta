using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Astre_description : MonoBehaviour
{
    public TextMeshProUGUI text_description;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecrireAstre(Astre astre)
    {
        string description = 
                       "NOM : " + astre.name
            + "\r\n" + "TYPE : " + astre.type
            + "\r\n" + "POPULATION : " + astre.population.ToString() + " individus"
            + "\r\n" + "ROTATION : " + astre.periode_rotation.ToString() + " h"
            + "\r\n" + "REVOLUTION : " + astre.periode_revolution.ToString() + " h"
            + "\r\n" + "DIAMETRE : " + astre.diametre.ToString() + " km"
            + "\r\n" + "DIFFICULTE : " + astre.population.ToString()
            ;
        text_description.text = description;
    }
}