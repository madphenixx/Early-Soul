using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public InputActionReference helpRef;

    public GameObject tutoObject;
    
    public bool tutoNext;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        helpRef.action.started += TutoReplay;
        helpRef.action.canceled += TutoReplay;

        if (PlayerPrefs.GetInt("viewedTutos") >= SceneManager.GetActiveScene().buildIndex)
        {
            tutoObject.SetActive(false);
        }

        else
        {
            tutoObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void TurnOffTuto()
    {
        Time.timeScale = 1f;
        tutoObject.SetActive(false);
        tutoNext = false;
        PlayerPrefs.SetInt("viewedTutos", SceneManager.GetActiveScene().buildIndex);
    }

    void TutoReplay(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            tutoObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnDisable()
    {
        helpRef.action.started -= TutoReplay;
        helpRef.action.canceled -= TutoReplay;
    }
}
