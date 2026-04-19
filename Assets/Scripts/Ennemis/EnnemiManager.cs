using Unity.VisualScripting;
using UnityEngine;

public class EnnemiManager : MonoBehaviour
{
    [Header("Debug: detection")]
    [SerializeField] private GameObject[] allEnnemies;
    [SerializeField] private GameObject player;

    [Header("Debug: attack")]
    [SerializeField] private EnnemiSol attacker;

    [Header("Settings")]
    [SerializeField] private float distanceMin = 1000;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        allEnnemies = GameObject.FindGameObjectsWithTag("EnnemiSol");
        foreach (GameObject ennemi in allEnnemies)
        {
            float distance = Vector2.Distance(player.transform.position, ennemi.transform.position);
            if (distance < distanceMin)
            {
                EnnemiSol ennemiSol = ennemi.GetComponent<EnnemiSol>();
                ennemiSol.isAttacker = true;
                
                if (attacker is not null)
                {
                    attacker.isAttacker = false;
                }

                attacker = ennemiSol;
                distanceMin = distance;
            }
        }

        if (attacker != null)
        {
            attacker.isAttacker = true;
            distanceMin = 1000;
        }
    }
}
