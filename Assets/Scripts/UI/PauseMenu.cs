using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseRef;

    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private GameObject optionsMenuObject;
    [SerializeField] private Slider volumeSlider;

    public static bool isPaused = false; // Permet de savoir si le jeu est en pause ou non.

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");

        pauseMenuObject = GameObject.Find("PauseMenu");
        optionsMenuObject = GameObject.Find("OptionsMenu");

        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);

        pauseRef.action.started += PauseGame;
        pauseRef.action.canceled += PauseGame;
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log(pauseMenuObject);
            Debug.Log(optionsMenuObject);
            isPaused = !isPaused;

            if (isPaused)
            {
                pauseMenuObject.SetActive(true);
                optionsMenuObject.SetActive(false);
                Time.timeScale = 0f; // Le temps s'arrete
            }

            if (isPaused == false)
            {
                Time.timeScale = 1f; // Le temps reprend
                pauseMenuObject.SetActive(false);
                optionsMenuObject.SetActive(false);
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitMenu()
    {
        Debug.Log("QUIT!");
        Time.timeScale = 1.0f;
        Application.Quit();
    }

    public void SetVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
