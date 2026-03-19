using UnityEngine;

public class CharaDialogue : MonoBehaviour
{
    public DialogueSO[] conversations;
    public DialogueManager dialogueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision) //On modifiera pour que cela se lance quand on attaeint autre chose mais la c'est pas important
    {
        if (collision.gameObject.CompareTag("DialogueTrigger"))
        { 
            dialogueManager.InitiateDialogue(this);
            Destroy(collision.gameObject);
        }
    }
}
