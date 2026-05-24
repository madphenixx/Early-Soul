using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Leaderboards.Models;

public class LeaderboardItem : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] public Text rankText;
    [SerializeField] public Text nameText;
    [SerializeField] public Text scoreText;

    private LeaderboardEntry player;

    public void InitializeLeaderBoard(LeaderboardEntry player)
    {
        this.player = player;
        rankText.text = (player.Rank + 1).ToString();
        nameText.text = player.PlayerName;
        scoreText.text = player.Score.ToString();
    }
}
