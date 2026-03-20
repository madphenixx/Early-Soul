using UnityEngine;

public class ClassEnnemi : MonoBehaviour
{
    // public Animator enemyAnimator;
    public int pv = 10;
    

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
            //On détruit l'objet, ajoute des points au score et actionne l'animation de mort
            GameManager.score += 50;
            //enemyAnimator.SetTrigger("IsDead");

            //Faudra aussi faire une coroutine pour attendre la fin de l'animation pour mourir
            Destroy(gameObject);
        }
    }
}
