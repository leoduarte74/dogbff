using TMPro;
using UnityEngine;

public class Couch1Collider : MonoBehaviour
{
    private GameObject text;
    private TextManagement textManagement;
    private GameObject dialogue_box;
    private string couch1 = " I am perpetually astonished by how this sofa achieves a flawless equilibrium \r\n between lateral stability, cushion airflow, and long-term postural alignment, \\n making it an unrivaled triumph of everyday seating engineering, woof!";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GameObject.Find("Couch1E");
        text.SetActive(false);
        textManagement = FindFirstObjectByType<TextManagement>();
        dialogue_box = GameObject.Find("DialogueBox_0");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        text.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E) && !TextManagement.pressed)
        {
            dialogue_box.SetActive(true);
            text.SetActive(false);
            TextBooleanManager.text_active = true;
            textManagement.ShowDialog(couch1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        text.SetActive(false);
        TextBooleanManager.text_active = false;
    }
}
