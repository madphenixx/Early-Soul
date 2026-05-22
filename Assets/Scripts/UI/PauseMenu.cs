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
    [SerializeField] private Slider SFXSlider;

    [Header("Sons")]
    [SerializeField] private AudioSource pausingSound;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private AudioSource sliderSound;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private GameObject optionsMenuObject;

    public static bool isPaused = false;

    private void Awake()
    {
        clickSound.Stop();
        sliderSound.Stop();
    }

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXvolume");

        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(false);

        pauseRef.action.started += PauseGame;
        pauseRef.action.canceled += PauseGame;
    }

    public void ClicSound()
    {
        clickSound.Play();
    }

    public void SlideSound()
    {
        sliderSound.Play();
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            pausingSound.Play();
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
        pausingSound.Play();
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
        //AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    public void SetVolumeSFX(float sliderValue)
    {
        PlayerPrefs.SetFloat("SFXvolume", sliderValue);
        //AudioListener.volume = PlayerPrefs.GetFloat("SFXvolume");
    }

    void OnDisable()
    {
        pauseRef.action.started -= PauseGame;
        pauseRef.action.canceled -= PauseGame;
    }
}
