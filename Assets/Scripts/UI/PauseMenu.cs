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
        if (!ctx.canceled)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                pauseMenuObject.SetActive(true);
                optionsMenuObject.SetActive(false);
                Time.timeScale = 0f; // Le temps s'arrete
                //Cursor.lockState = CursorLockMode.Locked;
            }

            if (isPaused == false)
            {
                Time.timeScale = 1f; // Le temps reprend
                pauseMenuObject.SetActive(false);
                optionsMenuObject.SetActive(false);
                //Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        SceneManager.LoadScene(0);
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void MainMenuEnd()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        PlayerPrefs.DeleteKey("savedScene");
        SceneManager.LoadScene(0);
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("savedScene"));
    }

    public void QuitMenu()
    {
        Debug.Log("QUIT!");
        Time.timeScale = 1.0f;
        Application.Quit();
    }

    public void QuitEnd()
    {
        Debug.Log("QUIT!");
        Time.timeScale = 1.0f;
        PlayerPrefs.DeleteKey("savedScene");
        Application.Quit();
    }

    public void SetVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    void OnDisable()
    {
        pauseRef.action.started -= PauseGame;
        pauseRef.action.canceled -= PauseGame;
    }
}
