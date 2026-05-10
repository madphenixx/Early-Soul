using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemiSol : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject meleeRange;
    [SerializeField] private GameObject explosion;

    private GameObject player;
    
    private SpriteRenderer spriteRenderer;
    public static Vector2 spawnPos;

    private Coroutine attackRoutine;
    private Coroutine stateRoutine;

    private int moveCount = 0;
    private int attackCount = 0;
    private float attackTime;

    [Header("Settings: Speed")]
    [SerializeField] private float reactivityTime = 0.5f;
    [SerializeField] private float minMeleeTime = 1f;
    [SerializeField] private float maxMeleeTime = 4f;
    [SerializeField] private float speed = 200;

    [Header("Settings: Defense")]
    // [SerializeField] private int dodgeChance = 11;
    [SerializeField] private int distanceDefense = 5;
    [SerializeField] private int distanceApproach = 4;
    [SerializeField] private int maxDistanceApproach = 7;

    [Header("Debug: state")]
    [SerializeField] private string currentState = null;
    private readonly string stateAttaque = "Attaque";
    private readonly string stateDefense = "Defense";
    private readonly string stateApproche = "Approche";

    [Header("Debug: booleans")]
    public bool tookDamage = false;
    public bool parryTime = false;
    public bool isAttacker = false;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool facingRight = true;   
    public static bool startAttack = false;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startAttack = false;
        currentState = null;
        
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == null && startAttack == true)
        {
            currentState = stateApproche;
        }

        if (currentState == stateApproche)
        {
            moveCount = 0;
            attackCount = 0;
            ApproachState();
        }

        else if (currentState == stateAttaque)
        {
            moveCount = 0;
            AttackState();
        }

        else if (currentState == stateDefense)
        {
            attackCount = 0;
            DefenseState();
        }

        if (transform.position.x < player.transform.position.x && facingRight)
        {
            Flip();
        } 

        else if (transform.position.x > player.transform.position.x && !facingRight)
        {
            Flip();
        }

        // if (EnnemiManager.playerAttacking == true)
        // {
        //     EnnemiManager.playerAttacking = false;
        //     int randInt = Random.Range(1, dodgeChance);
        
        //     if (randInt == 1)
        //     {
        //         currentState = stateDefense;
        //     }
        // }
    }

    void ApproachState()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (moveCount <= 0)
        {
            if (player.transform.position.x < transform.position.x)
            {
                Move(-distance + distanceApproach);
            }

            else
            {
                Move(distance - distanceApproach);
            }

            moveCount += 1;
        }
        
        if (distance <= maxDistanceApproach)
        {
            currentState = stateDefense;
        }

        if (!isAttacker)
        {
            if (stateRoutine == null)
            {
                stateRoutine = StartCoroutine(TimeState(stateDefense));
            }

            // currentState = stateDefense;
        }

        if (tookDamage || parryTime && isAttacker)
        {
            currentState = stateDefense;
            tookDamage = false;
            parryTime = false;
        }

        if (attackRoutine == null)
        {
            attackRoutine = StartCoroutine(AttackWait());
        }

        if (isAttacker && canAttack)
        {
            stateRoutine = StartCoroutine(TimeState(stateAttaque));
            attackRoutine = null;
        }
    }

    void AttackState()
    {
        if (tookDamage || parryTime && isAttacker)
        {
            currentState = stateDefense;
            
            tookDamage = false;
            parryTime = false;
        }

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (attackCount <= 0)
        {
            if (player.transform.position.x < transform.position.x)
            {
                MoveAttack(-distance + 1);
            }

            else
            {
                MoveAttack(distance - 1);
            }

            attackCount += 1;
        }

        else if (attackCount > 0)
        {
            canAttack = false;

            if (stateRoutine == null)
            {
                stateRoutine = StartCoroutine(TimeState(stateDefense));
            }
        }
    }

    void DefenseState()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        
        if (distance > maxDistanceApproach && stateRoutine == null)
        {
            StartCoroutine(TimeState(stateApproche));
        }

        if (moveCount <= 0)
        {
            if (player.transform.position.x < transform.position.x)
            {
                Move(distanceDefense);
            }

            else if (player.transform.position.x >= transform.position.x)
            {
                Move(-distanceDefense);
            }

            moveCount += 1;
        }

        if (attackRoutine == null)
        {
            attackRoutine = StartCoroutine(AttackWait());
        }

        if (isAttacker && canAttack)
        {
            attackRoutine = null;
            if (stateRoutine == null)
            {
                stateRoutine = StartCoroutine(TimeState(stateAttaque));
            }
        }

         if (tookDamage || parryTime && isAttacker)
         {
            currentState = stateApproche;
         }
    }

    private IEnumerator AttackWait()
    {
        attackTime = Random.Range(minMeleeTime, maxMeleeTime);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }

    private IEnumerator TimeState(string state)
    {
        yield return new WaitForSeconds(reactivityTime);
        currentState = state;
        stateRoutine = null;
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void MoveAttack(float distance)
    {
        Move(distance);
        
        spawnPos = new Vector2(transform.position.x, transform.position.y);
        GameObject attack = Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);
        attack.GetComponent<MeleeEnnemi>().attacker = gameObject;
    }

    void Move(float distance)
    {
        Vector3 desiredPosition = transform.position + new Vector3(distance, 0, 0);
        Vector3 newPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
        transform.position = newPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack") && isAttacker)
        {
            currentState = stateDefense;
        }
    }

    void OnDisable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Vector3 spawnPos = new Vector3(transform.position.x + 1 , transform.position.y + 2);
            Instantiate(explosion, spawnPos, Quaternion.identity);
        }
    }
}
