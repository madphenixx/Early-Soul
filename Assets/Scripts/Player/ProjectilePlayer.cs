using UnityEngine;
using UnityEngine.UI;

public class ProjectilePlayer : MonoBehaviour
{
    [SerializeField] private GameObject cible;
    private Rigidbody2D rb;
    
    private Vector2 launchDir;
    private Vector2 launchDirNorm;
    [SerializeField] private float speed = 10;

    [SerializeField] private GameObject[] allEnnemies;
    [SerializeField] private float distanceMin = 20;

    private GameManager gameManager;


    void Awake() // Voir si faut pas mettre l'évélutation de la distance dans un autre void
    {
        allEnnemies = GameObject.FindGameObjectsWithTag("Ennemi");
        foreach (GameObject ennemi in allEnnemies)
        {
            float distance = Vector2.Distance(transform.position, ennemi.transform.position);
            if (distance < distanceMin)
            {
                cible = ennemi;
                distanceMin = distance;
            }
        }

        allEnnemies = GameObject.FindGameObjectsWithTag("EnnemiSol");
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

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            int produit = 1 + (GameManager.combo/5); //A modifier et équilibrer (multiplicateur de combo)}
            Slider slEnnemi = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();

            collision.gameObject.GetComponent<ClassEnnemi>().pv += - produit;
            slEnnemi.value = collision.gameObject.GetComponent<ClassEnnemi>().pv;

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();

            GameManager.score = GameManager.score + 10 * produit;
            // gameManager.AddScoreAdd(10 * produit, true);
            GameManager.scoreText.text = "Score: " + GameManager.score.ToString();

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("EnnemiSol"))
        {
            Vector3 returnBase = PlayerAttack.spawnPos;
            launchDir = returnBase - gameObject.transform.position;
            launchDirNorm = launchDir.normalized;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.pv +=  - 1;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score: "+ GameManager.score.ToString();
            
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = launchDirNorm * speed;
    }
    
}
