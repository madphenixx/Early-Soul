using UnityEngine;
using System.Collections;

public class ProjectileBoss : MonoBehaviour
{
    private GameObject cible;

    [SerializeField] private AudioSource projBoss;
    private Rigidbody2D rb;
    private Vector2 launchDir;
    private Vector2 launchDirNorm;

    [Header("Settings")]
    public int baseAttack = 4;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float duration = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        cible = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ProjectileDestroy());

        launchDir = cible.transform.position - gameObject.transform.position;
        launchDirNorm = launchDir.normalized;

        projBoss.Play();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = launchDirNorm * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerMovement.isInvicible == false)
        {
            GameManager.pv += - baseAttack;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo : " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x1";

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score : " + GameManager.score.ToString();

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ProjectileDestroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
