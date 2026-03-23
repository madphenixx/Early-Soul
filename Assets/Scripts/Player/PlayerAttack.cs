using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{ 
    [SerializeField] private InputActionReference distanceRef;
    [SerializeField] private InputActionReference meleeRef;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject meleeRange;
    
    public static Vector2 spawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanceRef.action.started += DistanceAttack;
        distanceRef.action.canceled += DistanceAttack;

        meleeRef.action.started += MeleeAttack;
        meleeRef.action.canceled += MeleeAttack;
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
        if (!ctx.canceled && PauseMenu.isPaused == false)
        {
            //transform.position += new Vector3(1, 0, 0);
            spawnPos = new Vector2(transform.position.x, transform.position.y);
            Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);
        }
    }
}
