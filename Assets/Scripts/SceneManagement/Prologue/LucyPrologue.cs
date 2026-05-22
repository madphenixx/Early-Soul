using UnityEngine;

public class LucyPrologue : MonoBehaviour
{
    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioSource walkTrans;

    [SerializeField] private GameObject cible;
    
    [SerializeField] private Animator playerAnimator;

    private SpriteRenderer spriteRenderer;
    
    [Header("Settings: Movements")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float maxDistanceApproach = 7;
    [SerializeField] private float distanceApproach = -4;

    [Header("Debug: movement")]
    public float direction;
    
    [Header("Debug: booleans")]
    public static bool facingRight = false; 

    void Awake()
    {
        walk.Stop();
        walkTrans.Stop();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
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

        if (distance > maxDistanceApproach && DialogueManager.dialogueActive == false)
        {
            walkTrans.Play();
            walk.Play();
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
            walkTrans.Play();
            walk.Pause();
            direction = 0;
        }

        Flip();

        if (direction == 0)
        {
            //walkTrans.Play();
            walk.Pause();
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
