using UnityEngine;
using System.Collections;

public class EnnemiVol : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject projectile;

    [Header("Settings")]
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 1.7f;
    
    private Vector2 spawnPos;

    private Coroutine launchRoutine;

    [Header("Settings")]
    [SerializeField] private float spawnTime;

    public static bool canAttack = false;

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
}
