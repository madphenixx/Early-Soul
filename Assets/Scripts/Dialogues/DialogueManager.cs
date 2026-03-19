using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public InputActionReference interactRef;
    public DialogueSO currentConversation;
    public GameObject dialogueCanvas;
    public Text charaName;
    public Image charaAvatar;
    public Text dialogueText;

    public bool dialogueActive;
    public bool dialogueNext;
    public int stepNum = 0;
    public string currentSpeaker;
    public Sprite currentAvatar;
    public CharaSO[] charaSO;

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
         if (dialogueActive && dialogueNext)
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
    }

    public void TurnOffDialogue()
    {
        stepNum = 0;
        dialogueActive = false;
        dialogueCanvas.SetActive(false);
    }

    public void PlayDialogue()
    {
        SetActorInfo();
        charaName.text = currentSpeaker;
        charaAvatar.sprite = currentAvatar;
        dialogueText.text = currentConversation.dialogues[stepNum];
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
}


public enum CharaSOs
{
    Faucheuse,
    Lucy
};
