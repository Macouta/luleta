using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Astre : MonoBehaviour
{
    // donnees
    public string nom, type;
    public int periode_rotation, periode_revolution, diametre, population;
    public float vitesse_rot;
    private List<string> liste_noms = new()
    {
        "nom1",
        "nom2",
        "nom3"
    };
    private Dictionary<string, List<int>> astre_data = new Dictionary<string, List<int>>()
    {   // type, rotation_max, revolution_max, diametre_max, population_max 
        {"Planète tellurique", new(){1500, 1500000, 50000, 1000000000} },
        {"Planète gazeuse",    new(){1500, 1500000, 50000, 1000000000}},
        {"Satellite",          new(){300, 300000, 10000, 10000}},
        {"Comète",             new(){300, 300000, 1000, 2500}},
        {"Étoile",             new(){300000, 0, 100000, 0}},
    };

    // visuel
    public float scale;
    public Vector3 offset;
    public Color col_A, col_B, col_C, col_D;


    void Start()
    {
        InitAstre();
    }
    void Awake()
    {
        InitAstre();
    }

    public void InitAstre()
    {
        nom = liste_noms[Random.Range(0, liste_noms.Count)];
        type = astre_data.ElementAt(Random.Range(0, astre_data.Count)).Key;
        periode_rotation = Random.Range(6, astre_data[type][0]); //h
        periode_revolution = Random.Range(3000, astre_data[type][1]); //h
        diametre = Random.Range(500, astre_data[type][2]); //km
        population = Random.Range(0, astre_data[type][3]); //nb d'habitants ou d'ennemis
        vitesse_rot = 360f / periode_rotation;

        //Mat variables
        scale = Random.Range(0f, 1f);
        offset = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
        Color[] HarmonicColors = Get4HarmonicColors();
        col_A = HarmonicColors[0];
        col_B = HarmonicColors[1];
        col_C = HarmonicColors[2];
        col_D = HarmonicColors[3];
    }

    private Color[] Get4HarmonicColors()
    {
        //couleur base
        float hue = Random.Range(0f, 1f);
        float sat = Random.Range(0f, 1f);
        Color colA = Color.HSVToRGB(hue, sat, Random.Range(0.66f, 1f));
        //couleur similaire
        float off = Random.Range(0f, 0.1f);
        Color colB = Color.HSVToRGB(Mathf.Repeat(hue + off, 1f), Mathf.Repeat(sat + off, 1f), Random.Range(0.5f, 0.9f));
        //couleur complementaire
        hue = Mathf.Repeat(hue + 0.5f, 1f);
        sat = Mathf.Repeat(hue + 1f, 1f);
        off = Random.Range(0f, 0.2f);
        Color colC = Color.HSVToRGB(hue, Mathf.Clamp01(sat + off), Random.Range(0.4f, 1f));
        //couleur complementaire similaire
        off = Random.Range(0f, 0.1f);
        Color colD = Color.HSVToRGB(Mathf.Repeat(hue + off, 1f), Mathf.Clamp01(sat + off), Random.Range(0.3f, 0.9f));

        Color[] HarmonicColors = new Color[4] { colA, colB, colC, colD };
        return HarmonicColors;
    }
}


//TODO
//trouver des noms