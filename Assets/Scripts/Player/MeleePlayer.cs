using UnityEngine;
using UnityEngine.UI;

public class MeleePlayer : MonoBehaviour
{
    public GameManager gameManager;

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
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            int produit = 1 + (GameManager.combo / 5); //A modifier et équilibrer (multiplicateur de combo)}
            Slider slEnnemi = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();

            collision.gameObject.GetComponent<ClassEnnemi>().pv += -produit;
            slEnnemi.value = collision.gameObject.GetComponent<ClassEnnemi>().pv;

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();

            GameManager.score = GameManager.score + 10 * produit;
            // gameManager.AddScoreAdd(10 * produit, true);
            GameManager.scoreText.text = "Score: " + GameManager.score.ToString();

            Destroy(gameObject);
        }
    }
}
