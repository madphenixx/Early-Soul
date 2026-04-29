using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private InputActionReference interactRef;
    
    [Header("UI Elements")]
    [SerializeField] private Text charaNameLeft;
    [SerializeField] private Image charaNameBulleLeft;
    [SerializeField] private Image charaAvatarLeft;
    [SerializeField] private Text charaNameRight;
    [SerializeField] private Image charaNameBulleRight;
    [SerializeField] private Image charaAvatarRight;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject dialogueCanvas;
    private Sprite currentAvatar;

    [Header("Debug: detection")]
    [SerializeField] private GameObject player;

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
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
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
        
        // player.GetComponent<SpriteRenderer>().enabled = false;
        GameManager.movementAllowed = false;
    }

    private void TurnOffDialogue()
    {
        stepNum = 0;
        dialogueActive = false;
        dialogueCanvas.SetActive(false);
        // player.GetComponent<SpriteRenderer>().enabled = true;
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameManager.movementAllowed = true;
    }

    private void PlayDialogue()
    {
        SetActorInfo();
        if (currentConversation.isRight[stepNum] == false)
        {
            charaNameLeft.text = currentSpeaker;
            charaAvatarLeft.sprite = currentAvatar;

            PolishActor();
        }

        else if (currentConversation.isRight[stepNum] == true)
        {
            charaNameRight.text = currentSpeaker;
            charaAvatarRight.sprite = currentAvatar;

            PolishActor();
        }
        
        if (typeWriterRoutine != null)
        {
            StopCoroutine(typeWriterRoutine);
        }

        typeWriterRoutine = StartCoroutine(typeWriterEffect(dialogueText.text = currentConversation.dialogues[stepNum]));
        dialogueCanvas.SetActive(true);
        stepNum += 1;
    }

    private void PolishActor()
    {
        bool uniqueCharacter = true;
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
            charaNameRight.gameObject.SetActive(false);
            charaAvatarRight.gameObject.SetActive(false);
            charaNameBulleRight.gameObject.SetActive(false);
        }

        else
        {
            charaNameRight.gameObject.SetActive(true);
            charaAvatarRight.gameObject.SetActive(true);
            charaNameBulleRight.gameObject.SetActive(true);

            bool currentIsRight = currentConversation.isRight[stepNum];

            if (currentIsRight == false)
            {
                charaAvatarLeft.color = Color.white;
                charaNameBulleLeft.color = Color.white;

                charaAvatarRight.color = Color.slateGray;
                charaNameBulleRight.color = Color.slateGray;
            }

            if (currentIsRight == true)
            {
                charaAvatarLeft.color = Color.slateGray;
                charaNameBulleLeft.color = Color.slateGray;

                charaAvatarRight.color = Color.white;
                charaNameBulleRight.color = Color.white;
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