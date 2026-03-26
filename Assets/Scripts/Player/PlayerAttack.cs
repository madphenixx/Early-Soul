using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{ 
    [SerializeField] private InputActionReference distanceRef;
    [SerializeField] private InputActionReference meleeRef;
    [SerializeField] private InputActionReference parryRef;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject meleeRange;
    [SerializeField] private GameObject parry;

    public static Vector2 spawnPos;
    [SerializeField] private float parryCooldownTime = 0.3f;
    [SerializeField] private float meleeCooldownTime = 0.3f;

    [SerializeField] private bool canParry = true;
    [SerializeField] private bool canMelee = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanceRef.action.started += DistanceAttack;
        distanceRef.action.canceled += DistanceAttack;

        meleeRef.action.started += MeleeAttack;
        meleeRef.action.canceled += MeleeAttack;

        parryRef.action.started += Parry;
        parryRef.action.canceled += Parry;
    }

    void DistanceAttack(InputAction.CallbackContext ctx)
    { 
        if (!ctx.canceled && PauseMenu.isPaused == false)
        {
            spawnPos = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            Instantiate(projectile, spawnPos, Quaternion.identity);
        } 
    }

    void MeleeAttack(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && PauseMenu.isPaused == false && canMelee == true)
        {
            //transform.position += new Vector3(1, 0, 0);
            spawnPos = new Vector2(transform.position.x, transform.position.y);
            Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);
            StartCoroutine(MeleeCooldown());
        }
    }

    void Parry(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled && PauseMenu.isPaused == false && canParry == true)
        {
            //transform.position += new Vector3(1, 0, 0);
            spawnPos = new Vector2(transform.position.x + 1, transform.position.y);
            Instantiate(parry, spawnPos, Quaternion.identity);
            StartCoroutine(ParryCooldown());
        }
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
