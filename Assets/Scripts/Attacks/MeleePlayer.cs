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
                EnnemiSol ennemiSol = collision.gameObject.GetComponent<EnnemiSol>();
                ennemiSol.tookDamage = true;
            }

            float produit = 1 + (GameManager.combo * 0.5f); //A modifier et équilibrer (multiplicateur de combo)}
            Slider slEnnemi = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x" + produit.ToString();

            collision.gameObject.GetComponent<ClassEnnemi>().pv += -1 * produit;
            slEnnemi.value = collision.gameObject.GetComponent<ClassEnnemi>().pv;

            GameManager.score = Mathf.RoundToInt(GameManager.score + 10 * produit);
            // gameManager.AddScoreAdd(10 * produit, true);
            GameManager.scoreText.text = "Score: " + GameManager.score.ToString();

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            //Debug.Log("comboe" + GameManager.combo);
            float produit = (1 + (GameManager.combo * 0.5f)) * gameObject.GetComponent<Boss>().resistanceMelee; //A modifier et équilibrer (multiplicateur de combo)}*
            //Debug.Log(produit);
            Slider slEnnemi = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x" + produit.ToString();

            collision.gameObject.GetComponent<ClassEnnemi>().pv += -1 * produit;
            slEnnemi.value = collision.gameObject.GetComponent<ClassEnnemi>().pv;


            GameManager.score = Mathf.RoundToInt(GameManager.score + 10 * produit);
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