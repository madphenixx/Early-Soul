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

    [Header("Prefabs")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject meleeRange;
    [SerializeField] private GameObject parry;
    [SerializeField] private GameObject reaper;

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
        if (!ctx.canceled && PauseMenu.isPaused == false && canDistance == true)
        {
            spawnPos = new Vector2(transform.position.x + 1, transform.position.y);
            Instantiate(projectile, spawnPos, Quaternion.identity);
            
            //if (distTime == null)
            //{
                distTime = StartCoroutine(DistanceCooldown());
            //}
        } 
    }

    void MeleeAttack(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && PauseMenu.isPaused == false && canMelee == true)
        {
            // EnnemiManager.playerAttacking = true;

            if (PlayerMovement.facingRight)
            {
                //transform.position += new Vector3(1, 0, 0);
                spawnPos = new Vector2(transform.position.x + 1, transform.position.y);
            }

            else
            {
                //transform.position += new Vector3(1, 0, 0);
                spawnPos = new Vector2(transform.position.x - 1, transform.position.y);
                
            }

            Instantiate(meleeRange, spawnPos, Quaternion.identity);
            // Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);

            //if (meleeTime == null)
            //{
                meleeTime = StartCoroutine(MeleeCooldown());
            //} 
        }
    }

    void Parry(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && PauseMenu.isPaused == false && canParry == true)
        {
            if (PlayerMovement.facingRight)
            {
                spawnPos = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z + 0.2f);
                Instantiate(parry, spawnPos, Quaternion.identity, transform);
            }

            else
            {
                spawnPos = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z + 0.2f);
                GameObject parryObj = Instantiate(parry, spawnPos, Quaternion.identity, transform);
                parryObj.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                reaper = GameObject.Find("Faucheuse");
                reaper.SetActive(false);
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
                reaper.SetActive(true);
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
