using UnityEngine;
using TMPro;

public class CharcoInteraccion : MonoBehaviour
{
    // Variables assigned in the Inspector
    public GameObject promptText;

    // Components initialized in Start
    private AudioSource puddleAudio;
    private DialogueManager dialogueManager;

    // Interaction state flag
    private bool isPuppyInside = false;

    // --Initializes components and hides prompt--
    void Start()
    {
        puddleAudio = GetComponent<AudioSource>();
        dialogueManager = FindFirstObjectByType<DialogueManager>();

        // --Hides the 'Press E' prompt on start--
        if (promptText != null)
        {
            promptText.SetActive(false);
        }
    }

    // --Called when an object enters the trigger--
    private void OnTriggerEnter2D(Collider2D other)
    {
        // --Checks if the object is the Puppy--
        if (other.CompareTag("Puppy"))
        {
            isPuppyInside = true;

            // --Shows the 'Press E' prompt--
            if (promptText != null)
            {
                promptText.SetActive(true);
            }
        }
    }

    // --Called when an object exits the trigger--
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Puppy"))
        {
            isPuppyInside = false;

            // --Hides the 'Press E' prompt--
            if (promptText != null)
            {
                promptText.SetActive(false);
            }
        }
    }

    // --Main interaction logic--
    void Update()
    {
        // --Prevents interaction if dialogue is already active--
        if (PuppyMovement.is_dialogue_active) return;

        // --Checks if Puppy is inside and 'E' is pressed--
        if (isPuppyInside && Input.GetKeyDown(KeyCode.E))
        {
            // --The dialogue text to show--
            string charcoText = "Ugh.. the water in this puddle is freezing, my paws are freezing...";

            // --Plays the splash sound--
            if (puddleAudio != null)
            {
                puddleAudio.Play();
            }

            // --Shows the dialogue box--
            if (dialogueManager != null)
            {
                dialogueManager.ShowDialogue(charcoText);
            }

            // --Hides the prompt since dialogue is starting--
            promptText.SetActive(false);
        }
    }
}