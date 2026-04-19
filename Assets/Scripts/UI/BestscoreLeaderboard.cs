using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;

public class BestscoreLeaderboard : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private RectTransform playersObject;
    public Text pageText;
    [SerializeField] private Button addScore;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;


    [Header("Prefabs")]
    [SerializeField] private LeaderboardItem playerItemPrefab;

    [Header("Settings")]
    [SerializeField] private int playersPerPage = 8;

    [DataMember(Name = "playerName", IsRequired = true, EmitDefaultValue = true)]
    public string PlayerName { get; }

    private int currentPage = 1;
    private int totalPages = 0;

    void Awake()
    {
        ClearPlayerList();
    }

    public void TestAddScore()
    {
        AddScore(10);
    }

    private void OnEnable()
    {
        pageText.text = "-";
        nextButton.interactable = false;
        prevButton.interactable = false;

        ClearPlayerList();
        currentPage = 1;
        totalPages = 0;
        LoadPlayers(1);
    }
    
    public async void AddScore(int score)
    {
        addScore.interactable = false;

        try
        {
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync("seraph", score);
            LoadPlayers(currentPage);
        }

        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }

        addScore.interactable = true;
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
            var scores = await LeaderboardsService.Instance.GetScoresAsync("seraph", options);
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
