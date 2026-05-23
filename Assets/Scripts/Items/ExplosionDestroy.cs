using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExplosionDestroy : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource explosion;
    [SerializeField] private AudioClip[] explosionsRand;

    [Header("Settings")]
    [SerializeField] private float lifetime = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosion = GetComponent<AudioSource>();
        int randInt = Random.Range(0, explosionsRand.Length);
        explosion.clip = explosionsRand[randInt];
        explosion.Play();
        StartCoroutine(Destroy());

        if (SceneManager.GetActiveScene().name == "DeathScreen" || SceneManager.GetActiveScene().name == "VictoryScreen")
        {
            Destroy(gameObject);
        }
        explosion.volume = PlayerPrefs.GetFloat("SFXvolume");
    }

    void Update()
    {
        explosion.volume = PlayerPrefs.GetFloat("SFXvolume");
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifetime);

        Destroy(gameObject);
    }
}
