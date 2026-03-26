using UnityEngine;
using System.Collections;

public class ParryPlayer : MonoBehaviour
{
    public static bool parryTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ParryDestroy());
    }

    private IEnumerator ParryDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnnemiAttack"))
        {
            GameManager.combo += 1;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();

            GameManager.score = GameManager.score + 10;
            GameManager.scoreText.text = "Score: " + GameManager.score.ToString();
            parryTime = true;
        }
    }
}
