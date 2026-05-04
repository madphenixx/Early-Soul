using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class BoatMovements : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference moveRef;

    [Header("Effects")]
    [SerializeField] private ParticleSystem swoosh;
    [SerializeField] private ParticleSystem swooshFront;
    [SerializeField] private ParticleSystem swooshBack;

    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;

    [Header("Debug: movement")]
    [SerializeField] private Vector3 direction;

    [Header("Settings")]
    [SerializeField] private float playerSpeed;
    
    [Header("Debug: booleans")]
    [SerializeField] private bool facingRight = true;
    public static bool isInvicible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();

        moveRef.action.started += MoveBoat;
        moveRef.action.performed += MoveBoat;
        moveRef.action.canceled += MoveBoat;

        swoosh.gameObject.SetActive(false);
        swooshFront.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (GameManager.movementAllowed)
        {
            playerTransform.position += new Vector3(direction.x * playerSpeed * Time.deltaTime, direction.y * playerSpeed * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnnemiAttack"))
        {
            StartCoroutine(BlinkingDamage());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (direction.x < 0 && facingRight)
        // {
        //     Flip();
        // } 

        // else if (direction.x > 0 && !facingRight)
        // {
        //     Flip();
        // }
    }

    void MoveBoat(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && GameManager.movementAllowed)
        {
            direction = ctx.ReadValue<Vector2>();
            swoosh.gameObject.SetActive(true);

            if (direction.x == 1)
            {
                swooshFront.gameObject.SetActive(true);
            }

            else if (direction.x == -1)
            {
                swooshBack.gameObject.SetActive(true);
            }
        }
        
        else
        {
            direction = new Vector2(0,0);
            swoosh.gameObject.SetActive(false);
            swooshFront.gameObject.SetActive(false);
            swooshBack.gameObject.SetActive(false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private IEnumerator BlinkingDamage()
    {
        isInvicible = true;

        spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.2f);

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);

        isInvicible = false;
    }

    void OnDisable()
    {
        
        moveRef.action.started -= MoveBoat;
        moveRef.action.performed -= MoveBoat;
        moveRef.action.canceled -= MoveBoat;
    }
}
