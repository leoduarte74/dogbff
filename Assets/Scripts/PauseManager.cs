using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio; // --Needed for the AudioMixer--

public class PauseManager : MonoBehaviour
{
    // --Singleton to make it persistent--
    public static PauseManager instance;

    // --Assign these in the Inspector--
    public GameObject pauseMenuCanvas; // --El Canvas que contiene los botones--
    public AudioMixer mainMixer;     // --El Audio Mixer--

    private bool isPaused = false;
    private float defaultVolume = 0f; // --To store the normal volume--

    void Awake()
    {
        // --Singleton Pattern (Ensures only one PauseManager exists)--
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // --Get the default volume (0dB is full volume)--
        mainMixer.GetFloat("MasterVolume", out defaultVolume);
    }

    void Update()
    {
        // --Listen for the Escape key--
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Pause()
    {
        isPaused = true;
        pauseMenuCanvas.SetActive(true); // --Show the pause UI--

        // --Pause the game simulation--
        Time.timeScale = 0f;

        // --Lower the volume--
        mainMixer.SetFloat("MasterVolume", -20f);
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false); // --Hide the pause UI--

        // --Resume the game simulation--
        Time.timeScale = 1f;

        // --Restore the default volume--
        mainMixer.SetFloat("MasterVolume", defaultVolume);
    }

    public void ExitToMainMenu()
    {
        // --CRUCIAL: Unpause before leaving the scene--
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}