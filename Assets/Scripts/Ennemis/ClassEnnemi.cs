using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClassEnnemi : MonoBehaviour
{
    // public Animator enemyAnimator;

    private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    public float pv = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (gameObject.CompareTag("Ennemi") || gameObject.CompareTag("EnnemiSol"))
        {
            Slider slEnnemi = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();
            slEnnemi.maxValue = pv;
            slEnnemi.value = pv;
        }

        //enemyAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            StartCoroutine(BlinkingDamage());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pv <= 0 && gameObject.CompareTag("Ennemi"))
        {
            GameManager.score += 50;
            //enemyAnimator.SetTrigger("IsDead");
            //Faudra aussi faire une coroutine pour attendre la fin de l'animation pour mourir
            Destroy(gameObject);
        }
    }

    private IEnumerator BlinkingDamage()
    {
        spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.2f);

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
    }
}
