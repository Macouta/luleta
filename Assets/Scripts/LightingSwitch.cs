using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSwitch : MonoBehaviour
{

    public float neutral_intensity;
    public float fictional_intensity;

    public float neutral_temperature;
    public float fictional_temperature;

    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        neutralLighting(duration);
        fictionalLighting(duration);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(neutralLighting(duration));
        }
        if (Input.GetKeyDown("g"))
        {
            StartCoroutine(fictionalLighting(duration));
        }
    }

    IEnumerator neutralLighting(float duration)
    {
        float counter = 0f;
        Debug.Log("test neutral");
        while (counter < duration)
        {
            counter += Time.deltaTime;

            Light current_light = this.transform.GetChild(0).gameObject.GetComponent<Light>();
            current_light.intensity = Mathf.Lerp(fictional_intensity, neutral_intensity, counter / duration);
            current_light.colorTemperature = Mathf.Lerp(fictional_temperature, neutral_temperature, counter / duration);

            current_light = this.transform.GetChild(1).gameObject.GetComponent<Light>();
            current_light.intensity = Mathf.Lerp(fictional_intensity, neutral_intensity, counter / duration);
            current_light.colorTemperature = Mathf.Lerp(fictional_temperature, neutral_temperature, counter / duration);
            yield return null;
        }
    }

    IEnumerator fictionalLighting(float duration)
    {
        float counter = 0f;
        Debug.Log("test fictional");
        while (counter < duration)
        {
            counter += Time.deltaTime;

            Light current_light = this.transform.GetChild(0).gameObject.GetComponent<Light>();
            current_light.intensity = Mathf.Lerp(neutral_intensity, fictional_intensity, counter / duration);
            current_light.colorTemperature = Mathf.Lerp(neutral_temperature, fictional_temperature, counter / duration);

            current_light = this.transform.GetChild(1).gameObject.GetComponent<Light>();
            current_light.intensity = Mathf.Lerp(neutral_intensity, fictional_intensity, counter / duration);
            current_light.colorTemperature = Mathf.Lerp(neutral_temperature, fictional_temperature, counter / duration);
            yield return null;
        }
    }
}
