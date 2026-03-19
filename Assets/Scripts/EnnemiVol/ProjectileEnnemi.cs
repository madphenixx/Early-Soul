using UnityEngine;

public class ProjectileEnnemi : MonoBehaviour
{   
    public GameObject cible;
    public Rigidbody2D rb;
    // public Animator enemyAnimator;

    public Vector2 launchDir;
    public Vector2 launchDirNorm;
    public float speed;

    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        cible = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        launchDir = cible.transform.position - gameObject.transform.position;
        launchDirNorm = launchDir.normalized;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.pv +=  - 1;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score: "+ GameManager.score.ToString();
            // gameManager.AddScoreAdd(10, false);

            Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        rb.linearVelocity = launchDirNorm * speed;
    }
}
