using UnityEngine;

public class ProjCinématique : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private GameObject cible;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 launchDir;
    private Vector2 launchDirNorm;

    [Header("Settings")]
    [SerializeField] private float speed = 10;

    void Start()
    {
        cible = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
        {
            Vector3 spawnPosEx = new Vector3(transform.position.x - 2f, transform.position.y);
            Instantiate(explosion, spawnPosEx, Quaternion.identity);
            SeraphManager.boatTouched = true;
            Destroy(gameObject);
        }
    }
}
