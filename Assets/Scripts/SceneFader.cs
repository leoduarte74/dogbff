using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    private Image fadeImage;
    public float fadeDuration = 1.0f;

    void Start()
    {
        fadeImage = GetComponent<Image>();
        // Empezar el fundido A TRANSPARENTE inmediatamente.
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = fadeDuration;
        // La variable de color (Alpha) en Start() debe ser 1 (Negro total)
        fadeImage.color = new Color(0, 0, 0, 1);

        // Fundir de negro total (1) a transparente (0)
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            // El color permanece negro, solo el canal Alpha cambia.
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Asegurar que es completamente transparente al final
        fadeImage.color = new Color(0, 0, 0, 0);
        gameObject.SetActive(false);
    }
}