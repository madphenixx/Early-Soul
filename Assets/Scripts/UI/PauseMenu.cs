using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference pauseRef;

    [Header("UI Elements")]
    [SerializeField] private Slider volumeSlider;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private GameObject optionsMenuObject;

    public static bool isPaused = false;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");

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
                Time.timeScale = 0f;
                //Cursor.lockState = CursorLockMode.Locked;
            }

            if (isPaused == false)
            {
                Time.timeScale = 1f;
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

    public void AdminReset()
    {
        Debug.Log("Reset!");
        Time.timeScale = 1.0f;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("volume", 5);
        Debug.Log(PlayerPrefs.GetFloat("volume"));
    }
}
