using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private List<LocalScoreClass> bestScores = new List<LocalScoreClass>();

    void Start()
    {
        scene = PlayerPrefs.GetInt("savedScene");
        bestScoreText = GameObject.Find("BestScore").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore1" + scene.ToString()), PlayerPrefs.GetInt("bestScore1" + scene.ToString())));
        bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore2" + scene.ToString()), PlayerPrefs.GetInt("bestScore2" + scene.ToString())));
        bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore3" + scene.ToString()), PlayerPrefs.GetInt("bestScore3" + scene.ToString())));
        bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore4" + scene.ToString()), PlayerPrefs.GetInt("bestScore4" + scene.ToString())));
        bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore5" + scene.ToString()), PlayerPrefs.GetInt("bestScore5" + scene.ToString())));

        //bestScores.Add(PlayerPrefs.GetInt("bestScore1" + scene.ToString()));
        //bestScores.Add(PlayerPrefs.GetInt("bestScore2" + scene.ToString()));
        //bestScores.Add(PlayerPrefs.GetInt("bestScore3" + scene.ToString()));
        //bestScores.Add(PlayerPrefs.GetInt("bestScore4" + scene.ToString()));
        //bestScores.Add(PlayerPrefs.GetInt("bestScore5" + scene.ToString()));

        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore1" + scene.ToString());

        if (SceneManager.GetActiveScene().name == "DeathScreen" || SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            score = PlayerPrefs.GetInt("currentScore");
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void OnDisable()
    {
        if (PlayerPrefs.GetInt("currentScore") > PlayerPrefs.GetInt("bestScore5" + scene.ToString())  && SceneManager.GetActiveScene().name != "DeathScreen" && SceneManager.GetActiveScene().name != "VictoryScreen")
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy ") + DateTime.Now.ToString("HH:mm");
            bestScores.Add(new LocalScoreClass(0, date, PlayerPrefs.GetInt("currentScore")));

            // bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore1" + scene.ToString()), PlayerPrefs.GetInt("bestScore1" + scene.ToString())));
            // bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore2" + scene.ToString()), PlayerPrefs.GetInt("bestScore2" + scene.ToString())));
            // bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore3" + scene.ToString()), PlayerPrefs.GetInt("bestScore3" + scene.ToString())));
            // bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore4" + scene.ToString()), PlayerPrefs.GetInt("bestScore4" + scene.ToString())));
            // bestScores.Add(new LocalScoreClass(0, PlayerPrefs.GetString("dateScore5" + scene.ToString()), PlayerPrefs.GetInt("bestScore5" + scene.ToString())));

            // bestScores.Sort();

            var sortedList = bestScores.OrderBy(sc => sc.score).ToList<LocalScoreClass>();

            sortedList.Reverse();
            sortedList.RemoveAt(5);
            
            PlayerPrefs.SetString("dateScore1" + scene.ToString(), sortedList[0].date);
            PlayerPrefs.SetString("dateScore2" + scene.ToString(), sortedList[1].date);
            PlayerPrefs.SetString("dateScore3" + scene.ToString(), sortedList[2].date);
            PlayerPrefs.SetString("dateScore4" + scene.ToString(), sortedList[3].date);
            PlayerPrefs.SetString("dateScore5" + scene.ToString(), sortedList[4].date);

            PlayerPrefs.SetInt("bestScore1" + scene.ToString(), sortedList[0].score);
            PlayerPrefs.SetInt("bestScore2" + scene.ToString(), sortedList[1].score);
            PlayerPrefs.SetInt("bestScore3" + scene.ToString(), sortedList[2].score);
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), sortedList[3].score);
            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), sortedList[4].score);
        }
    }
}
