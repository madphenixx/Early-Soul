using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{ 
    public InputActionReference distanceRef;
    public InputActionReference meleeRef;

    public GameObject projectile;
    
    public float speed = 10;
    public Vector2 spawnPos;
    public float reach = 7;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distanceRef.action.started += DistanceAttack;
        distanceRef.action.canceled += DistanceAttack;

        meleeRef.action.started += MeleeAttack;
        meleeRef.action.canceled += MeleeAttack;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void DistanceAttack(InputAction.CallbackContext ctx)
    { 
        if (!ctx.canceled)
        {
            spawnPos = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            Instantiate(projectile, spawnPos, Quaternion.identity);
        } 
    }

    void MeleeAttack(InputAction.CallbackContext ctx)
    {
        if (!ctx.canceled)
        {
            StartCoroutine(MeleeTime());
        }
    }

    private IEnumerator MeleeTime()
    {
        //gameObject.transform.position.x += 1;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        float time = -1f;
        while (time < 0)
        {
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
