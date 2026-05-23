using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjCinématique : MonoBehaviour
{
    [SerializeField] private AudioSource sound;
    [SerializeField] private GameObject explosion;
    private GameObject cible;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 launchDir;
    private Vector2 launchDirNorm;

    private Coroutine coroutine = null;

    [Header("Settings")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float time;

    void Start()
    {
        cible = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (cible.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }

        sound = GetComponent<AudioSource>();
        sound.Play();
        launchDir = cible.transform.position - gameObject.transform.position;
        launchDirNorm = launchDir.normalized;
        sound.volume = PlayerPrefs.GetFloat("SFXvolume");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = launchDirNorm * speed;
    }

    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("SFXvolume");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boat") && coroutine == null)
        {
            Vector3 spawnPosEx = new Vector3(transform.position.x - 2f, transform.position.y);
            Instantiate(explosion, spawnPosEx, Quaternion.identity);
            spriteRenderer.enabled = false;
            coroutine = StartCoroutine(NextScene(time));
        }
    }

    private IEnumerator NextScene(float time)
    {
        yield return new WaitForSeconds(time);
        SeraphManager.boatTouched = true;
        Destroy(gameObject);
    }
}
