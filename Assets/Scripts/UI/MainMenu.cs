using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void PlayGame()
    {
        if (PlayerPrefs.HasKey("savedScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("savedScene"));
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }    
}
