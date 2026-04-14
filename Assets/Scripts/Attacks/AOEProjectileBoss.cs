using UnityEngine;
using System.Collections;

public class AOEProjectileBoss : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public int baseAttack = 4;

    private Vector2 launchDir;
    [SerializeField] private float speed = 0.7f;
    [SerializeField] private float duration = 1;

    public bool isLeftOne = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ProjectileDestroy());

        if (isLeftOne)
        {
            launchDir = new Vector2(-10, 0);
        }

        else
        {
            launchDir = new Vector2(10, 0);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = launchDir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerMovement.isInvicible == false)
        {
            GameManager.pv += -baseAttack;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo: x" + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x1";

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score: " + GameManager.score.ToString();

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ProjectileDestroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
