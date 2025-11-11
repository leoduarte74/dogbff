using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    // --Assign these in the Inspector--
    public Image fadePanel;
    public float fadeDuration = 1.0f;
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public AudioMixer mainMixer;

    // --NUEVO: Esta función se ejecuta CADA VEZ que se carga la escena--
    void Start()
    {
        // --Asegurarse de que el panel esté activo y negro al inicio--
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 1); // --Empieza negro (Alpha 1)--

        // --Iniciar el fundido de entrada--
        StartCoroutine(FadeIn());
    }

    // --NUEVA CORRUTINA: Para el fundido de entrada (negro a transparente)--
    IEnumerator FadeIn()
    {
        // --Asegurarse de que bloquea los clics al inicio--
        fadePanel.raycastTarget = true;

        float timer = fadeDuration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // --Al terminar, asegurarse de que esté transparente--
        fadePanel.color = new Color(0, 0, 0, 0);
        // --CRUCIAL: Desactivar Raycast para poder cliquear los botones--
        fadePanel.raycastTarget = false;
    }

    // --Función asignada al botón PLAY (Fade Out)--
    public void StartGame()
    {
        Debug.Log("Iniciando Transición...");
        // --Asegurarse de que el panel esté activo y bloquee clics--
        fadePanel.gameObject.SetActive(true);
        fadePanel.raycastTarget = true;

        StartCoroutine(FadeOutAndLoadScene("Scene1"));
    }

    // --Corrutina que maneja el fundido a negro (sin cambios)--
    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        if (fadePanel == null)
        {
            SceneManager.LoadScene(sceneName);
            yield break;
        }

        float timer = 0f;
        // --Este bucle lleva el Alpha de 0 (transparente) a 1 (negro)--
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadePanel.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene(sceneName);
    }

    // --(El resto de tus funciones: OpenOptions, CloseOptions, SetVolume, ExitGame)--
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void CloseOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void ExitGame()
    {
        Debug.Log("Saliendo del Juego...");
        Application.Quit();
    }
}