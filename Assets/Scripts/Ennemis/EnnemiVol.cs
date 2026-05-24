using UnityEngine;
using System.Collections;
public class EnnemiVol : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource projSource;
    [SerializeField] private AudioClip[] projSounds;

    [Header("Prefabs")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject player;

    [Header("Settings")]
    [SerializeField] private float minDistance = 50f;
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 1.7f;
    [SerializeField] private float health;
    
    private Vector2 spawnPos;

    private Coroutine launchRoutine;

    [Header("Settings")]
    [SerializeField] private float spawnTime;

    public static bool canAttack = false;

    void Awake()
    {
        player = GameObject.Find("Player");
        projSource = GetComponent<AudioSource>();
        projSource.Pause();
        canAttack = false;
        health = gameObject.GetComponent<ClassEnnemi>().pv;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (canAttack == true && launchRoutine == null && distance < minDistance)
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

            int randInt = Random.Range(0, projSounds.Length);
            projSource.clip = projSounds[randInt];
            projSource.Play();
            
            Instantiate(projectile, spawnPos, Quaternion.identity);
        }
    }
}
