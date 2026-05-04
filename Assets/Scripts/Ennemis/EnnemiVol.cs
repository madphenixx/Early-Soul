using UnityEngine;
using System.Collections;

public class EnnemiVol : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject explosion;

    [Header("Settings")]
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 1.7f;
    [SerializeField] private float health;
    
    private Vector2 spawnPos;

    private Coroutine launchRoutine;

    [Header("Settings")]
    [SerializeField] private float spawnTime;

    public static bool canAttack = false;

    void Start()
    {
        canAttack = false;
        health = gameObject.GetComponent<ClassEnnemi>().pv;
    }

    void Update()
    {
        if (canAttack == true && launchRoutine == null)
        {
            launchRoutine = StartCoroutine(LaunchProjectiles());
        }

        else if (DialogueManager.dialogueActive == true && launchRoutine != null && canAttack == false)
        {
            StopCoroutine(launchRoutine);
        }
    }

    private IEnumerator LaunchProjectiles()
    {
        while (true)
        {
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Instantiate(projectile, spawnPos, Quaternion.identity);
        }
    }

    void OnDestroy()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y);
        Instantiate(explosion, spawnPos, Quaternion.identity);
    }
}
