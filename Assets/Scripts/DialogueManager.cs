using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events; // --Needed for UnityAction (the callback)--

public class DialogueManager : MonoBehaviour
{
    // --Set these in the Inspector--
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;

    private bool isTyping = false;

    // --This will store the function to call when 'E' is pressed again--
    private UnityAction onDialogueClosed;

    // --Standard function (used by the Puddle)--
    public void ShowDialogue(string text)
    {
        // --When called normally, the action is just to hide--
        StartDialogue(text, HideDialogue);
    }

    // --NEW FUNCTION: For the Faint Trigger (or any special event)--
    public void ShowDialogueWithCallback(string text, UnityAction callbackAction)
    {
        // --When called this way, the action is the specific callback--
        StartDialogue(text, callbackAction);
    }

    // --Internal function to start the process--
    private void StartDialogue(string text, UnityAction callback)
    {
        if (dialogueBox.activeSelf || isTyping) return;

        onDialogueClosed = callback; // --Store the action--

        PuppyMovement.is_dialogue_active = true;
        dialogueBox.SetActive(true);

        StartCoroutine(TypeTextEffect(text));
    }

    private IEnumerator TypeTextEffect(string fullText)
    {
        isTyping = true;
        dialogueText.text = "";

        // --Wait one frame (fixes the instant close bug)--
        yield return null;

        foreach (char character in fullText.ToCharArray())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueText.text = fullText;
                break;
            }
            dialogueText.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        // --Wait for 'E' press to continue--
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        // --Execute the stored action (either HideDialogue or the Faint effect)--
        if (onDialogueClosed != null)
        {
            onDialogueClosed.Invoke();
        }
    }

    // --Standard close function--
    public void HideDialogue()
    {
        PuppyMovement.is_dialogue_active = false;
        dialogueBox.SetActive(false);
    }
}