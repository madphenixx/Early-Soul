using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeleePlayer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(MeleeDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemi") || collision.gameObject.CompareTag("EnnemiSol"))
        {
            if (collision.gameObject.CompareTag("EnnemiSol"))
            {
                EnnemiSol.tookDamage = true;
            }

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

    private IEnumerator MeleeDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    } 
}