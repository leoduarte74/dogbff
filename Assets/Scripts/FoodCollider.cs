using TMPro;
using UnityEngine;

public class FoodCollider : MonoBehaviour
{
    public GameObject text = GameObject.Find("FoodE");
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    // Update is called once per frame
    void Update()
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
