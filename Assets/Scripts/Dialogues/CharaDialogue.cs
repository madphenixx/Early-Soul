using UnityEngine;

public class CharaDialogue : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueSO[] conversations;

    private DialogueManager dialogueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D collision) //On modifiera pour que cela se lance quand on attaeint autre chose mais la c'est pas important
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boat"))
        { 
            dialogueManager.InitiateDialogue(this);
            Destroy(gameObject);
        }
    }
}
