using UnityEngine;

public class ProjectileEnnemiVol : MonoBehaviour
{   
    [SerializeField] private GameObject cible;
    [SerializeField] private Rigidbody2D rb;
    public int baseAttack = 1;

    private Vector2 launchDir;
    private Vector2 launchDirNorm;
    [SerializeField] private float speed = 10;

    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        cible = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        launchDir = cible.transform.position - gameObject.transform.position;
        launchDirNorm = launchDir.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = launchDirNorm * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerMovement.isInvicible == false)
        {
            GameManager.pv +=  - baseAttack;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x1";

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score: "+ GameManager.score.ToString();
            // gameManager.AddScoreAdd(10, false);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
