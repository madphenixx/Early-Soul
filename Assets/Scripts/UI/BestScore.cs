using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class BestScore : MonoBehaviour
{
    private Text scoreText;
    private Text bestScoreText;

    [Header("Debug: scores")]
    [SerializeField] private int score;
    [SerializeField] private int scene;
    [SerializeField] private List<LocalScoreEl> bestScores = new List<LocalScoreEl>();

    void Start()
    {
        scene = PlayerPrefs.GetInt("savedScene");
        bestScoreText = GameObject.Find("BestScore").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        bestScores.Add(new LocalScoreEl() { date = PlayerPrefs.GetString("dateScore1" + scene.ToString()), score = PlayerPrefs.GetInt("bestScore1" + scene.ToString()) } );
        bestScores.Add(new LocalScoreEl() { date = PlayerPrefs.GetString("dateScore2" + scene.ToString()), score = PlayerPrefs.GetInt("bestScore2" + scene.ToString()) });
        bestScores.Add(new LocalScoreEl() { date = PlayerPrefs.GetString("dateScore3" + scene.ToString()), score = PlayerPrefs.GetInt("bestScore3" + scene.ToString()) });
        bestScores.Add(new LocalScoreEl() { date = PlayerPrefs.GetString("dateScore4" + scene.ToString()), score = PlayerPrefs.GetInt("bestScore4" + scene.ToString()) });
        bestScores.Add(new LocalScoreEl() { date = PlayerPrefs.GetString("dateScore5" + scene.ToString()), score = PlayerPrefs.GetInt("bestScore5" + scene.ToString()) });

        //bestScores.Add(PlayerPrefs.GetInt("bestScore1" + scene.ToString()));
        //bestScores.Add(PlayerPrefs.GetInt("bestScore2" + scene.ToString()));
        //bestScores.Add(PlayerPrefs.GetInt("bestScore3" + scene.ToString()));
        //bestScores.Add(PlayerPrefs.GetInt("bestScore4" + scene.ToString()));
        //bestScores.Add(PlayerPrefs.GetInt("bestScore5" + scene.ToString()));

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
        if (score > bestScores[4].score  && SceneManager.GetActiveScene().name != "DeathScreen" && SceneManager.GetActiveScene().name != "VictoryScreen")
        {
            string date = DateTime.Now.ToString("dd-MM-yyyy ") + DateTime.Now.ToString("HH:mm");
            bestScores.Add(new LocalScoreEl() { date = date, score = score });
            bestScores.Sort();
            bestScores.Reverse();
            bestScores.RemoveAt(5);

            PlayerPrefs.SetInt("bestScore1" + scene.ToString(), bestScores[0].score);
            PlayerPrefs.SetInt("bestScore2" + scene.ToString(), bestScores[1].score);
            PlayerPrefs.SetInt("bestScore3" + scene.ToString(), bestScores[2].score);
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), bestScores[3].score);
            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), bestScores[4].score);

            PlayerPrefs.SetString("dateScore1" + scene.ToString(), bestScores[0].date);
            PlayerPrefs.SetString("dateScore2" + scene.ToString(), bestScores[1].date);
            PlayerPrefs.SetString("dateScore3" + scene.ToString(), bestScores[2].date);
            PlayerPrefs.SetString("dateScore4" + scene.ToString(), bestScores[3].date);
            PlayerPrefs.SetString("dateScore5" + scene.ToString(), bestScores[5].date);
        }
    }
}
