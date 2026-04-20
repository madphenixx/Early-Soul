using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System;

public class Authentication : Initialisation
{
    [Header("Objects")]
    [SerializeField] private ErrorMenu errorMenu;

    private string usernameInput;
    private string passwordInput;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        base.Initialize();
    }

    public void AnonymousSignIn()
    {
        PlayerID.Instance.SignInAnonymouslyAsync();
    }

    public void ConfirmID(string inputID)
    {
        usernameInput = inputID;
        // playerIdPanel.SetActive(false);
    }

    public void ConfirmPW(string inputPW)
    {
        passwordInput = inputPW;
        // playerIdPanel.SetActive(false);
    }

    public void SignIn()
    {
        string user = usernameInput.Trim();
        string pass = passwordInput.Trim();
        if (string.IsNullOrEmpty(user) == false && string.IsNullOrEmpty(pass) == false)
        {
            PlayerID.Instance.SignInWithUsernameAndPasswordAsync(user, pass);
        }
    }

    public void SignUp()
    {
        string user = usernameInput.Trim();
        string pass = passwordInput.Trim();
        if (string.IsNullOrEmpty(user) == false && string.IsNullOrEmpty(pass) == false)
        {
            if (IsPasswordValid(pass))
            {
                PlayerID.Instance.SignUpWithUsernameAndPasswordAsync(user, pass);
            }

            else
            {
                errorMenu.OpenError(ErrorMenu.Action.None, "Password does not match requirements. Insert at least 1 uppercase, 1 lowercase, 1 digit and 1 symbol. With minimum 8 and a maximum of 30 characters.", "OK");
                errorMenu.gameObject.SetActive(true);
            }
        }
    }


    private bool IsPasswordValid(string password)
    {
        if (password.Length < 8 || password.Length > 30)
        {
            return false;
        }

        bool hasUppercase = false;
        bool hasLowercase = false;
        bool hasDigit = false;
        bool hasSymbol = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c))
            {
                hasUppercase = true;
            }

            else if (char.IsLower(c))
            {
                hasLowercase = true;
            }

            else if (char.IsDigit(c))
            {
                hasDigit = true;
            }

            else if (!char.IsLetterOrDigit(c))
            {
                hasSymbol = true;
            }
        }

        return hasUppercase && hasLowercase && hasDigit && hasSymbol;
    }
}
