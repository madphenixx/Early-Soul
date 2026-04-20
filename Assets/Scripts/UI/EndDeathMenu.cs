using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EndDeathMenu : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("savedScene"));
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void QuitMenu()
    {
        Debug.Log("QUIT!");
        Time.timeScale = 1.0f;
        Application.Quit();
    }

    public void MainMenuEnd()
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.DeleteKey("savedScene");
        SceneManager.LoadScene(0);
        //Cursor.lockState = CursorLockMode.Confined;
    }

    public void QuitEnd()
    {
        Debug.Log("QUIT!");
        Time.timeScale = 1.0f;
        PlayerPrefs.DeleteKey("savedScene");
        Application.Quit();
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
