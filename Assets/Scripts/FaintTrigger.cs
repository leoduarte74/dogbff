using UnityEngine;

public class FaintTrigger : MonoBehaviour
{
    // --Assign these in the Inspector--
    public DialogueManager dialogueManager;
    public FaintEffect faintEffect;

    private bool hasTriggered = false; // --Ensures it only runs once--

    private void OnTriggerEnter2D(Collider2D other)
    {
        // --Check if it's the Puppy and if the event hasn't run yet--
        if (other.CompareTag("Puppy") && !hasTriggered)
        {
            hasTriggered = true;

            // --Call the new function in DialogueManager--
            string faintText = "*cof cof* I'm not feeling well... I'm gonna rest a little bit...";

            // --Passes the text AND the function to call when done (StartBlinking)--
            dialogueManager.ShowDialogueWithCallback(faintText, faintEffect.StartBlinking);
        }
    }
}