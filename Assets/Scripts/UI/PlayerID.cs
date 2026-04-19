using UnityEngine;
using System;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class PlayerID : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject playerIdPanel;

    private string enteredID;

    // private bool eventsInitialized = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.DeleteKey("playerID");

        if (PlayerPrefs.HasKey("playerID") == false)
        {
            playerIdPanel.SetActive(true);
        }
    }

    public void ConfirmID()
    {
        // PlayerPrefs.SetString("playerID", enteredID);
        playerIdPanel.SetActive(false);
        StartClientService();
    }

    public void EnterID(string inputID)
    {
        enteredID = inputID;
    }

    // private void SetUpEvents()
    // {
    //     eventsInitialized = true;
    //     AuthenticationService.Instance.SignedIn += () =>
    //     {
            
    //     };

    //     AuthenticationService.Instance.SignedOut += () =>
    //     {
            
    //     };

    //     AuthenticationService.Instance.Expired += () =>
    //     {
            
    //     };
    // }

    public async void StartClientService()
    {
        try
        {
           if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                var options = new InitializationOptions();
                options.SetProfile(PlayerPrefs.GetString("defaultPlayer"));
                await UnityServices.InitializeAsync();
                await AuthenticationService.Instance.UpdatePlayerNameAsync(PlayerPrefs.GetString("playerID"));
            } 

            // if (!eventsInitialized)
            // {
            //     SetUpEvents();
            // }

            // if (AuthenticationService.Instance.SessionTokenExists)
            // {
            //     SignInAnonymouslyAsync();
            // }

            else
            {
                
            }
        }

        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }
    }

    // public async void SignInAnonymouslyAsync()
    // {
    //     try
    //     {
    //         await AuthenticationService.Instance.SignInAnonymouslyAsync();
    //     }

    //     catch (AuthenticationException exception)
    //     {
    //         Debug.Log(exception.Message);
    //     }

    //     catch (RequestFailedException exception)
    //     {
    //         Debug.Log(exception.Message);
    //     }
    // }
}
