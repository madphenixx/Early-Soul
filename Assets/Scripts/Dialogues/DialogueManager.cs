using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public InputActionReference interactRef;
    
    public DialogueSO currentConversation;
    public GameObject dialogueCanvas;
    public Text charaName;
    public Image charaAvatar;
    public Text dialogueText;

    public Sprite currentAvatar;
    public CharaSO[] charaSO;
    public Coroutine typeWriterRoutine;

    public int stepNum = 0;
    public float typingSpeed = 0.02f;
    public string currentSpeaker;
    
    public bool dialogueActive;
    public bool dialogueNext;
    public bool canContinueText = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueCanvas = GameObject.Find("DialogueUI");
        charaName = GameObject.Find("CharaText").GetComponent<Text>();
        charaAvatar = GameObject.Find("Avatar").GetComponent<Image>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();

        dialogueCanvas.SetActive(false);

        interactRef.action.started += DialogueEnter;
        interactRef.action.canceled += DialogueEnter;
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
        GameManager.movementAllowed = false;
    }

    public void TurnOffDialogue()
    {
        stepNum = 0;
        dialogueActive = false;
        dialogueCanvas.SetActive(false);
        GameManager.movementAllowed = true;
    }

    public void PlayDialogue()
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

    public void SetActorInfo()
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