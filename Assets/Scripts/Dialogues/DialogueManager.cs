using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private InputActionReference interactRef;
    
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private Text charaName;
    [SerializeField] private Image charaAvatar;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Sprite currentAvatar;

    [SerializeField] private DialogueSO currentConversation;
    [SerializeField] private CharaSO[] charaSO;
    [SerializeField] private Coroutine typeWriterRoutine;

    [SerializeField] private int stepNum = 0;
    [SerializeField] private float typingSpeed = 0.02f;
    [SerializeField] private string currentSpeaker;
    
    public static bool dialogueActive;
    [SerializeField] private bool dialogueNext;
    [SerializeField] private bool canContinueText = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactRef.action.started += DialogueEnter;
        interactRef.action.canceled += DialogueEnter;

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && dialogueNext && canContinueText)
        {
            if (stepNum >= currentConversation.dialogues.Length)
            {
                TurnOffDialogue();
            }

            else
            {
                PlayDialogue();
                dialogueNext = false;
            }
        }
    }

    public void InitiateDialogue(CharaDialogue charaDialogue)
    {
        currentConversation = charaDialogue.conversations[0];
        dialogueActive = true;
        dialogueNext = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        player.SetActive(false);
        player.GetComponent<PlayerMovement>().direction = 0;
        GameManager.movementAllowed = false;
    }

    private void TurnOffDialogue()
    {
        stepNum = 0;
        dialogueActive = false;
        dialogueCanvas.SetActive(false);
        player.SetActive(true);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameManager.movementAllowed = true;
    }

    private void PlayDialogue()
    {
        SetActorInfo();
        charaName.text = currentSpeaker;
        charaAvatar.sprite = currentAvatar;
        if (typeWriterRoutine != null)
        {
            StopCoroutine(typeWriterRoutine);
        }
        typeWriterRoutine = StartCoroutine(typeWriterEffect(dialogueText.text = currentConversation.dialogues[stepNum]));
        dialogueCanvas.SetActive(true);
        stepNum += 1;
    }

    private void SetActorInfo()
    {
        for (int i = 0; i < charaSO.Length; i++)
        {
            if (charaSO[i].nom == currentConversation.characters[stepNum].nom.ToString())
            {
                currentSpeaker = charaSO[i].nom;
                currentAvatar = charaSO[i].avatar;
                break;
            }
        }
    }

    void DialogueEnter(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            dialogueNext = true;
        }
    }

    private IEnumerator typeWriterEffect(string line)
    {
        dialogueText.text="";
        canContinueText = false;
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in line.ToCharArray())
        {
            if (dialogueNext == true)
            {
                dialogueText.text = line;
                dialogueNext = false;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        canContinueText = true;
    }

    void OnDisable()
    {
        interactRef.action.started += DialogueEnter;
        interactRef.action.canceled += DialogueEnter;
    }
}