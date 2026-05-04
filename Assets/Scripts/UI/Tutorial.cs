using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference helpRef;

    [Header("UI")]
    [SerializeField] private GameObject tutoObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        helpRef.action.started += TutoReplay;
        helpRef.action.canceled += TutoReplay;
    }

    public void TurnOffTuto()
    {
        Time.timeScale = 1f;
        tutoObject.SetActive(false);
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
