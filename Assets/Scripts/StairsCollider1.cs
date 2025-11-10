using System;
using TMPro;
using UnityEngine;

public class StairsCollider1 : MonoBehaviour
{
    private GameObject text;
    private string escalera = "These stairs look dangerous, I'm afraid of heights!";
    private TextManagement textManagement;
    private GameObject dialogue_box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GameObject.Find("StairsE");
        text.GetComponent<TextMeshProUGUI>().enabled = false;
        textManagement = FindFirstObjectByType<TextManagement>();
        dialogue_box = GameObject.Find("DialogueBox_0");
    }

    // Update is called once per frame
    void Update()
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
            textManagement.ShowDialog(escalera);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
            text.GetComponent<TextMeshProUGUI>().enabled = false;
            TextBooleanManager.text_active = false;
    }
        
}
