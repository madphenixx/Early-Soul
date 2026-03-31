using Unity.VisualScripting;
using UnityEngine;

public class EnnemiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allEnnemies;
    [SerializeField] private EnnemiSol attacker;
    [SerializeField] private GameObject player;

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

        attacker.isAttacker = true;
        distanceMin = 1000;
    }
}
