using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{ 
    [Header("Controls")]
    [SerializeField] private InputActionReference distanceRef;
    [SerializeField] private InputActionReference meleeRef;
    [SerializeField] private InputActionReference parryRef;

    [Header("Audio")]
    [SerializeField] private AudioSource hexCharge;

    [Header("Prefabs")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject meleeRange;
    [SerializeField] private GameObject parry;

    [SerializeField] private Animator reaper;
    [SerializeField] private Animator hex;

    public static Vector3 spawnPos;

    private Coroutine meleeTime;
    private Coroutine parryTime;
    private Coroutine distTime;

    [Header("Settings")]
    [SerializeField] private float distanceCooldownTime = 0.2f;
    [SerializeField] private float parryCooldownTime = 0.3f;
    [SerializeField] private float meleeCooldownTime = 0.3f;

    [Header("Debug: booleans")]
    [SerializeField] private bool wasFalse;
    [SerializeField] private bool canDistance = true;
    [SerializeField] private bool canParry = true;
    [SerializeField] private bool canMelee = true;
    public static bool isMeleeing = false;
    public static bool isParrying = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wasFalse = false;

        distanceRef.action.started += DistanceAttack;
        distanceRef.action.canceled += DistanceAttack;

        meleeRef.action.started += MeleeAttack;
        meleeRef.action.canceled += MeleeAttack;

        parryRef.action.started += Parry;
        parryRef.action.canceled += Parry;
    }

    void FixedUpdate()
    {
        if (GameManager.canAttack == false)
        {
            canDistance = false;
            canMelee = false;
            canParry = false;

            wasFalse = true;
        }

        if (GameManager.canAttack == true && wasFalse == true)
        {
            wasFalse = false;

            canDistance = true;
            canMelee = true;
            canParry = true;
        }
    }

    void DistanceAttack(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && PauseMenu.isPaused == false && canDistance == true && DialogueManager.dialogueActive == false)
        {
            if (PlayerMovement.facingRight == false)
            {
                hex.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                spawnPos = new Vector2(transform.position.x - 1.5f, transform.position.y + 0.427f);
            }

            else
            {
                hex.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                spawnPos = new Vector2(transform.position.x + 1.5f, transform.position.y + 0.427f);
            }

            hex.SetTrigger("isDistance");
            hexCharge.Play();
            
            Instantiate(projectile, spawnPos, Quaternion.identity);
            
            distTime = StartCoroutine(DistanceCooldown());
        } 
    }

    void MeleeAttack(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && PauseMenu.isPaused == false && canMelee == true && SceneManager.GetActiveScene().buildIndex != 2)
        {
            // EnnemiManager.playerAttacking = true;

            if (PlayerMovement.facingRight)
            {
                spawnPos = new Vector3(transform.position.x + 1, transform.position.y + 0.6f, transform.position.z + 0.2f);
                Instantiate(meleeRange, spawnPos, Quaternion.identity);
            }

            else
            {
                spawnPos = new Vector3(transform.position.x - 1, transform.position.y + 0.6f, transform.position.z + 0.2f);
                GameObject meleeObj = Instantiate(meleeRange, spawnPos, Quaternion.identity);
                meleeObj.GetComponent<SpriteRenderer>().flipX = false;

            }

                meleeTime = StartCoroutine(MeleeCooldown()); 
        }
    }

    void Parry(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && PauseMenu.isPaused == false && canParry == true)
        {
            if (PlayerMovement.facingRight)
            {
                spawnPos = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z + 0.2f);
                Instantiate(parry, spawnPos, Quaternion.identity);
            }

            else
            {
                spawnPos = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z + 0.2f);
                GameObject parryObj = Instantiate(parry, spawnPos, Quaternion.identity);
                parryObj.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                reaper = GameObject.Find("Faucheuse").GetComponent<Animator>();
                reaper.SetBool("isParry", true);
            }

            //if (parryTime == null)
            //{
                parryTime = StartCoroutine(ParryCooldown());
            //}
        }

        else if (ctx.canceled && reaper != null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                reaper.SetBool("isParry", false);
            }
        }
    }

    private IEnumerator DistanceCooldown()
    {
        canDistance = false;
        yield return new WaitForSeconds(distanceCooldownTime);
        canDistance = true;
    }

    private IEnumerator ParryCooldown()
    {
        canParry = false;
        yield return new WaitForSeconds(parryCooldownTime);
        canParry = true;
    }

    private IEnumerator MeleeCooldown()
    {
        canMelee = false;
        yield return new WaitForSeconds(meleeCooldownTime);
        canMelee = true;
    }

    void OnDisable()
    {
        distanceRef.action.started -= DistanceAttack;
        distanceRef.action.canceled -= DistanceAttack;

        meleeRef.action.started -= MeleeAttack;
        meleeRef.action.canceled -= MeleeAttack;

        parryRef.action.started -= Parry;
        parryRef.action.canceled -= Parry;
    }
}
