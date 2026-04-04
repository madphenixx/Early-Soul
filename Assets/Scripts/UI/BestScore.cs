using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    private Text scoreText;
    private Text bestScoreText;
    [SerializeField] private int score;
    [SerializeField] private int scene;
    [SerializeField] private int bestScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scene = PlayerPrefs.GetInt("savedScene");
        bestScoreText = scoreText = GameObject.Find("BestScore").GetComponent<Text>();
        bestScore = PlayerPrefs.GetInt("bestScore" + scene.ToString());
            
        if (bestScore == null)
        {
            Debug.Log("no best score.. yet");
            PlayerPrefs.SetInt("bestScore" + scene.ToString(), score);
        }

        if(SceneManager.GetActiveScene().name == "DeathScreen")
        {
            score = PlayerPrefs.GetInt("currentScore");
            scoreText = GameObject.Find("Score").GetComponent<Text>();

            scoreText.text = "Score: " + score.ToString();
            Debug.Log(score);
            Debug.Log(scoreText);
            bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore" + scene.ToString()).ToString();
        }

        else
        {
            if (bestScore != null)
            {
                bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore" + scene.ToString()).ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        score = PlayerPrefs.GetInt("currentScore");

        if (score > bestScore && bestScore != null)
        {
            PlayerPrefs.SetInt("bestScore" + scene.ToString(), score);
        }
    }
}
