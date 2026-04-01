using UnityEngine;
using System.Collections;

public class Virtue : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    
    private Vector2 spawnPos;
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
