using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Services.Authentication;

public class MainMenu : Initialisation
{
    [Header("UI Elements")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button scene1;
    [SerializeField] private Button scene2;
    [SerializeField] private Button scene3;
    [SerializeField] private Button continueButton;
    [SerializeField] private Text nameText;

    [Header("Objects")]
    [SerializeField] private GameObject panelID;
    private int progress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        volumeSlider.value = PlayerPrefs.GetFloat("volume");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (PlayerPrefs.HasKey("progress") == false)
            {
                scene3.interactable = false;
                scene2.interactable = false;
                scene1.interactable = false;
                continueButton.interactable = false;
            }

            if (PlayerPrefs.HasKey("savedScene") == false)
            {
                continueButton.interactable = false;
            }

            else
            {
               progress = PlayerPrefs.GetInt("progress");

               if (progress < 4)
               {
                   scene3.interactable = false;

                   if (progress < 3)
                   {
                       scene2.interactable = false;

                       if (progress < 2)
                       {
                            scene1.interactable = false;
                       }
                   }
               } 
            }   
        }
    }

    void Update()
    {
        if (panelID.activeSelf == false)
        {
            UpdatePlayerNameUI();
        } 
    }

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        base.Initialize();
    }

    public override void OnEnable()
    {
        UpdatePlayerNameUI();
        base.OnEnable();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("savedScene"));
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteKey("savedScene");
        PlayerPrefs.DeleteKey("viewedTutos");
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void SelectCombat(int sceneN)
    {
        SceneManager.LoadScene(sceneN);
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioListener.volume = PlayerPrefs.GetFloat("volume")/2;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    } 

    public void SignOut()
    {
        PlayerID.Instance.SignOut();
        PlayerPrefs.DeleteKey("playerUserName");
        nameText.text = "";
    }

    private void UpdatePlayerNameUI()
    {
        nameText.text = AuthenticationService.Instance.PlayerInfo.Username;
    }  
}
