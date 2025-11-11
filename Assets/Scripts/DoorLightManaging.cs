using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class DoorLightManaging : MonoBehaviour
{
    private GameObject light;
    public static bool ready_start_text = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GameObject.Find("Door Light");
        StartCoroutine(OpenSlowly());
    }

    IEnumerator OpenSlowly()
    {
        yield return new WaitForSeconds(4f);
        while (light.GetComponent<Light2D>().intensity < 3f)
        {
            light.GetComponent<Light2D>().intensity += 0.25f;
            yield return null;
        }

        ready_start_text = true;
        Debug.Log("Text should start now");
    }
    
}
