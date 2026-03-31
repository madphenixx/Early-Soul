using System.Collections;
using UnityEngine;
using static UnityEditor.Rendering.MaterialUpgrader;

public class EnnemiSol : MonoBehaviour
{
    private GameObject[] allAttacks;
    [SerializeField] private GameObject meleeRange;
    [SerializeField] private GameObject player;
    
    private SpriteRenderer spriteRenderer;
    public static Vector2 spawnPos;
    [SerializeField] private float attackTime;
    [SerializeField] private int moveCount = 0;
    [SerializeField] private int attackCount = 0;

    [SerializeField] private Coroutine attackRoutine;

    [SerializeField] private string currentState;
    private readonly string stateAttaque = "Attaque";
    private readonly string stateDefense = "Defense";
    private readonly string stateApproche = "Approche";

    public bool tookDamage = false;
    public bool parryTime = false;
    public bool isAttacker = false;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private bool facingRight = true;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentState = stateApproche;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void ApproachState()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (moveCount <= 0)
        {
            if (player.transform.position.x < transform.position.x)
            {
                Move(-distance + 2);
            }

            else
            {
                Move(distance - 2);
            }

            moveCount += 1;
        }

        if (!isAttacker)
        {
            currentState = stateDefense;
        }

        if (tookDamage)
        {
            currentState = stateDefense;
            tookDamage = false;
        }

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }

        attackRoutine = StartCoroutine(AttackWait());

        if (isAttacker && canAttack)
        {
            currentState = stateAttaque;
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
                MoveAttack(-distance + 2);
            }

            else
            {
                MoveAttack(distance - 2);
            }

            attackCount += 1;
        }

        else if(attackCount > 0)
        {
            currentState = stateDefense;
            canAttack = false;
        }
  
    }

    void DefenseState()
    {
        if (moveCount <= 0)
        {
            if (player.transform.position.x < transform.position.x)
            {
                Move(4);
                moveCount += 1;
            }

            else if (player.transform.position.x >= transform.position.x)
            {
                Move(-4);
                moveCount += 1;
            }
        }

        if (attackRoutine != null)
        {
            attackRoutine = StartCoroutine(AttackWait());
        }

        else
        {
            StopCoroutine(attackRoutine);
        }


        if (isAttacker && canAttack)
        {
            currentState = stateAttaque;
        }
    }

    private IEnumerator AttackWait()
    {
        attackTime = Random.Range(Time.deltaTime, 4f);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
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
        Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);
    }

    void Move(float distance)
    {
        Vector3 desiredPosition = transform.position + new Vector3(distance, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, 10f);
    }
}
