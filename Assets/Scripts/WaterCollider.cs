using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    public GameObject text = GameObject.Find("WaterE");
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        text.GetComponent<TextMeshProUGUI>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        text.GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
