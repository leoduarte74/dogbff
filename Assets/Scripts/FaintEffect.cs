using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FaintEffect : MonoBehaviour
{
    // --(Campos existentes)--
    public Image blinkPanel;
    public float blinkSpeed = 0.2f;
    public int blinkCount = 3;

    public AudioSource cinematicAudioSource;
    public AudioClip carApproachSound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip carDriveAwaySound;

    // --(Tu nombre de escena es Scene2 segun la consola)--
    public string nextSceneName = "Scene2";

    // --NUEVO: TIEMPO DE ESPERA FINAL--
    // --Aumenta este valor en el Inspector para que dure más el sonido del auto yéndose--
    public float finalFadeOutWait = 6.0f;

    // --CAMBIO: Convertido a un Array (lista) para aceptar MÚLTIPLES sonidos--
    public AudioSource[] backgroundMusicSources;

    public float musicTargetVolume = 0.1f;
    public float musicFadeDuration = 1.5f;


    public void StartBlinking()
    {
        // --(Esta línea la borramos en el paso anterior, está bien)--
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        // --(Bucle de parpadeo sin cambios)--
        for (int i = 0; i < blinkCount; i++)
        {
            yield return StartCoroutine(Fade(1f, blinkSpeed));
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(Fade(0f, blinkSpeed));
            yield return new WaitForSeconds(0.2f);
        }

        yield return StartCoroutine(Fade(1f, 0.5f));

        // --Iniciar el fundido de la música (ahora afecta a todos los sonidos en el array)--
        if (backgroundMusicSources.Length > 0)
        {
            StartCoroutine(FadeOutMusic());
        }

        StartCoroutine(RescueSequence());
    }

    IEnumerator RescueSequence()
    {
        yield return new WaitForSeconds(2.0f);
        cinematicAudioSource.PlayOneShot(carApproachSound);
        yield return new WaitForSeconds(3.0f);
        cinematicAudioSource.PlayOneShot(doorOpenSound);
        yield return new WaitForSeconds(2.0f);
        cinematicAudioSource.PlayOneShot(doorCloseSound);
        yield return new WaitForSeconds(1.0f);
        cinematicAudioSource.PlayOneShot(carDriveAwaySound);

        // --CAMBIO: Usando la nueva variable de tiempo de espera--
        yield return new WaitForSeconds(finalFadeOutWait);

        SceneManager.LoadScene(nextSceneName);
    }

    // --CAMBIO: Corrutina actualizada para manejar un Array de AudioSources--
    IEnumerator FadeOutMusic()
    {
        // --Almacena todos los volúmenes iniciales--
        float[] startVolumes = new float[backgroundMusicSources.Length];
        for (int i = 0; i < backgroundMusicSources.Length; i++)
        {
            startVolumes[i] = backgroundMusicSources[i].volume;
        }

        float timer = 0f;

        while (timer < musicFadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / musicFadeDuration;

            // --Baja el volumen de CADA audio source en la lista--
            for (int i = 0; i < backgroundMusicSources.Length; i++)
            {
                backgroundMusicSources[i].volume = Mathf.Lerp(startVolumes[i], musicTargetVolume, progress);
            }
            yield return null;
        }

        // --Asegura que todos queden en el volumen objetivo--
        for (int i = 0; i < backgroundMusicSources.Length; i++)
        {
            backgroundMusicSources[i].volume = musicTargetVolume;
        }
    }

    // --(Tu función Fade existente)--
    IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = blinkPanel.color.a;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            blinkPanel.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }

        blinkPanel.color = new Color(0, 0, 0, targetAlpha);
    }
}