using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    private Text scoreText;
    private Text bestScoreText;

    [Header("Debug: scores")]
    [SerializeField] private int score;
    [SerializeField] private int scene;
    [SerializeField] private List<int> bestScores = new List<int>();

    void Start()
    {
        scene = PlayerPrefs.GetInt("savedScene");
        bestScoreText = GameObject.Find("BestScore").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        bestScores.Add(PlayerPrefs.GetInt("bestScore1" + scene.ToString()));
        bestScores.Add(PlayerPrefs.GetInt("bestScore2" + scene.ToString()));
        bestScores.Add(PlayerPrefs.GetInt("bestScore3" + scene.ToString()));
        bestScores.Add(PlayerPrefs.GetInt("bestScore4" + scene.ToString()));
        bestScores.Add(PlayerPrefs.GetInt("bestScore5" + scene.ToString()));

        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore1" + scene.ToString()).ToString();

        if (SceneManager.GetActiveScene().name == "DeathScreen" || SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            score = PlayerPrefs.GetInt("currentScore");
            scoreText.text = "Score: " + score.ToString();
            bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore1" + scene.ToString()).ToString();
        }

        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore1" + scene.ToString()).ToString();
    }

    void Update()
    {
        score = PlayerPrefs.GetInt("currentScore");

        // if (score > bestScores[4])
        // {
        //     bestScores.Add(score);
        //     bestScores.Sort();
        //     bestScores.Reverse();
        //     bestScores.RemoveAt(5);

        //     PlayerPrefs.SetInt("bestScore1" + scene.ToString(), bestScores[0]);
        //     PlayerPrefs.SetInt("bestScore2" + scene.ToString(), bestScores[1]);
        //     PlayerPrefs.SetInt("bestScore3" + scene.ToString(), bestScores[2]);
        //     PlayerPrefs.SetInt("bestScore4" + scene.ToString(), bestScores[3]);
        //     PlayerPrefs.SetInt("bestScore5" + scene.ToString(), bestScores[4]);
        // }
    }

    void OnDisable()
    {
        if (score > bestScores[4]  && SceneManager.GetActiveScene().name != "DeathScreen" && SceneManager.GetActiveScene().name != "VictoryScreen")
        {
            bestScores.Add(score);
            bestScores.Sort();
            bestScores.Reverse();
            bestScores.RemoveAt(5);

            PlayerPrefs.SetInt("bestScore1" + scene.ToString(), bestScores[0]);
            PlayerPrefs.SetInt("bestScore2" + scene.ToString(), bestScores[1]);
            PlayerPrefs.SetInt("bestScore3" + scene.ToString(), bestScores[2]);
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), bestScores[3]);
            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), bestScores[4]);

            //el.date.ToString("dd-MM-yyyy ") + el.date.ToString("HH:mm")
        }
    }
}
