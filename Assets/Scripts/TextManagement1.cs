using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class TextManagement1 : MonoBehaviour
{
    private GameObject dialogue_box;
    private TextMeshProUGUI dialogue_text;
    private GameObject text_object;
    private GameObject OptionText;
    private float typing_speed = 0.035f;
    public static bool pressed = false;
    public static bool finished = true;

    private UnityAction onDialogueClosed;

    private void Start()
    {
        dialogue_box = GameObject.Find("DialogueBox_0");
        text_object = GameObject.Find("ActionText");
        dialogue_text = text_object.GetComponent<TextMeshProUGUI>();
        

    }
    public void ShowDialog(string text)
    {
        StartDialogue(text, HideDialogue);
    }


    private void StartDialogue(string text, UnityAction callback)
    {
        pressed = true;
        onDialogueClosed = callback;
        TextBooleanManager.text_active = true;

        StartCoroutine(TypeTextEffect(text));
    }

    public void StartDialogueNoHide(string text)
    {
        Debug.Log("I got called");
        pressed = true;
        TextBooleanManager.text_active = true;

        StartCoroutine(TypeTextEffectNoHide(text));
    }

    private IEnumerator TypeTextEffectNoHide(string text)
    {
        finished = false;
        Debug.Log("i got called too");
        dialogue_text.text = " ";

        yield return null;

        foreach (char character in text.ToCharArray())
        {
            dialogue_text.text += character;
            yield return new WaitForSeconds(typing_speed);
        }
        finished = true;
    }
    private IEnumerator TypeTextEffect(string text)
    {
        
        dialogue_text.text = " ";

        yield return null;

        foreach (char character in text.ToCharArray())
        {
            dialogue_text.text += character;
            yield return new WaitForSeconds(typing_speed);
        }

        if (Couch2Collider.sleep_options)
        {
            OptionText.SetActive(true);
            yield return StartCoroutine(Decition());
            if (onDialogueClosed != null)
            {
                dialogue_text.text = " ";
                onDialogueClosed.Invoke();
            }
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        if (onDialogueClosed != null)
        {
            dialogue_text.text = " ";
            onDialogueClosed.Invoke();
        }
    }

    public void HideDialogue()
    {
        pressed = false;
        TextBooleanManager.text_active = false;
        dialogue_box.SetActive(false);
    }

    private IEnumerator Decition()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OptionText.SetActive(false);
                Debug.Log("Se cancelo");
                yield break;

            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                OptionText.SetActive(false);
                Debug.Log("Cambio de Escena");
                yield break;
            }
            yield return null;
        }
        
    }

  
}
