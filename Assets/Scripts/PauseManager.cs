using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio; // --Needed for the AudioMixer--

public class PauseManager : MonoBehaviour
{
    // --Singleton to make it persistent--
    public static PauseManager instance;

    // --Assign these in the Inspector--
    // --Volvimos a GameObject, es más simple--
    public GameObject pauseMenuCanvas;
    public AudioMixer mainMixer;

    public GameObject pauseMenuPanel; // --Panel with Resume, Options, Exit--
    public GameObject optionsPanel;   // --Panel with Slider and Back button--

    private bool isPaused = false;
    private float defaultVolume = 0f;

    void Awake()
    {
        // --Singleton Pattern--
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

        // --Get default volume--
        mainMixer.GetFloat("MasterVolume", out defaultVolume);
    }

    // --Ya no necesitamos OnEnable, OnDisable, OnSceneLoaded--

    void Update()
    {
        // --Listen for the Escape key--
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (optionsPanel.activeSelf)
                {
                    CloseOptions();
                }
                else
                {
                    Resume();
                }
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
        pauseMenuCanvas.SetActive(true);
        pauseMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);

        Time.timeScale = 0f;
        mainMixer.SetFloat("MasterVolume", -20f);
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        mainMixer.SetFloat("MasterVolume", defaultVolume);
    }

    public void OpenOptions()
    {
        pauseMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        pauseMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void ExitToMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}