using UnityEngine;
using System.Collections;

public class AOEProjectileBoss : MonoBehaviour
{
    [SerializeField] private AudioSource aoeBoss;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 launchDir;

    [Header("Settings")]
    public int baseAttack = 2;
    [SerializeField] private float speed = 0.7f;
    [SerializeField] private float duration = 1;

    [Header("Debug: booleans")]
    public bool isLeftOne = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ProjectileDestroy());

        if (isLeftOne)
        {
            launchDir = new Vector2(-10, 0);
            spriteRenderer.flipX = true;
        }

        else
        {
            launchDir = new Vector2(10, 0);
        }

        aoeBoss.Play();
        aoeBoss.volume = PlayerPrefs.GetFloat("SFXvolume");
    }

    void FixedUpdate()
    {
        aoeBoss.volume = PlayerPrefs.GetFloat("SFXvolume");
        rb.linearVelocity = launchDir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerMovement.isInvicible == false)
        {
            GameManager.pv += - baseAttack;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo : x" + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x1";

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score : " + GameManager.score.ToString();

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
