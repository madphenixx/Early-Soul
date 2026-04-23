using UnityEngine;
using System;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class PlayerID : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject playerIdPanel;

    [SerializeField] private ErrorMenu errorMenu;
    private static PlayerID singleton = null;
    public static PlayerID Instance
    {
        get
        {
            if (singleton != null) return singleton;
            singleton = FindAnyObjectByType<PlayerID>();

            if (singleton == null)
            {
                singleton.Initialize();
            }

            return singleton; 
        }
    }

    private bool initialized = false;
    private bool eventsInitialized = false;

    

    public void  Awake()
    {
        Application.runInBackground = true;
        StartClientService();
    }

    private async void Initialize()
    {
        if (initialized) { return; }
        initialized = true;

        await UnityServices.InitializeAsync();
    }

    private void SetUpEvents()
    {
        eventsInitialized = true;
        AuthenticationService.Instance.SignedIn += () =>
        {
            SignInConfirmAsync();
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            playerIdPanel.SetActive(true);
        };

        AuthenticationService.Instance.Expired += () =>
        {
            SignInAnonymouslyAsync();
        };
    }

    public async void StartClientService()
    {
        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                var options = new InitializationOptions();
                options.SetProfile("default_profile");
                await UnityServices.InitializeAsync();
            } 

            if (!eventsInitialized)
            {
                SetUpEvents();
            }

            if (AuthenticationService.Instance.SessionTokenExists)
            {
                SignInAnonymouslyAsync();
            }

            else
            {
                playerIdPanel.SetActive(true);
            }
        }

        catch (Exception)
        {
            errorMenu.OpenError(ErrorMenu.Action.StartService, "Failed to connect to the network.", "Retry");
            errorMenu.gameObject.SetActive(true);
        }
    }

    public async void SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        catch (AuthenticationException)
        {
            errorMenu.OpenError(ErrorMenu.Action.OpenAuthMenu, "Failed to sign in.", "OK");
            errorMenu.gameObject.SetActive(true);
        }

        catch (RequestFailedException)
        {
            errorMenu.OpenError(ErrorMenu.Action.SignIn, "Failed to connect to the network.", "Retry");
            errorMenu.gameObject.SetActive(true);
        }
    }

    public async void SignInWithUsernameAndPasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            PlayerPrefs.SetString("playerUserName", username);
        }

        catch (AuthenticationException exception)
        {
            errorMenu.OpenError(ErrorMenu.Action.OpenAuthMenu, exception.Message, "OK");
            errorMenu.gameObject.SetActive(true);
        }

        catch (RequestFailedException)
        {
            errorMenu.OpenError(ErrorMenu.Action.SignIn, "Incorrect Username or Password", "Retry");
            errorMenu.gameObject.SetActive(true);
        }
    }

    public async void SignUpWithUsernameAndPasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            PlayerPrefs.SetString("playerUserName", username);
        }

        catch (AuthenticationException )
        {
            errorMenu.OpenError(ErrorMenu.Action.OpenAuthMenu, "There is already an user with this username", "OK");
            errorMenu.gameObject.SetActive(true);
        }

        catch (RequestFailedException)
        {
            errorMenu.OpenError(ErrorMenu.Action.SignIn, "Failed to connect to the network.", "OK");
            errorMenu.gameObject.SetActive(true);
        }
    }

    public void SignOut()
    {
        AuthenticationService.Instance.SignOut();
        playerIdPanel.SetActive(true);
    }

    private void SetupEvents()
    {
        eventsInitialized = true;
        AuthenticationService.Instance.SignedIn += () =>
        {
            SignInConfirmAsync();
        };

        AuthenticationService.Instance.SignedOut += () =>
        {
            playerIdPanel.SetActive(true);
        };
        
        AuthenticationService.Instance.Expired += () =>
        {
            SignInAnonymouslyAsync();
        };
    }

    private async void SignInConfirmAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(AuthenticationService.Instance.PlayerName))
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync("Player");
            }

            playerIdPanel.SetActive(false);
        }

        catch
        {
            errorMenu.gameObject.SetActive(true);
        }
    }
}
