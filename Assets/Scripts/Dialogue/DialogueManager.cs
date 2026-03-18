using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public InputActionReference distanceRef;
    public DialogueSO currentConversation;
    public GameObject dialogueCanvas;
    public Text charaName;
    public Image charaAvatar;
    public Text dialogueText;

    public bool dialogueActive;
    public int stepNum;
    public string currentSpeaker;
    public Sprite currentAvatar;
    public CharaSO[] charaSO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // dialogueCanvas = GameObject.Find("DialogueCanvas");
        // charaName = GameObject.Find("CharaText").GetComponent<Text>();
        // charaAvatar = GameObject.Find("Avatar").GetComponent<Image>();
        // dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (dialogueActive && distanceRef.started)
        {
            Debug.Log("Interact");
            if (stepNum >= currentConversation.characters.Length)
            {
                TurnOffDialogue();
            }

            else
            {
                PlayDialogue();
            }
        }
    }

    public void InitiateDialogue(CharaDialogue charaDialogue)
    {
        currentConversation = charaDialogue.conversations[0];
        dialogueActive = true;
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
        dialogueText.text = currentConversation.dialogues[stepNum];
        dialogueCanvas.SetActive(true);
        stepNum += 1;
        charaName.text = currentSpeaker;
        charaAvatar.sprite = currentAvatar;
    }

    public void SetActorInfo()
    {
        for (int i = 0; i < charaSO.Length; i++)
        {
            if (charaSO[i].nom == currentConversation.characters[stepNum].ToString())
            {
                currentSpeaker = charaSO[i].nom;
                currentAvatar = charaSO[i].avatar;
            }
        }
    }
}


public enum CharaSOs
{
    Faucheuse,
    Lucy
};
