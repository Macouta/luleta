using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSwitch : MonoBehaviour
{
    private Dictionary<Light, float> lights_info = new Dictionary<Light, float>();

    public GameObject neut_light;
    public GameObject fic_light;

    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        Light[] lights = FindObjectsOfType(typeof(Light)) as Light[];
        foreach (Light current_light in lights)
        {
            lights_info.Add(current_light, current_light.intensity);
        }
        neut_light.SetActive(true);
        fic_light.SetActive(false);
        StartCoroutine(switchLighting(duration));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void launchNeutralLighting()
    {
        neut_light.SetActive(true);
        fic_light.SetActive(false);
        StartCoroutine(switchLighting(duration));
    }

    public void launchFictionalLighting()
    {
        neut_light.SetActive(false);
        fic_light.SetActive(true);
        StartCoroutine(switchLighting(duration));
    }

    private IEnumerator switchLighting(float duration)
    {
        float counter = 0f;
        Light[] lights = FindObjectsOfType(typeof(Light)) as Light[];

        while (counter < duration)
        {
            foreach (Light current_light in lights)
            {
                current_light.intensity = Mathf.Lerp(0, lights_info[current_light], tweening(counter / duration));
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