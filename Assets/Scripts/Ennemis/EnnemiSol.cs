using UnityEngine;
using System.Collections;

public class EnnemiSol : MonoBehaviour
{
    [SerializeField] private GameObject meleeRange;
    [SerializeField] private GameObject player;
    
    private SpriteRenderer spriteRenderer;
    public static Vector2 spawnPos;
    [SerializeField] private float attackTime;

    public static bool tookDamage = false;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private bool canAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        tookDamage = false;

        StartCoroutine(MeleeAttack()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (tookDamage)
        {
            canAttack = false;

            Move(2);
            
            tookDamage = false;
            canAttack = true;
        }

        if (ParryPlayer.parryTime)
        {
            canAttack = false;

            if (player.transform.position.x < transform.position.x)
            {
                Move(2);
                ParryPlayer.parryTime = false;
            }

            else
            {
                Move(-2);
                ParryPlayer.parryTime = false;
            }

            canAttack = true;
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

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private IEnumerator MeleeAttack()
    {
        while (true)
        {
            if (canAttack)
            {
                attackTime = Random.Range(Time.deltaTime, 3f);
                yield return new WaitForSeconds(attackTime);

                float distance = Vector2.Distance(player.transform.position, transform.position);

                if (player.transform.position.x < transform.position.x)
                {
                    StartCoroutine(MovementBackwards(distance - 2));
                }

                else
                {
                    StartCoroutine(MovementForward(distance + 2));
                }
            }
        }
    }

    private IEnumerator MovementForward(float distance)
    {
        float time = -distance;
        while (time < 0)
        {
            Move(distance / 10); //a modif pour qu'il glisse pas dans la parade
            time += distance/10;
            yield return null;
        }

        spawnPos = new Vector2(transform.position.x, transform.position.y);
        Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);
    }

    private IEnumerator MovementBackwards(float distance)
    {
        float time = -distance;
        while (time < 0)
        {
            Move(-distance / 10); //a modif pour qu'il glisse pas dans la parade
            time += distance/10;
            yield return null;
        }

        spawnPos = new Vector2(transform.position.x, transform.position.y);
        Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);
    }

    void Move(float distance)
    {
        transform.position += new Vector3(distance, 0, 0);
    }
}
