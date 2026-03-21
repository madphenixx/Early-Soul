using UnityEngine;

public class MeleeEnnemi : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.pv +=  - 1;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score: "+ GameManager.score.ToString();
            // gameManager.AddScoreAdd(10, false);

            Destroy(gameObject);
        }
    }
}
