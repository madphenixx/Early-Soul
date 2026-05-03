using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference interactRef;
    
    [Header("UI Elements")]
    [SerializeField] private Text charaName;
    [SerializeField] private Image charaAvatarLeft;
    [SerializeField] private Image charaAvatarRight;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text dialogueTextSolo;
    [SerializeField] private GameObject dialogueBulle;
    [SerializeField] private GameObject dialogueBulleSolo;
    [SerializeField] private GameObject dialogueCanvas;
    private Sprite currentAvatar;

    private GameObject player;

    [Header("All characters")]
    [SerializeField] private CharaSO[] charaSO;
    private DialogueSO currentConversation;
    private Coroutine typeWriterRoutine;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.02f;
    private int stepNum = 0;
    private string currentSpeaker;
    
    public static bool dialogueActive;
    private bool dialogueNext;
    private bool canContinueText = true;
    private bool uniqueCharacter = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
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

        if (player.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            PlayerMovement pM = playerMovement;
            pM.direction = 0;
        }

        else if (player.TryGetComponent<ReaperMovements>(out ReaperMovements reaperMovements))
        {
            ReaperMovements rM = reaperMovements;
            rM.direction = 0;
        }
        
        GameManager.movementAllowed = false;
        GameManager.canAttack = false;
    }

    private void TurnOffDialogue()
    {
        stepNum = 0;
        dialogueActive = false;
        dialogueCanvas.SetActive(false);

        GameManager.movementAllowed = true;
        GameManager.canAttack = true;
    }

    private void PlayDialogue()
    {
        SetActorInfo();

        if (currentConversation.isRight[stepNum] == false)
        {
            charaName.text = currentSpeaker;
            charaAvatarLeft.sprite = currentAvatar;

            PolishActor();
        }

        else if (currentConversation.isRight[stepNum] == true)
        {
            charaName.text = currentSpeaker;
            charaAvatarRight.sprite = currentAvatar;

            PolishActor();
        }
        
        if (typeWriterRoutine != null)
        {
            StopCoroutine(typeWriterRoutine);
        }

        if (uniqueCharacter == false)
        typeWriterRoutine = StartCoroutine(typeWriterEffect(dialogueText.text = currentConversation.dialogues[stepNum], dialogueText));

        else if (uniqueCharacter == true)
        typeWriterRoutine = StartCoroutine(typeWriterEffect(dialogueTextSolo.text = currentConversation.dialogues[stepNum], dialogueTextSolo));

        dialogueCanvas.SetActive(true);
        stepNum += 1;
    }

    private void PolishActor()
    {
        uniqueCharacter = true;
        CharaSO characterRef = currentConversation.characters[0];

        for (int i = 0; i < currentConversation.characters.Length; i++)
        {
            if (currentConversation.characters[i] != characterRef)
            {
                uniqueCharacter = false;
            }
        }

        if (uniqueCharacter == true)
        {
            charaAvatarRight.gameObject.SetActive(false);
            dialogueBulle.SetActive(false);
            dialogueBulleSolo.SetActive(true);
            dialogueText.gameObject.SetActive(false);
            dialogueTextSolo.gameObject.SetActive(true);
        }

        else
        {
            charaAvatarRight.gameObject.SetActive(true);
            dialogueBulle.SetActive(true);
            dialogueBulleSolo.SetActive(false);
            dialogueText.gameObject.SetActive(true);
            dialogueTextSolo.gameObject.SetActive(false);

            bool currentIsRight = currentConversation.isRight[stepNum];

            for (int i = 0; i < currentConversation.isRight.Length; i++)
            {
                if (currentConversation.isRight[i] == !currentIsRight)
                {
                    if (currentIsRight == false)
                    {
                        charaAvatarRight.sprite = currentConversation.characters[i].avatar;
                        break;
                    }
                    
                    else if (currentIsRight == true)
                    {
                        charaAvatarLeft.sprite = currentConversation.characters[i].avatar;
                        break;
                    }
                }
            }

            if (currentIsRight == false)
            {
                charaAvatarLeft.color = Color.white;
                charaAvatarRight.color = Color.slateGray;
            }

            if (currentIsRight == true)
            {
                charaAvatarLeft.color = Color.slateGray;
                charaAvatarRight.color = Color.white;
            }
        }
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
            //Debug.Log("AAAAAAAAAAAAAAAAAAAA");
            dialogueNext = true;
        }
    }

    private IEnumerator typeWriterEffect(string line, Text diaText)
    {
        diaText.text="";
        canContinueText = false;
        yield return new WaitForSeconds(0.5f);

        foreach (char letter in line.ToCharArray())
        {
            if (dialogueNext == true)
            {
                diaText.text = line;
                dialogueNext = false;
                break;
            }
            
            diaText.text += letter;
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