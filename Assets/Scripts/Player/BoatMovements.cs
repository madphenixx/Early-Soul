using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovements : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference moveRef;

    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;

    [Header("Debug: movement")]
    [SerializeField] private Vector3 direction;

    [Header("Settings")]
    [SerializeField] private float playerSpeed;
    
    [Header("Debug: booleans")]
    [SerializeField] private bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();

        moveRef.action.started += MoveBoat;
        moveRef.action.performed += MoveBoat;
        moveRef.action.canceled += MoveBoat;
    }

    private void FixedUpdate()
    {
        if (GameManager.movementAllowed)
        {
            playerTransform.position += new Vector3(direction.x * playerSpeed * Time.deltaTime, direction.y * playerSpeed * Time.deltaTime, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.x < 0 && facingRight)
        {
            Flip();
        } 

        else if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void MoveBoat(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            direction = ctx.ReadValue<Vector2>();
        }
        
        else
        {
            direction = new Vector2(0,0);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    void OnDisable()
    {
        
        moveRef.action.started -= MoveBoat;
        moveRef.action.performed -= MoveBoat;
        moveRef.action.canceled -= MoveBoat;
    }
}
