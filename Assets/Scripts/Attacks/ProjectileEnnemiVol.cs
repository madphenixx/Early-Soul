using UnityEngine;
using System.Collections;

public class ProjectileEnnemiVol : MonoBehaviour
{   
    private GameObject cible;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 launchDir;
    private Vector2 launchDirNorm;

    [Header("Settings")]
    public int baseAttack = 1;
    [SerializeField] private float speed = 10;
    [SerializeField] private float duration = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        cible = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ProjectileDestroy());

        if (cible.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }

        launchDir = cible.transform.position - gameObject.transform.position;
        launchDirNorm = launchDir.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = launchDirNorm * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerMovement.isInvicible == false && BoatMovements.isInvicible == false)
        {
            GameManager.pv +=  - baseAttack;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo : " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x1";

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score : "+ GameManager.score.ToString();

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
