using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Virtue : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject explosion;
    
    private Vector2 spawnPos;

    [Header("Settings")]
    [SerializeField] private float spawnTime = 4;

    void Start()
    {
        StartCoroutine(LaunchProjectiles());
    }

    private IEnumerator LaunchProjectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            
            Instantiate(projectile, spawnPos, Quaternion.identity);
            GameObject projDroit = Instantiate(projectile, spawnPos, Quaternion.identity);

            projDroit.GetComponent<ProjectileVirtue>().isLeftOne = false;
        }
    }
}
