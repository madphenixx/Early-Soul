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
        //Debug.Log("RAAAAAAAAAAAAAAAH");
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        isPaused = false;

        pauseMenuObject = GameObject.Find("PauseMenu");
        optionsMenuObject = GameObject.Find("OptionsMenu");

        Debug.Log(pauseMenuObject);
        Debug.Log(optionsMenuObject);

        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);

        Debug.Log(pauseMenuObject);
        Debug.Log(optionsMenuObject);

        pauseRef.action.started += PauseGame;
        pauseRef.action.canceled += PauseGame;

        Debug.Log(pauseRef);
        Debug.Log(isPaused);
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isPaused = !isPaused;
            Debug.Log(isPaused);
            Debug.Log(pauseMenuObject);
            Debug.Log(optionsMenuObject);

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
        Application.Quit();
    }

    public void SetVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
