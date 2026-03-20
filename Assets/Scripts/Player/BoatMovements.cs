using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovements : MonoBehaviour
{
    [SerializeField] private InputActionReference moveRef;

    [SerializeField] private float playerSpeed;
    [SerializeField] private Vector3 direction;

    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;
    
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
        playerTransform.position += new Vector3(direction.x * playerSpeed * Time.deltaTime, direction.y * playerSpeed * Time.deltaTime, 0);
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
}
