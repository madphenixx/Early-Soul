using UnityEngine;
using UnityEngine.UI;

public class ClassEnnemi : MonoBehaviour
{
    // public Animator enemyAnimator;

    [Header("Settings")]
    public float pv = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameObject.CompareTag("Ennemi") || gameObject.CompareTag("EnnemiSol"))
        {
            Slider slEnnemi = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();
            slEnnemi.maxValue = pv;
            slEnnemi.value = pv;
        }

        //enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pv <= 0)
        {
            GameManager.score += 50;
            //enemyAnimator.SetTrigger("IsDead");
            //Faudra aussi faire une coroutine pour attendre la fin de l'animation pour mourir
            Destroy(gameObject);
        }
    }
}
