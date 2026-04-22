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

    [Header("Settings")]
    [SerializeField] private float spawnTime;

    void Start()
    {
        StartCoroutine(LaunchProjectiles());
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
