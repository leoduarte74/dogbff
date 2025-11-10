using TMPro;
using UnityEngine;

public class Couch2Collider : MonoBehaviour
{
    private GameObject text;
    private TextManagement textManagement;
    private GameObject dialogue_box;
    public static bool sleep_options = false;
    private string couch2 = "Maybe sleeping again isn't such a bad idea...";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GameObject.Find("Couch2E");
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
            sleep_options = true;
            textManagement.ShowDialog(couch2);
            

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        sleep_options = false;
        text.SetActive(false);
        TextBooleanManager.text_active = false;
    }
}
