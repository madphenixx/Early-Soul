using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference moveRef;
    [SerializeField] private InputActionReference jumpRef;
    [SerializeField] private InputActionReference dashRef;
    [SerializeField] private InputActionReference dodgeRef;

    [Header("Audio")]
    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioSource walkTrans;

    [SerializeField] private Animator playerAnimator;

    private SpriteRenderer spriteRenderer;
    //private Rigidbody2D rb;
    
    [Header("Settings: Movements")]
    [SerializeField] private float playerSpeed = 10;
    [SerializeField] private float basePlayerSpeed = 10;
    //[SerializeField] private float jumpForce = 10;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dodgeSpeed = 8f;

    [Header("Settings: iFrames")]
    [SerializeField] private float iframeTimeDodge = 1;
    [SerializeField] private float iframeTimeDamage = 0.5f;

    [Header("Debug: movement")]
    public float direction;
    
    [Header("Debug: booleans")]
    //[SerializeField] private bool isGrounded;
    [SerializeField] private bool isDodging;
    public static bool facingRight = true;
    public static bool isInvicible = false;

    void Awake()
    {
        isInvicible = false;
        direction = 0;
        playerAnimator.SetBool("isWalking", false);
        playerAnimator = GetComponent<Animator>();
        walk.Pause();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        //rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        moveRef.action.started += Move;
        moveRef.action.performed += Move;
        moveRef.action.canceled += Move;

        //jumpRef.action.started += Jump;
        //jumpRef.action.canceled += Jump;

        dashRef.action.started += Dash;
        dashRef.action.canceled += Dash;

        dodgeRef.action.started += Dodge;
        dodgeRef.action.canceled += Dodge;
    }

    private void FixedUpdate()
    {
        if (GameManager.movementAllowed)
        {
            // rb.linearVelocity = new Vector2(direction * playerSpeed * Time.deltaTime, rb.linearVelocityY);
            transform.position += new Vector3(direction * playerSpeed * Time.deltaTime, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDodging)
        {
            spriteRenderer.flipX = false;
        }

        else
        {
            Flip();
        }

        // if (direction == 0)
        // {
        //     playerAnimator.SetTrigger("StopWalking");
        // }

        // else
        // {
        //     playerAnimator.SetTrigger("IsWalking");
        // }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnnemiAttack"))
        {
            StartCoroutine(BlinkingDamage());
        }
    }

    void Move(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && DialogueManager.dialogueActive == false)
        {
            playerAnimator.SetBool("isWalking", true);
            direction = ctx.ReadValue<float>();
            walk.Pause();
            walkTrans.Play();
        }

        else if (ctx.canceled)
        {
            direction = 0;
            playerAnimator.SetBool("isWalking", false);
            walk.Play();
            walkTrans.Play();
        }
    }

    public void TookDamage(GameObject attacker, float distance)
    {
        StartCoroutine(IframeTime(iframeTimeDamage));

        if (attacker.transform.position.x < transform.position.x)
        {
            // Vector3 desiredPosition = transform.position + new Vector3(10, 0, 0);
            // Vector3 newPosition = Vector3.Lerp(transform.position, desiredPosition, playerSpeed * Time.deltaTime);
            // transform.position = newPosition;

            float wantedPosition = transform.position.x + distance;
            playerAnimator.SetBool("isDodging", true);
            direction = 1;

            if (transform.position.x < wantedPosition)
            {
                direction = 0;
                playerAnimator.SetBool("isDodging", false);
            }
        }

        else if (attacker.transform.position.x >= transform.position.x)
        {
            // Vector3 desiredPosition = transform.position + new Vector3(-10, 0, 0);
            // Vector3 newPosition = Vector3.Lerp(transform.position, desiredPosition, playerSpeed * Time.deltaTime);
            // transform.position = newPosition;

            // transform.position += new Vector3(-distance, 0, 0);

            float wantedPosition = transform.position.x - distance;
            playerAnimator.SetBool("isDodging", true);
            direction = -1;

            if (transform.position.x > wantedPosition)
            {
                direction = 0;
                playerAnimator.SetBool("isDodging", false);
            }
        }
    }

    private IEnumerator BlinkingDamage()
    {

        spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.2f);

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
    }

    //private void Jump(InputAction.CallbackContext ctx)
    //{
    //    if (ctx.started && isGrounded)
    //    {
    //        // playerAnimator.SetTrigger("JumpUp");
    //        isGrounded = false;
    //        StartCoroutine(JumpTime());
    //    }
    //}

    //private IEnumerator JumpTime()
    //{
    //    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    //    float time = -1.5f;

    //    while (time < 0)
    //    {
    //        time += 5 * Time.deltaTime;
    //        yield return null;
    //    }

    //    // playerAnimator.SetTrigger("JumpDown");
    //    rb.AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
    //}

    void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerSpeed = playerSpeed * dashSpeed;
            StartCoroutine(DashTime());
        }

        // else
        // {
        //     playerAnimator.SetBool("isSprinting", false);
        // }
    }

    private IEnumerator DashTime()
    {
        if (GameManager.movementAllowed == true)
        {
            playerAnimator.SetBool("isSprinting", true);
        }
        
        float time = -1;

        while (time < 0)
        {
            time += 5 * Time.deltaTime;
            yield return null;
        }

        playerSpeed = basePlayerSpeed;
        playerAnimator.SetBool("isSprinting", false);
    }

    void Dodge(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isDodging = true;
            playerSpeed = basePlayerSpeed * dodgeSpeed;

            StartCoroutine(DodgeTime());
            StartCoroutine(IframeTime(iframeTimeDodge));
        }

        if (ctx.canceled)
        {
            
        }
    }

    private IEnumerator DodgeTime()
    {
        if (GameManager.movementAllowed == true)
        {
            playerAnimator.SetBool("isDodging", true);
        }

        direction = -1;

        while (direction < 0)
        {
            direction += 5 * Time.deltaTime;
            yield return null;
        }

        direction = 0;
        playerSpeed = basePlayerSpeed;

        playerAnimator.SetBool("isDodging", false);
        isDodging = false;
    }

    private IEnumerator IframeTime(float iframeTime)
    {
        isInvicible = true;
        yield return new WaitForSeconds(iframeTime);
        isInvicible = false;
    }

    void Flip()
    {
        if (direction < 0 && facingRight && !isDodging)
        {
            facingRight = false;
            spriteRenderer.flipX = true;
        } 

        else if (direction > 0 && !facingRight && !isDodging)
        {
            facingRight = true;
            spriteRenderer.flipX = false;
        }
    }

    void OnDisable()
    {
        moveRef.action.started -= Move;
        moveRef.action.performed -= Move;
        moveRef.action.canceled -= Move;

        //jumpRef.action.started -= Jump;
        //jumpRef.action.canceled -= Jump;

        dashRef.action.started -= Dash;
        dashRef.action.canceled -= Dash;

        dodgeRef.action.started -= Dodge;
        dodgeRef.action.canceled -= Dodge;
    }
}
