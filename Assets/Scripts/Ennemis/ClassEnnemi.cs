using UnityEngine;

public class ClassEnnemi : MonoBehaviour
{
    // public Animator enemyAnimator;
    public float pv = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
