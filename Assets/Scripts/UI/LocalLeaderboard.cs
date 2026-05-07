using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LocalLeaderboard : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private RectTransform playersObject;

    [Header("Prefabs")]
    [SerializeField] private LocalScoreEl playerItemPrefab;

    [SerializeField] private List<int> bestScores = new List<int>();
    [SerializeField] private List<string> dateScores = new List<string>();

    [SerializeField] private int sceneNum;

    void Start()
    {
        bestScores.Add(PlayerPrefs.GetInt("bestScore1" + sceneNum.ToString()));
        bestScores.Add(PlayerPrefs.GetInt("bestScore2" + sceneNum.ToString()));
        bestScores.Add(PlayerPrefs.GetInt("bestScore3" + sceneNum.ToString()));
        bestScores.Add(PlayerPrefs.GetInt("bestScore4" + sceneNum.ToString()));
        bestScores.Add(PlayerPrefs.GetInt("bestScore5" + sceneNum.ToString()));


        dateScores.Add(PlayerPrefs.GetString("dateScore1" + sceneNum.ToString()));
        dateScores.Add(PlayerPrefs.GetString("dateScore2" + sceneNum.ToString()));
        dateScores.Add(PlayerPrefs.GetString("dateScore3" + sceneNum.ToString()));
        dateScores.Add(PlayerPrefs.GetString("dateScore4" + sceneNum.ToString()));
        dateScores.Add(PlayerPrefs.GetString("dateScore5" + sceneNum.ToString()));
        LoadPlayers();
    }

    private void LoadPlayers()
    {
        for (int i = 0; i < bestScores.Count; i++)
        {
            LocalScoreEl item = Instantiate(playerItemPrefab, playersObject);
            item.score = bestScores[i];
            item.rank = i;
            item.date = dateScores[i];

            item.InitializeLeaderBoard(item);
        }
    }
}
