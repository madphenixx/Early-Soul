using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExplosionDestroy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float lifetime = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Destroy());
       if (SceneManager.GetActiveScene().name == "DeathScreen" || SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifetime);

        Destroy(gameObject);
    }
}
