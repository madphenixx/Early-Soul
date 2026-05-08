using UnityEngine;

public class VolCine : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    void Start()
    {
        Vector2 spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Instantiate(projectile, spawnPos, Quaternion.identity);
    }
}
