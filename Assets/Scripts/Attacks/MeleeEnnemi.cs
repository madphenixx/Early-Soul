using UnityEngine;
using System.Collections;

public class MeleeEnnemi : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Player") && PlayerMovement.isInvicible == false)
        {
            float produit = 1 + (GameManager.combo / 5); //A modifier et équilibrer (multiplicateur de combo)}

            GameManager.pv +=  - 1;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x1";

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score: "+ GameManager.score.ToString();
            // gameManager.AddScoreAdd(10, false);

            Destroy(gameObject);
        }
    }

    private IEnumerator MeleeDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
