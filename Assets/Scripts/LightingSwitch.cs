using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSwitch : MonoBehaviour
{
    private Light[] lights;

    public float neutral_intensity;
    public float fictional_intensity;

    public float neutral_temperature;
    public float fictional_temperature;

    public GameObject neut_light;
    public GameObject fic_light;

    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        /*recup toutes les infos des lights pour les passer */
        neut_light.SetActive(true);
        fic_light.SetActive(false);
        StartCoroutine(neutralLighting(duration));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void launchNeutralLighting()
    {
        neut_light.SetActive(true);
        fic_light.SetActive(false);
        StartCoroutine(neutralLighting(duration));
    }

    public void launchFictionalLighting()
    {
        neut_light.SetActive(false);
        fic_light.SetActive(true);
        StartCoroutine(fictionalLighting(duration));
    }

    IEnumerator neutralLighting(float duration)
    {
        float counter = 0f;
        lights = FindObjectsOfType(typeof(Light)) as Light[];

        while (counter < duration)
        {
            foreach (Light current_light in lights)
            {
                current_light.intensity = Mathf.Lerp(fictional_intensity, neutral_intensity, tweening(counter / duration));
                current_light.colorTemperature = Mathf.Lerp(fictional_temperature, neutral_temperature, tweening(counter / duration));
            }
            counter += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator fictionalLighting(float duration)
    {
        float counter = 0f;
        lights = FindObjectsOfType(typeof(Light)) as Light[];

        while (counter < duration)
        {
            foreach (Light current_light in lights)
            {
                current_light.intensity = Mathf.Lerp(neutral_intensity, fictional_intensity, tweening(counter / duration));
                current_light.colorTemperature = Mathf.Lerp(neutral_temperature, fictional_temperature, tweening(counter / duration));
            }
            counter += Time.deltaTime;
            yield return null;
        }
    }

    private float tweening(float time)
    {
        return 0.5f + 0.5f * Mathf.Sin((time - 0.5f) * Mathf.PI);
    }
}