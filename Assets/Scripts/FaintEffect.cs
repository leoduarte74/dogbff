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

    public string nextSceneName = "ApartmentScene";

    // --NUEVOS CAMPOS PARA LA MÚSICA--
    // --Arrastra aquí el AudioSource que tiene la música de fondo--
    public AudioSource backgroundMusicSource;
    // --A qué volumen quieres bajar la música (0.1 = 10%)--
    public float musicTargetVolume = 0.1f;
    // --En cuánto tiempo quieres que baje el volumen--
    public float musicFadeDuration = 1.5f;


    public void StartBlinking()
    {
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            yield return StartCoroutine(Fade(1f, blinkSpeed));
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(Fade(0f, blinkSpeed));
            yield return new WaitForSeconds(0.2f);
        }

        yield return StartCoroutine(Fade(1f, 0.5f));

        // --NUEVO: Iniciar el fundido de la música--
        if (backgroundMusicSource != null)
        {
            StartCoroutine(FadeOutMusic());
        }

        StartCoroutine(RescueSequence());
    }

    IEnumerator RescueSequence()
    {
        // --(Tu secuencia de audio de rescate existente)--
        yield return new WaitForSeconds(2.0f);
        cinematicAudioSource.PlayOneShot(carApproachSound);
        yield return new WaitForSeconds(3.0f);
        cinematicAudioSource.PlayOneShot(doorOpenSound);
        yield return new WaitForSeconds(2.0f);
        cinematicAudioSource.PlayOneShot(doorCloseSound);
        yield return new WaitForSeconds(1.0f);
        cinematicAudioSource.PlayOneShot(carDriveAwaySound);
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(nextSceneName);
    }

    // --NUEVA CORRUTINA: Para bajar el volumen de la música--
    IEnumerator FadeOutMusic()
    {
        float startVolume = backgroundMusicSource.volume;
        float timer = 0f;

        while (timer < musicFadeDuration)
        {
            timer += Time.deltaTime;
            // --Interpola suavemente el volumen desde el inicio hasta el objetivo--
            backgroundMusicSource.volume = Mathf.Lerp(startVolume, musicTargetVolume, timer / musicFadeDuration);
            yield return null;
        }

        // --Asegura que el volumen quede en el valor exacto--
        backgroundMusicSource.volume = musicTargetVolume;
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