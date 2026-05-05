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

    // Update is called once per frame
    void Update()
    {
        score = PlayerPrefs.GetInt("currentScore");

        if (score > bestScores[0])
        {
            // PlayerPrefs.SetInt("bestScore1" + scene.ToString(), score);

            int temp = PlayerPrefs.GetInt("bestScore1"  + scene.ToString());
            PlayerPrefs.SetInt("bestScore1" + scene.ToString(), score);

            int tempBis = PlayerPrefs.GetInt("bestScore2" + scene.ToString());
            PlayerPrefs.SetInt("bestScore2" + scene.ToString(), temp);

            temp = PlayerPrefs.GetInt("bestScore3" + scene.ToString());
            PlayerPrefs.SetInt("bestScore3" + scene.ToString(), tempBis);

            tempBis = PlayerPrefs.GetInt("bestScore4" + scene.ToString());
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), temp);

            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), tempBis);
        }

        else if (score > bestScores[1])
        {
            int tempBis = PlayerPrefs.GetInt("bestScore2" + scene.ToString());
            PlayerPrefs.SetInt("bestScore2" + scene.ToString(), score);

            int temp = PlayerPrefs.GetInt("bestScore3" + scene.ToString());
            PlayerPrefs.SetInt("bestScore3" + scene.ToString(), tempBis);

            tempBis = PlayerPrefs.GetInt("bestScore4" + scene.ToString());
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), temp);

            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), tempBis);
        }

        else if (score > bestScores[2])
        {
            int temp = PlayerPrefs.GetInt("bestScore3" + scene.ToString());
            PlayerPrefs.SetInt("bestScore3" + scene.ToString(), score);

            int tempBis = PlayerPrefs.GetInt("bestScore4" + scene.ToString());
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), temp);

            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), tempBis);
        }

        else if (score > bestScores[3])
        {
            int temp = PlayerPrefs.GetInt("bestScore4" + scene.ToString());
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), score);
            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), temp);

            PlayerPrefs.SetInt("bestScore" + scene.ToString(), score);
        }

        else if (score > bestScores[4])
        {
            PlayerPrefs.SetInt("bestScore" + scene.ToString(), score);
        }
    }
}
