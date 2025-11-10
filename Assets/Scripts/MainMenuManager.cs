using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio; // --Needed for AudioMixer--

public class MainMenuManager : MonoBehaviour
{
    // --Assign these in the Inspector--
    public Image fadePanel;
    public float fadeDuration = 1.0f;
    public GameObject mainMenuPanel; // --Panel with Start, Exit--
    public GameObject optionsPanel;  // --Panel with Slider--
    public AudioMixer mainMixer;     // --Your MainMixer asset--

    // --Function assigned to the PLAY button--
    public void StartGame()
    {
        Debug.Log("Iniciando Transición...");
        // --Start the fade effect before loading the scene--
        StartCoroutine(FadeOutAndLoadScene("Scene1"));
    }

    // --Coroutine that handles the fade to black and then loads the scene--
    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // --1. Ensure the panel is visible and ready--
        if (fadePanel == null)
        {
            Debug.LogError("Fade Panel not assigned!");
            SceneManager.LoadScene(sceneName); // --Load without fade if it fails--
            yield break; // --Exit the coroutine--
        }

        fadePanel.gameObject.SetActive(true); // --Make sure the panel is active--

        // --2. Fade To Black (Fade Out - opacity from 0 to 1)--
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            // --Set the Alpha of the panel's color--
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null; // --Wait one frame--
        }

        // --3. Ensure it's fully black--
        fadePanel.color = new Color(0, 0, 0, 1);

        // --4. Load the scene--
        SceneManager.LoadScene(sceneName);
    }

    // --Function for the OPTIONS button--
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // --Function for the BACK button (from Options panel)--
    public void CloseOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    // --Function for the volume SLIDER--
    public void SetVolume(float volume)
    {
        // --This formula converts the slider (linear) to decibels (logarithmic)--
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del Juego...");
        Application.Quit();
    }
}