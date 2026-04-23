using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    private Text scoreText;
    private Text bestScoreText;

    [Header("Debug: scores")]
    [SerializeField] private int score;
    [SerializeField] private int scene;
    [SerializeField] private int bestScore1;
    [SerializeField] private int bestScore2;
    [SerializeField] private int bestScore3;
    [SerializeField] private int bestScore4;
    [SerializeField] private int bestScore5;
    
    void Start()
    {
        scene = PlayerPrefs.GetInt("savedScene");
        bestScoreText = GameObject.Find("BestScore").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        bestScore1 = PlayerPrefs.GetInt("bestScore1" + scene.ToString());
        bestScore2 = PlayerPrefs.GetInt("bestScore2" + scene.ToString());
        bestScore3 = PlayerPrefs.GetInt("bestScore3" + scene.ToString());
        bestScore4 = PlayerPrefs.GetInt("bestScore4" + scene.ToString());
        bestScore5 = PlayerPrefs.GetInt("bestScore5" + scene.ToString());

        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore1" + scene.ToString()).ToString();

        if (SceneManager.GetActiveScene().name == "DeathScreen")
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

        if (score > bestScore1)
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

        else if (score > bestScore2)
        {
            int tempBis = PlayerPrefs.GetInt("bestScore2" + scene.ToString());
            PlayerPrefs.SetInt("bestScore2" + scene.ToString(), score);

            int temp = PlayerPrefs.GetInt("bestScore3" + scene.ToString());
            PlayerPrefs.SetInt("bestScore3" + scene.ToString(), tempBis);

            tempBis = PlayerPrefs.GetInt("bestScore4" + scene.ToString());
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), temp);

            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), tempBis);
        }

        else if (score > bestScore3)
        {
            int temp = PlayerPrefs.GetInt("bestScore3" + scene.ToString());
            PlayerPrefs.SetInt("bestScore3" + scene.ToString(), score);

            int tempBis = PlayerPrefs.GetInt("bestScore4" + scene.ToString());
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), temp);

            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), tempBis);
        }

        else if (score > bestScore4)
        {
            int temp = PlayerPrefs.GetInt("bestScore4" + scene.ToString());
            PlayerPrefs.SetInt("bestScore4" + scene.ToString(), score);
            PlayerPrefs.SetInt("bestScore5" + scene.ToString(), temp);

            PlayerPrefs.SetInt("bestScore" + scene.ToString(), score);
        }

        else if (score > bestScore5)
        {
            PlayerPrefs.SetInt("bestScore" + scene.ToString(), score);
        }
    }
}
