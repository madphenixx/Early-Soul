using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

//- []  Différents projectiles
//- []  Spawn sakapatate
//- []  AOE
//- []  Projectiles
//- []  Phase 2 à 1 / 3 de vie
//- []  Nous pousse si’il prend trop de dégats lors d’n laps de temps
//- []  Bouclier ??
//- []  Bouquet final(enchanement d’aoE)
//- []  Différentes résistances(mêlée, distance, finisher)

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject projectileAOE;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject virtue;
    [SerializeField] private ClassEnnemi classEnnemi;

    private Vector2 spawnPos;
    [SerializeField] private float aoeSpawnTime = 1;
    [SerializeField] private float changeTime = 0.5f;
    public float resistanceMelee = 1;
    public float resistanceDistance = 0.5f;
    [SerializeField] private float resistanceFinisher = 2;
    [SerializeField] private float startP2 = 10;
    [SerializeField] private float maxProjTime = 3.5f;
    [SerializeField] private float maxSpawnTime = 2.5f;
    [SerializeField] private float maxSpawnDistance = 4f;
    [SerializeField] private float minSpawnDistance = 0.7f;
    public float damageCount;

    private Coroutine attackRoutine;

    [SerializeField] private string currentState;
    private readonly string stateAttack = "Attack";
    private readonly string stateDefense = "Defense";
    private readonly string stateP2 = "Phase 2";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        classEnnemi = gameObject.GetComponent<ClassEnnemi>();

        currentState = stateAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == stateAttack)
        {
            AttackState();
        }

        else if (currentState == stateDefense)
        {
            DefenseState();
        }

        else if (currentState == stateP2)
        {
            P2State();
        }
    }

    void AttackState()
    {
        if (classEnnemi.pv <= startP2)
        {
            currentState = stateP2;
        }

        else if (damageCount >= 10)
        {
            currentState = stateDefense;
        }

        int randInt = Random.Range(1, 4);

        if (randInt == 1) //AOE
        {
            if (attackRoutine == null)
            {
                attackRoutine = StartCoroutine(LaunchAOE());
            }
        }

        else if (randInt == 2) //Proj
        {
            if (attackRoutine == null)
            {
                attackRoutine = StartCoroutine(LaunchProjectile());
            }
        }

        else if (randInt == 3) //Spawn
        {
            if (attackRoutine == null)
            {
                attackRoutine = StartCoroutine(SpawnVirtue());
            }
        }
    }

    void DefenseState()
    {
        damageCount = 0;
    }

    void P2State()
    {
        resistanceMelee = resistanceFinisher;
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

        spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Instantiate(projectileAOE, spawnPos, Quaternion.identity);
        GameObject projDroit = Instantiate(projectileAOE, spawnPos, Quaternion.identity);

        projDroit.GetComponent<AOEProjectileBoss>().isLeftOne = false;
    }

    private IEnumerator LaunchProjectile()
    {
        float spawnTime = Random.Range(Time.deltaTime, maxProjTime);
        yield return new WaitForSeconds(spawnTime);

        spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        Instantiate(projectile, spawnPos, Quaternion.identity);
    }

    private IEnumerator SpawnVirtue()
    {
        float spawnTime = Random.Range(Time.deltaTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);

        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        int positive = Random.Range(0, 2);

        if (positive == 0)
        {
            spawnPos = new Vector2(gameObject.transform.position.x - distance, gameObject.transform.position.y);
        }

        else
        {
            spawnPos = new Vector2(gameObject.transform.position.x + distance, gameObject.transform.position.y);
        }
        
        Instantiate(virtue, spawnPos, Quaternion.identity);
    }
}
