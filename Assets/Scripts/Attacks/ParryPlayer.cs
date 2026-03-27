using UnityEngine;
using System.Collections;

public class ParryPlayer : MonoBehaviour
{
    public static bool parryTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ParryDestroy(0.2f));
    }

    private IEnumerator ParryDestroy(float time)
    {
        yield return new WaitForSeconds(time);
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

            ParryDestroy(0.1f);
        }
    }
}
