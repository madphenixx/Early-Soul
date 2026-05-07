using UnityEngine;
using System;
using UnityEngine.UI;

public class LocalScoreEl : MonoBehaviour
{
    [Header("UI Elements")]
    public Text rankText;
    public Text dateText;
    public Text scoreText;

    public void InitializeLeaderBoard(LocalScoreClass el)
    {
        rankText.text = (el.rank + 1).ToString();
        dateText.text = el.date;
        scoreText.text = el.score.ToString();
    }
}

[System.Serializable]
public class LocalScoreClass
{
   [Header("Caracteristics")]
    public int rank;
    public string date;
    public int score;

    public LocalScoreClass(int Rank, string Date, int Score)
    {
        rank = Rank;
        date = Date;
        score = Score;
    }
}
