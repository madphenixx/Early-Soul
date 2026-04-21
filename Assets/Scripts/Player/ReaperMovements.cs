using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReaperMovements : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference moveRef;

    [SerializeField] private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    
    [Header("Settings: Movements")]
    [SerializeField] private float playerSpeed = 6;

    [Header("Debug: movement")]
    public float direction;
    
    public static bool facingRight = true;
    public static bool isInvicible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        moveRef.action.started += Move;
        moveRef.action.performed += Move;
        moveRef.action.canceled += Move;
    }

    private void FixedUpdate()
    {  
        if (GameManager.movementAllowed)
        {
            transform.position += new Vector3(direction * playerSpeed * Time.deltaTime, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    void Move(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            direction = ctx.ReadValue<float>();
            playerAnimator.SetTrigger("isWalking");
        }

        else
        {
            direction = 0;
            playerAnimator.SetTrigger("stopWalking");
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

    void OnDisable()
    {
        moveRef.action.started -= Move;
        moveRef.action.performed -= Move;
        moveRef.action.canceled -= Move;
    }
}
