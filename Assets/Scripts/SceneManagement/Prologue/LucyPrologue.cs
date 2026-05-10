using UnityEngine;

public class LucyPrologue : MonoBehaviour
{
    [SerializeField] private GameObject cible;
    
    [SerializeField] private Animator playerAnimator;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
    [Header("Settings: Movements")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxDistanceApproach = 4;
    [SerializeField] private float distanceApproach = -3;

    [Header("Debug: movement")]
    public float direction;
    
    [Header("Debug: booleans")]
    public static bool facingRight = true; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(cible.transform.position, transform.position);

        if (distance > maxDistanceApproach)
        {
            playerAnimator.SetBool("isWalking", true);

            if (cible.transform.position.x < transform.position.x)
            {
                direction = -1;
            }

            else
            {
                direction = 1;
            }
        }

        else if (distance - distanceApproach < maxDistanceApproach)
        {
            direction = 0;
        }

        Flip();

        if (direction == 0)
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    void Flip()
    {
        if (direction < 0 && facingRight)
        {
            facingRight = false;
            spriteRenderer.flipX = true;
        } 

        else if (direction > 0 && !facingRight)
        {
            facingRight = true;
            spriteRenderer.flipX = false;
        }
    }
}
