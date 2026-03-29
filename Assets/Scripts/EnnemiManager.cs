using UnityEngine;

public class EnnemiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allEnnemies;
    [SerializeField] private float distanceMin = 1000;
    void Update()
    {
        allEnnemies = GameObject.FindGameObjectsWithTag("EnnemiSol");
        foreach (GameObject ennemi in allEnnemies)
        {
            float distance = Vector2.Distance(transform.position, ennemi.transform.position);
            if (distance < distanceMin)
            {
                EnnemiSol ennemiSol = ennemi.GetComponent<EnnemiSol>();
                ennemiSol.isAttacker = true;
                distanceMin = distance;
            }
        }
        
        distanceMin = 1000;
    }
}
