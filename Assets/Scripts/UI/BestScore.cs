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
    
    void Start()
    {
        scene = PlayerPrefs.GetInt("savedScene");
        bestScoreText = GameObject.Find("BestScore").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        bestScore = PlayerPrefs.GetInt("bestScore" + scene.ToString());
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore" + scene.ToString()).ToString();

        if (SceneManager.GetActiveScene().name == "DeathScreen")
        {
            score = PlayerPrefs.GetInt("currentScore");
            scoreText.text = "Score: " + score.ToString();
            bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore" + scene.ToString()).ToString();
        }

        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("bestScore" + scene.ToString()).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        score = PlayerPrefs.GetInt("currentScore");
        Debug.Log(PlayerPrefs.GetInt("currentScore"));

        if (score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore" + scene.ToString(), score);
            Debug.Log("good");
        }
    }
}
