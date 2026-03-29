using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveRef;
    [SerializeField] private InputActionReference jumpRef;
    [SerializeField] private InputActionReference dashRef;
    [SerializeField] private InputActionReference dodgeRef;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    // public Animator playerAnimator;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float basePlayerSpeed;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float direction;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dodgeSpeed = 8f;
    
    public static bool facingRight = true;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isDodging;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        moveRef.action.started += Move;
        moveRef.action.performed += Move;
        moveRef.action.canceled += Move;

        jumpRef.action.started += Jump;
        jumpRef.action.canceled += Jump;

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
        Flip();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void Move(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            direction = ctx.ReadValue<float>();
            // playerAnimator.SetTrigger("IsWalking");
        }
        else
        {
            direction = 0;
            // playerAnimator.SetTrigger("StopWalking");
        }
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && isGrounded)
        {
            // playerAnimator.SetTrigger("JumpUp");
            isGrounded = false;
            StartCoroutine(JumpTime());
        }
    }

    private IEnumerator JumpTime()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        float time = -1.5f;
        while (time < 0)
        {
            time += 5 * Time.deltaTime;
            yield return null;
        }
        // playerAnimator.SetTrigger("JumpDown");
        rb.AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
    }

    void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerSpeed = playerSpeed * dashSpeed;
            StartCoroutine(DashTime());
            // playerAnimator.SetTrigger("IsRunning");
        }
    }

    private IEnumerator DashTime()
    {
        float time = -1;
        while (time < 0)
        {
            time += 5 * Time.deltaTime;
            yield return null;
        }
        playerSpeed = basePlayerSpeed;
    }

    void Dodge(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isDodging = true;
            playerSpeed = basePlayerSpeed * dodgeSpeed;
            StartCoroutine(DodgeTime());
            // playerAnimator.SetTrigger("IsDodging");
        }
        if (ctx.canceled)
        {
            isDodging = false;
        }
    }

    private IEnumerator DodgeTime()
    {
        direction = -1;
        while (direction < 0)
        {
            direction += 5 * Time.deltaTime;
            yield return null;
        }
        direction = 0;
        playerSpeed = basePlayerSpeed;
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

        jumpRef.action.started -= Jump;
        jumpRef.action.canceled -= Jump;

        dashRef.action.started -= Dash;
        dashRef.action.canceled -= Dash;

        dodgeRef.action.started -= Dodge;
        dodgeRef.action.canceled -= Dodge;
    }
}
