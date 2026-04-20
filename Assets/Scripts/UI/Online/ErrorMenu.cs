using UnityEngine;
using UnityEngine.UI;

public class ErrorMenu : Initialisation
{
    [Header("UI Elements")]
    [SerializeField] private Text errorText;
    [SerializeField] private Text buttonText;

    [Header("Objects")]
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private GameObject authPanel;

    public enum Action
    {
        None = 0, StartService = 1, SignIn = 2, OpenAuthMenu = 3
    }

    private Action action = Action.None;


    void Start()
    {
        errorPanel = gameObject;
    }

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        base.Initialize();
    }

    public void OpenError(Action action, string error, string button)
    {
        // errorPanel.SetActive(true);
        this.action = action;

        if (string.IsNullOrEmpty(error) == false)
        {
            errorText.text = error;
        }

        if (string.IsNullOrEmpty(button) == false)
        {
            buttonText.text = button;
        }
    }

    public void ButtonAction()
    {
        switch (action)
        {
            case Action.StartService:
                PlayerID.Instance.StartClientService();
                break;
            case Action.SignIn:
                PlayerID.Instance.SignInAnonymouslyAsync();
                break;
            case Action.OpenAuthMenu:
                authPanel.SetActive(true);
                break;
        }

        errorPanel.SetActive(false);
    }
}
