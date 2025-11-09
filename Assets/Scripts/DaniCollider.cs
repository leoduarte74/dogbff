using TMPro;
using UnityEngine;

public class DaniCollider : MonoBehaviour
{
    private GameObject text;
    private TextManagement textManagement;
    private GameObject dialogue_box;
    private string dani = "I wonder who that is...";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GameObject.Find("DaniE");
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
            textManagement.ShowDialog(dani);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        text.SetActive(false);
        TextBooleanManager.text_active = false;
    }
}
