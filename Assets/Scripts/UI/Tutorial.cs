using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference helpRef;

    [Header("UI")]
    [SerializeField] private GameObject tutoObject;

    private bool isActive;

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
    }

    void TutoReplay(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            isActive = !isActive;

            if (isActive == true)
            {
                tutoObject.SetActive(false);
                Time.timeScale = 1f;
            }

            if (isActive == false)
            {
                tutoObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    void OnDisable()
    {
        helpRef.action.started -= TutoReplay;
        helpRef.action.canceled -= TutoReplay;
    }
}
