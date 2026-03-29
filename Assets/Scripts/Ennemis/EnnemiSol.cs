using UnityEngine;
using System.Collections;

public class EnnemiSol : MonoBehaviour
{
    private GameObject[] allAttacks;
    [SerializeField] private GameObject meleeRange;
    [SerializeField] private GameObject player;
    
    private SpriteRenderer spriteRenderer;
    public static Vector2 spawnPos;
    [SerializeField] private float attackTime;
    [SerializeField] private int playerAttackCount;

    public bool tookDamage = false;
    public bool parryTime = false;
    public bool isAttacker = false;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private bool canAttack = true;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(MeleeAttack()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (tookDamage)
        {
            canAttack = false;

            if (player.transform.position.x < transform.position.x)
            {
                Move(2);
            }

            else
            {
                Move(-2);
            }

            tookDamage = false;
            canAttack = true;
        }

        if (parryTime && isAttacker)
        {
            canAttack = false;

            if (player.transform.position.x < transform.position.x)
            {
                Move(2);
            }

            else
            {
                Move(-2);
            }

            canAttack = true;
            parryTime = false;
        }

        if (transform.position.x < player.transform.position.x && facingRight)
        {
            Flip();
        } 

        else if (transform.position.x > player.transform.position.x && !facingRight)
        {
            Flip();
        }

        allAttacks = GameObject.FindGameObjectsWithTag("PlayerAttack");
        playerAttackCount = allAttacks.Length;
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private IEnumerator MeleeAttack()
    {
        while (true)
        {
            if (canAttack && playerAttackCount == 0 && isAttacker)
            {
                attackTime = Random.Range(Time.deltaTime, 3f);
                yield return new WaitForSeconds(attackTime);

                float distance = Vector2.Distance(player.transform.position, transform.position);

                if (player.transform.position.x < transform.position.x)
                {
                    MoveAttack(-distance + 2);
                }

                else
                {
                    MoveAttack(distance - 2);
                }
            }

            else if(canAttack && playerAttackCount == 0 && !isAttacker)
            {
                float moveTime = Random.Range(Time.deltaTime, 1.5f);
                yield return new WaitForSeconds(moveTime);

                float distance = Vector2.Distance(player.transform.position, transform.position);

                if (player.transform.position.x < transform.position.x)
                {
                    Move(-distance + 2);
                }

                else
                {
                    Move(distance - 2);
                }
            }

            else
            {
                yield return new WaitUntil(() => canAttack && playerAttackCount == 0);
            }
        }
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
