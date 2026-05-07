using UnityEngine;
using System;
using UnityEngine.UI;

public class LocalScoreEl : MonoBehaviour
{
    [Header("UI Elements")]
    public Text rankText;
    public Text dateText;
    public Text scoreText;

    [Header("Caracteristics")]
    public int rank ;
    public string date;
    public int score;

    public void InitializeLeaderBoard(LocalScoreEl el)
    {
        rankText.text = (el.rank + 1).ToString();
        dateText.text = el.date;
        scoreText.text = el.score.ToString();
    }
}
