using UnityEngine;
using System.Collections;

public class ParryPlayer : MonoBehaviour
{
    [SerializeField] private float parryeDuration = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerAttack.isParrying = true;
        GameManager.parrying = true;
        StartCoroutine(ParryDestroy(parryeDuration));
    }

    private void Update()
    {
        if (PlayerAttack.isMeleeing == true)
        {
            ParryDestroy(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnnemiAttack") || collision.gameObject.CompareTag("EnnemiSol"))
        {
            if (collision.gameObject.CompareTag("EnnemiSol"))
            {
                EnnemiSol ennemiSol = collision.gameObject.GetComponent<EnnemiSol>();
                ennemiSol.parryTime = true;
            }

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo : " + GameManager.combo.ToString();

            GameManager.score = GameManager.score + 10;
            GameManager.scoreText.text = "Score : " + GameManager.score.ToString();

            ParryDestroy(0.1f);
        }
    }

    private IEnumerator ParryDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.parrying = false;

        PlayerAttack.isParrying = false;
        Destroy(gameObject);
    }
}
