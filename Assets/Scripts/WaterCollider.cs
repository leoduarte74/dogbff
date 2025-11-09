using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    private GameObject text;
    private string agua = "Drinking...";
    private TextManagement textManagement;
    private GameObject dialogue_box;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GameObject.Find("WaterE");
        text.GetComponent<TextMeshProUGUI>().enabled = false;
        textManagement = FindFirstObjectByType<TextManagement>();
        dialogue_box = GameObject.Find("DialogueBox_0");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        text.GetComponent<TextMeshProUGUI>().enabled = true;
        if (Input.GetKeyDown(KeyCode.E) && !TextManagement.pressed)
        {
            dialogue_box.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().enabled = false;
            TextBooleanManager.text_active = true;
            textManagement.ShowDialog(agua);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        text.GetComponent<TextMeshProUGUI>().enabled = false;
        TextBooleanManager.text_active = false;
    }
}
