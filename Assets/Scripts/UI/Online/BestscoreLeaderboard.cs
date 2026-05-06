using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Runtime.InteropServices;

public class BestscoreLeaderboard : Initialisation
{
    [Header("UI Elements")]
    [SerializeField] private RectTransform playersObject;
    public Text pageText;
    [SerializeField] private Button addScore;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;


    [Header("Prefabs")]
    [SerializeField] private LeaderboardItem playerItemPrefab;
    [DataMember(Name = "playerName", IsRequired = true, EmitDefaultValue = true)]
    public string PlayerName { get; }
    private string sceneID;

    [SerializeField] private int sceneNum;

    [Header("Settings")]
    [SerializeField] private int playersPerPage = 8;

    private int currentPage = 1;
    private int totalPages = 0;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        ClearPlayerList();

        base.Initialize();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (!UnityServicesInitializer.IsReady)
        {
            return;
        }

        LoadPlayers(1);
       
        pageText.text = "-";
        nextButton.interactable = false;
        prevButton.interactable = false;

        ClearPlayerList();
        currentPage = 1;
        totalPages = 0;
    }

    async void Start()
    {
        sceneID = gameObject.name.ToLower();

        while (!UnityServicesInitializer.IsReady)
        {
            await Task.Delay(100);
        }

        await AddScoreAsync(PlayerPrefs.GetInt("bestScore1") + sceneNum);

        LoadPlayers(1);

        try
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            var scores = await LeaderboardsService.Instance.GetScoresAsync(sceneID);
        }

        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }

    public void TestAddScore()
    {
        _ = AddScoreAsync(10);
    }
    
    public async Task AddScoreAsync(int score)
    {
        while (!UnityServicesInitializer.IsReady)
        {
            await Task.Delay(100);
        }

        // addScore.interactable = false;

        try
        {
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(sceneID, score);
            LoadPlayers(currentPage);
        }

        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }

        // finally
        // {
        //     addScore.interactable = true;
        // }
    }

    private async void LoadPlayers(int page)
    {
        nextButton.interactable = false;
        prevButton.interactable = false;

        try
        {
            GetScoresOptions options = new GetScoresOptions();
            options.Offset = (page - 1) * playersPerPage;
            options.Limit = playersPerPage;
            var scores = await LeaderboardsService.Instance.GetScoresAsync(sceneID, options);
            ClearPlayerList();

            for (int i = 0; i < scores.Results.Count; i++)
            {
                LeaderboardItem item = Instantiate(playerItemPrefab, playersObject);
                item.InitializeLeaderBoard(scores.Results[i]);
            }

            totalPages = Mathf.CeilToInt(scores.Total / scores.Limit);
            currentPage = page;
        }

        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }

        pageText.text = currentPage.ToString() + " / " + totalPages.ToString();
        nextButton.interactable = currentPage < totalPages && totalPages > 1;
        prevButton.interactable = currentPage > 1 && totalPages > 1;
    }

    public void NextPage()
    {
        if (currentPage + 1 > totalPages)
        {
            LoadPlayers(1);
        }

        else
        {
            LoadPlayers(currentPage + 1);
        }
    }

    public void PreviousPage()
    {
        if (currentPage - 1 <= 0)
        {
            LoadPlayers(totalPages);
        }

        else
        {
            LoadPlayers(currentPage -1);
        }
    }

    public void ClearPlayerList()
    {
        LeaderboardItem[] items = playersObject.GetComponentsInChildren<LeaderboardItem>();

        if (items != null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Destroy(items[i].gameObject);
            }
        }
    }
}
