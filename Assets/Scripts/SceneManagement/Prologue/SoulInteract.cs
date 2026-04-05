using UnityEngine;
using UnityEngine.InputSystem;

public class SoulInteract : MonoBehaviour
{
    [SerializeField] private InputActionReference collectRef;

    [SerializeField] private GameObject soulInteraction;

    [SerializeField] private bool canInteract;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectRef.action.started += Collect;
        collectRef.action.canceled += Collect;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("RAAAAAAAAAAAAAAAA");
        if (collision.gameObject.CompareTag("Player"))
        {
            soulInteraction.SetActive(true);
        }

        canInteract = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soulInteraction.SetActive(false);
        }
    }

    void Collect(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && canInteract)
        {
            PrologueManager.soulCollected = true;
            Destroy(gameObject);
        }
    }

    void OnDisable()
    {
        collectRef.action.started -= Collect;
        collectRef.action.canceled -= Collect; 
    }
}
