using UnityEngine;
using UnityEngine.InputSystem;

public class SoulInteract : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference collectRef;

    [Header("UI")]
    [SerializeField] private GameObject soulInteraction;

    [Header("Debug: booleans")]
    [SerializeField] private bool canInteract = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectRef.action.started += Collect;
        collectRef.action.canceled += Collect;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soulInteraction.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soulInteraction.SetActive(false);
        }
    }

    void Collect(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && canInteract == true)
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
