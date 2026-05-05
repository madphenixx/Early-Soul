using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;



public class UnityServicesInitializer : MonoBehaviour

{
    public static bool IsReady { get; private set; } = false;
    public static UnityServicesInitializer Instance {get; private set;}


    async void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        if (IsReady) return;

        try
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)

                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            IsReady = true;

            Debug.Log("Unity Services initialisés");

        }

        catch (Exception e)

        {

            Debug.LogError("Erreur init Unity Services : " + e.Message);

        }

    }

}
