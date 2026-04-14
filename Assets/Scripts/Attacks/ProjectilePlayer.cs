using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProjectilePlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] allEnnemies;
    private GameObject[] allEnnemiesBase;
    private GameObject[] allEnnemiesGround;
    private GameObject[] allEnnemiesBoss;
    private GameObject cible;

    [SerializeField] private GameManager gameManager;
    
    private Vector2 launchDir;
    private Vector2 launchDirNorm;
    private Rigidbody2D rb;
    [SerializeField] private float distanceMin = 40;
    [SerializeField] private float speed = 10;
    [SerializeField] private float duration = 3;

    [SerializeField] private bool boomerang;

    void Awake() // Voir si faut pas mettre l'évélutation de la distance dans un autre void
    {
        allEnnemiesBase = GameObject.FindGameObjectsWithTag("Ennemi");

        allEnnemiesGround = GameObject.FindGameObjectsWithTag("EnnemiSol");

        allEnnemiesBoss = GameObject.FindGameObjectsWithTag("Boss");

        ArrayUtility.AddRange(ref allEnnemies, allEnnemiesBase);
        ArrayUtility.AddRange(ref allEnnemies, allEnnemiesGround);
        ArrayUtility.AddRange(ref allEnnemies, allEnnemiesBoss);

        foreach (GameObject ennemi in allEnnemies)
        {
            float distance = Vector2.Distance(transform.position, ennemi.transform.position);
            if (distance < distanceMin)
            {
                cible = ennemi;
                distanceMin = distance;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ProjectileDestroy());

        if (cible == null)
        {
            Destroy(gameObject);
        }

        else
        {
            launchDir = cible.transform.position - gameObject.transform.position;
            launchDirNorm = launchDir.normalized;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = launchDirNorm * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            //Debug.Log("comboe" + GameManager.combo);
            float produit = 1 + (GameManager.combo * 0.5f); //A modifier et équilibrer (multiplicateur de combo)}*
            //Debug.Log(produit);
            Slider slEnnemi = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x" + produit.ToString();

            collision.gameObject.GetComponent<ClassEnnemi>().pv += -1 * produit;
            slEnnemi.value = collision.gameObject.GetComponent<ClassEnnemi>().pv;
 

            GameManager.score = Mathf.RoundToInt(GameManager.score + 10 * produit);
            // gameManager.AddScoreAdd(10 * produit, true);
            GameManager.scoreText.text = "Score: " + GameManager.score.ToString();

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("EnnemiSol"))
        {
            Destroy(gameObject);
        }

        //if (collision.gameObject.CompareTag("EnnemiSol"))
        //{
        //    boomerang = true;
        //    Vector3 returnBase = PlayerAttack.spawnPos;
        //    launchDir = returnBase - gameObject.transform.position;
        //    launchDirNorm = launchDir.normalized;
        //}

        //if (collision.gameObject.CompareTag("Player") && boomerang == true)
        //{
        //    boomerang = false; 

        //    GameManager.pv +=  - 1;
        //    GameManager.pvSlider.value = GameManager.pv;

        //    GameManager.combo = 0;
        //    GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();

        //    GameManager.score = GameManager.score - 10;
        //    GameManager.scoreText.text = "Score: "+ GameManager.score.ToString();

        //    Destroy(gameObject);
        //}

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            //Debug.Log("comboe" + GameManager.combo);
            float produit = (1 + (GameManager.combo * 0.5f)) * gameObject.GetComponent<Boss>().resistanceDistance; //A modifier et équilibrer (multiplicateur de combo)}*
            //Debug.Log(produit);
            Slider slEnnemi = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x" + produit.ToString();

            collision.gameObject.GetComponent<ClassEnnemi>().pv += -1 * produit;
            slEnnemi.value = collision.gameObject.GetComponent<ClassEnnemi>().pv;


            GameManager.score = Mathf.RoundToInt(GameManager.score + 10 * produit);
            // gameManager.AddScoreAdd(10 * produit, true);
            GameManager.scoreText.text = "Score: " + GameManager.score.ToString();

            collision.gameObject.GetComponent<Boss>().damageCount += produit;
            Destroy(gameObject);
        }
    }

    private IEnumerator ProjectileDestroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
