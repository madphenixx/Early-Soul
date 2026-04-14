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
    [SerializeField] private ClassEnnemi classEnnemi;

    private Vector2 spawnPos;
    [SerializeField] private float aoeSpawnTime = 4;
    [SerializeField] private float reactivityTime = 0.5f;
    public float resistanceMelee = 1;
    public float resistanceDistance = 0.5f;
    [SerializeField] private float resistanceFinisher = 2;
    [SerializeField] private float startP2 = 10;

    private Coroutine aoeRoutine;

    [SerializeField] private string currentState;
    private readonly string stateA0E = "AOE";
    private readonly string stateProj = "Projectiles";
    private readonly string stateSpawn = "Virtue";
    private readonly string stateDefense = "Defense";
    private readonly string stateP2 = "Phase 2";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        classEnnemi = gameObject.GetComponent<ClassEnnemi>();
        //currentState = stateApproche;
    }

    // Update is called once per frame
    void Update()
    {
        if (classEnnemi.pv <= startP2)
        {
            currentState = stateP2;
        }

        if (currentState == stateA0E)
        {
            AOEState();
        }

        else if (currentState == stateProj)
        {
            ProjState();
        }

        else if (currentState == stateSpawn)
        {
            SpawnState();
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

    void AOEState()
    {
        if (aoeRoutine == null)
        {
            aoeRoutine = StartCoroutine(LaunchAOE());
        }
    }

    void ProjState()
    {

    }

    void SpawnState()
    {

    }

    void DefenseState()
    {

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
        while (true)
        {
            yield return new WaitForSeconds(aoeSpawnTime);

            spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Instantiate(projectileAOE, spawnPos, Quaternion.identity);
            GameObject projDroit = Instantiate(projectileAOE, spawnPos, Quaternion.identity);

            projDroit.GetComponent<AOEProjectileBoss>().isLeftOne = false;
        }
    }
}
