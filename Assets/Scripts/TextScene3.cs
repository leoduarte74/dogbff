using UnityEngine;
using System.Collections;

public class TextScene3 : MonoBehaviour
{
    public string text1;
    public string text2;
    public string text3;
    private GameObject text_manager;
    private GameObject dialogue_box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text_manager = GameObject.Find("TextManager");
        dialogue_box = GameObject.Find("DialogueBox_0");
        text1 = "This is it, Apartment 16A, this place is a mess... *notices the dog*";
        text2 = "There he is!, poor thing, he has no idea what happened to him";
        text3 = "Well, he'll be better off at the shelter...";
        StartCoroutine(TextManager()); 
    }
    
    IEnumerator TextManager()
    {
        while (!DoorLightManaging.ready_start_text)
        {
            yield return null;
        }
        Debug.Log("TextScene3 knows is time to start the text1");
        dialogue_box.GetComponent<Renderer>().enabled = true;
        text_manager.GetComponent<TextManagement1>().StartDialogueNoHide(text1);
        while (!TextManagement1.finished)
        {
            yield return null;
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        Debug.Log("TextScene3 knows is time to start the text2");
        text_manager.GetComponent<TextManagement1>().StartDialogueNoHide(text2);
        while (!TextManagement1.finished)
        {
            yield return null;
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        Debug.Log("TextScene3 knows is time to start the text3");
        text_manager.GetComponent<TextManagement1>().ShowDialog(text3);
        Debug.Log("CAMBIO DE ESCENA");
    }
}
