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

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            neut_light.SetActive(true);
            fic_light.SetActive(false);
            StartCoroutine(neutralLighting(duration));
        }
        if (Input.GetKeyDown("g"))
        {
            neut_light.SetActive(false);
            fic_light.SetActive(true);
            StartCoroutine(fictionalLighting(duration));
        }
    }

    IEnumerator neutralLighting(float duration)
    {
        float counter = 0f;
        lights = FindObjectsOfType(typeof(Light)) as Light[];

        foreach (Light current_light in lights)
        {
            counter = 0f;
            Debug.Log("test neutral : " + current_light.name);
            while (counter < duration)
            {
                counter += Time.deltaTime;
                Debug.Log("test neutral : " + counter);

                current_light.intensity = Mathf.Lerp(fictional_intensity, neutral_intensity, counter / duration);
                current_light.colorTemperature = Mathf.Lerp(fictional_temperature, neutral_temperature, counter / duration);
            }
            yield return null;
        }
    }

    IEnumerator fictionalLighting(float duration)
    {
        float counter = 0f;
        lights = FindObjectsOfType(typeof(Light)) as Light[];

        foreach (Light current_light in lights)
        {
            counter = 0f;
            Debug.Log("test fictional : " + current_light.name);
            while (counter < duration)
            {
                counter += Time.deltaTime;
                Debug.Log("test fictional : " + counter);

                current_light.intensity = Mathf.Lerp(neutral_intensity, fictional_intensity, counter / duration);
                current_light.colorTemperature = Mathf.Lerp(neutral_temperature, fictional_temperature, counter / duration);
            }
            yield return null;
        }
    }
}