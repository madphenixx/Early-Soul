using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioSource spawnVirtue;

    [Header("UI Elements")]
    [SerializeField] private Slider slBoss;
    [SerializeField] private GameObject bossUi;

    [Header("Prefabs")]
    [SerializeField] private GameObject projectileAOE;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject virtue;

    [Header("Debug: detection")]
    [SerializeField] private GameObject player;

    private Vector3 spawnPos;
    private Color p2Color;

    private ClassEnnemi classEnnemi;
    private Coroutine attackRoutine;
    private Coroutine spawnRoutine;

    private int randInt;

    [Header("Settings: Defense")]
    [SerializeField] private int defenseAttack = 1;
    [SerializeField] private float distanceDefState = 10;
    [SerializeField] private float defensePhaseStart = 10;

    [Header("Settings: Resistance")]
    public float resistanceMelee = 1;
    public float resistanceDistance = 0.1f;
    [SerializeField] private float resistanceFinisher = 2;
    [SerializeField] private float startP2 = 10;

    [Header("Settings: Speed")]
    [SerializeField] private float aoeSpawnTime = 1;
    [SerializeField] private float minProjTime = 1.5f;
    [SerializeField] private float maxProjTime = 3f;
    [SerializeField] private float minSpawnTime = 5f;
    [SerializeField] private float maxSpawnTime = 10f;
    [SerializeField] private float minAoeSpawnTimeP2 = 0f;
    [SerializeField] private float maxAoeSpawnTimeP2 = 3f;

    [Header("Settings: Distance")]
    [SerializeField] private float minSpawnDistance = 6f;
    [SerializeField] private float maxSpawnDistance = 9f;
    [SerializeField] private float spawnDistanceY = 0.2f;
    [SerializeField] private float aoeDistanceY = 0.5150235f;
    
    [Header("Debug: count")]
    public float damageCount;
    [SerializeField] private float defenseCount;
    // [SerializeField] private float virtueCount;

    [Header("Debug: state")]
    [SerializeField] private string currentState = null;
    private readonly string stateAttack = "Attack";
    private readonly string stateDefense = "Defense";
    private readonly string stateP2 = "Phase 2";

    [SerializeField] private bool phase2Test = false;
    public static bool canAttack = false;

    void Awake()
    {
        spawnVirtue.Stop();
        currentState = null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        classEnnemi = gameObject.GetComponent<ClassEnnemi>();
        slBoss.maxValue = classEnnemi.pv;
        slBoss.value = classEnnemi.pv;

        if (UnityEngine.ColorUtility.TryParseHtmlString("#CAC4E4", out Color color))
        {
            p2Color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == null && canAttack == true)
        {
            currentState = stateAttack;
        }

        if (phase2Test)
        {
            currentState = stateP2;
        }

        if (currentState == stateAttack)
        {
            defenseCount = 0;
            AttackState();
        }

        else if (currentState == stateDefense)
        {
            DefenseState();
        }

        else if (currentState == stateP2)
        {
            defenseCount = 0;
            P2State();
        }
    }

    void AttackState()
    {
        if (classEnnemi.pv <= startP2)
        {
            currentState = stateP2;
        }

        else if (damageCount >= defensePhaseStart)
        {
            currentState = stateDefense;
        }

        randInt = Random.Range(1, 101);

        if (randInt < 50) //AOE
        {
            if (attackRoutine == null)
            {
                attackRoutine = StartCoroutine(LaunchAOE());
            }
        }

        else if (randInt > 90) //Spawn
        {
            if (spawnRoutine == null)
            {
                spawnRoutine = StartCoroutine(SpawnVirtue());
            }
        }

        else //Proj
        {
            if (attackRoutine == null)
            {
                attackRoutine = StartCoroutine(LaunchProjectile());
            }
        }
    }

    void DefenseState()
    {
        if (defenseCount == 0)
        {
            damageCount = 0;
            player.GetComponent<PlayerMovement>().TookDamage(gameObject, distanceDefState);

            if (GameManager.parrying == false)
            {
                GameManager.pv += - defenseAttack;
                GameManager.pvSlider.value = GameManager.pv;

                GameManager.score = GameManager.score - 10;
                GameManager.scoreText.text = "Score: " + GameManager.score.ToString();
            }

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x1";
            defenseCount += 1;
        }

        currentState = stateAttack;
    }

    void P2State()
    {
        // GetComponent<SpriteRenderer>().color = p2Color;
        resistanceMelee = resistanceFinisher;

        if (attackRoutine == null)
        {
            attackRoutine = StartCoroutine(LaunchAOEP2());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            currentState = stateDefense;
        }
    }

    private IEnumerator LaunchAOE()
    {
        yield return new WaitForSeconds(aoeSpawnTime);

        spawnPos = new Vector3(gameObject.transform.position.x, aoeDistanceY, 0.5f);
        Instantiate(projectileAOE, spawnPos, Quaternion.identity);
        GameObject projDroit = Instantiate(projectileAOE, spawnPos, Quaternion.identity);

        projDroit.GetComponent<AOEProjectileBoss>().isLeftOne = false;

        attackRoutine = null;
    }

    private IEnumerator LaunchAOEP2()
    {
        float spawnTime = Random.Range(minAoeSpawnTimeP2, maxAoeSpawnTimeP2);
        yield return new WaitForSeconds(spawnTime);

        spawnPos = new Vector3(gameObject.transform.position.x, aoeDistanceY, 0.5f);
        GameObject projGauche = Instantiate(projectileAOE, spawnPos, Quaternion.identity);
        GameObject projDroit = Instantiate(projectileAOE, spawnPos, Quaternion.identity);

        projDroit.GetComponent<AOEProjectileBoss>().isLeftOne = false;

        projGauche.GetComponent<SpriteRenderer>().color = p2Color;
        projDroit.GetComponent<SpriteRenderer>().color = p2Color;

        attackRoutine = null;
    }

    private IEnumerator LaunchProjectile()
    {
        float spawnTime = Random.Range(minProjTime, maxProjTime);
        yield return new WaitForSeconds(spawnTime);

        spawnPos = new Vector3(gameObject.transform.position.x, aoeDistanceY, 0.5f);
        Instantiate(projectile, spawnPos, Quaternion.identity);

        attackRoutine = null;
    }

    private IEnumerator SpawnVirtue()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);

        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        if (player.transform.position.x < transform.position.x)
        {
            spawnPos = new Vector3(gameObject.transform.position.x - distance, spawnDistanceY, 0.5f);
        }

        else if (player.transform.position.x >= transform.position.x)
        {
            spawnPos = new Vector3(gameObject.transform.position.x + distance, spawnDistanceY, 0.5f);
        }

        Instantiate(virtue, spawnPos, Quaternion.identity);
        spawnVirtue.Play();
        spawnRoutine = null;
    }

    void OnDestroy()
    {
        currentState = null;
        canAttack = false;
    }
}
