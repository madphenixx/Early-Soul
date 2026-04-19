using UnityEngine;
using System.Collections;

public class EnnemiVol : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject projectile;
    
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
            spawnTime = Random.Range(Time.deltaTime, 1.7f);
            yield return new WaitForSeconds(spawnTime);

            spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Instantiate(projectile, spawnPos, Quaternion.identity);
        }
    }
}
