using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Astre : MonoBehaviour
{
    // donnees
    public int id;
    public string nom, type;
    public int periode_rotation, periode_revolution, diametre, population, defense;
    private Dictionary<string, List<int>> astre_data = new Dictionary<string, List<int>>()
    {   // type, rotation_max, revolution_max, diametre_max, population_max
        {"Planete tellurique", new(){2000, 1500000, 50000, 1000000000}},
        {"Planete gazeuse",    new(){1000, 1500000, 50000, 1000000000}},
        {"Satellite",          new(){200, 300000, 10000, 10000}},
        {"Comete",             new(){100, 300000, 1000, 2500}},
        {"Etoile",             new(){100000, 100000, 100000, 0}},
    };
    // relation
    public bool inaccessible = false;
    public bool decouverte = false;
    public int visites = 0;
    // visuel
    public float scale;
    public Vector3 offset;
    public Color col_A, col_B, col_C, col_D;
    public Color col_selection, col_defaut;
    public Sprite sprite_tellurique, sprite_gazeuse, sprite_satellite, sprite_comete, sprite_etoile;

    // gameobject
    public float vitesse_rot;
    private Astre_manager astre_manager;
    public Image image;


    void Start()
    {
        astre_manager = GetComponentInParent<Astre_manager>();
        InitAstre();
    }

    public void InitAstre()
    {
        //Donnes
        nom = AINamesGenerator.Utils.GetRandomName();
        InitType(1f, 2f, 2f, 2f, 1f); //tellurique, gazeuse, satellite, comete, etoile
        periode_rotation = Random.Range(25, astre_data[type][0]); //h
        periode_revolution = Random.Range(3000, astre_data[type][1]); //h
        diametre = Random.Range(500, astre_data[type][2]); //km
        population = Random.Range(0, astre_data[type][3]); //nb d'habitants ou d'ennemis
        defense = (int)(Mathf.Clamp01((Random.Range(1f, 5f) * (float)population / 1000000000f)) * 100f);
        vitesse_rot = (360f / (float)periode_rotation) * 30f + 5f;

        //Mat variables
        scale = Random.Range(0f, 1f);
        Color[] HarmonicColors = Get4HarmonicColors();
        col_A = HarmonicColors[0];
        col_B = HarmonicColors[1];
        col_C = HarmonicColors[2];
        col_D = HarmonicColors[3];

        //Sprite couleurs
        col_selection = Color.white;
        col_defaut = Color.gray;// new Color(0.2f, 0.30f, 0.60f);
        image = GetComponent<Image>();
        image.color = col_defaut;
        AppliquerSprite();

        //Gameobject
        float offset = 0.1f;
        this.transform.Translate(new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0f));
        float go_scale = 1f + diametre * 0.000015f;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
        this.transform.localScale = new Vector3(transform.localScale.x * go_scale,
                                                transform.localScale.y * go_scale,
                                                transform.localScale.z * go_scale);
    }

    private void InitType(float p0, float p1, float p2, float p3, float p4)
    {
        //normalisation
        float fac_norm = 1f / (p0 + p1 + p2 + p3 + p4);
        p0 *= fac_norm;
        p1 *= fac_norm;
        p2 *= fac_norm;
        p3 *= fac_norm;
        p4 *= fac_norm;

        float ran_type = Random.Range(0f, 1f);
        if (ran_type > p0 + p1 + p2 + p3)
            type = astre_data.ElementAt(4).Key;
        else if (ran_type > p0 + p1 + p2)
            type = astre_data.ElementAt(3).Key;
        else if (ran_type > p0 + p1)
            type = astre_data.ElementAt(2).Key;
        else if (ran_type > p0)
            type = astre_data.ElementAt(1).Key;
        else
            type = astre_data.ElementAt(0).Key;
    }

    private void AppliquerSprite()
    {
        if (type == "Planete tellurique") //"Plan�te tellurique"
        {
           image.sprite = sprite_tellurique;
        }
        else if (type == "Planete gazeuse") //"Plan�te gazeuse"
        {
            image.sprite = sprite_gazeuse;
        }
        else if (type == "Satellite") //"Satellite"
        {
            image.sprite = sprite_satellite;
        }
        else if (type == "Comete") //"Com�te"
        {
            image.sprite = sprite_comete;
        }
        else if (type == "Etoile") //"�toile"
        {
            image.sprite = sprite_etoile;
        }
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

    private float ColorCompare(Color colA, Color colB)
    {
        Vector3 vColA = new Vector3(colA.r, colA.g, colA.b);
        Vector3 vColB = new Vector3(colB.r, colB.g, colB.b);
        return Vector3.Distance(vColA, vColB);
    }
    private Color ColorSaturate(Color col)
    {
        float h, s, v;
        Color.RGBToHSV(col, out h, out s, out v);
        return Color.HSVToRGB(h, Mathf.Clamp(s, 0.5f, 1f), Mathf.Clamp(v, 0f, 0.8f));
    }

    //TODO Am�liorer l'harmonie des couleurs, �viter certaines ? (violet)

    public void Selectionner()
    {
        image.color = col_selection;
    }
    public void Deselectionner()
    {
        image.color = col_defaut;
    }
    public void Partir()
    {
        astre_manager.DefinirAstreActuel(-1);
    }
    public void Arriver()
    {
        visites++;
        if (!decouverte)
        {
            decouverte = true;
            col_defaut = ColorSaturate(col_A);
            astre_manager.DecouvrirAstre();
        }
        astre_manager.DefinirAstreActuel(id);
    }
    public void Aborder()
    {
        inaccessible = true;
        col_defaut = new Color(0.3f, 0.3f, 0.3f);
        col_selection = Color.red;
        image.color = col_defaut;
        astre_manager.DefinirAstreActuel(id); //mettre � jour les infos de description
    }
}