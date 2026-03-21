using UnityEngine;

public class EnnemiSol : ClassEnnemi
{
    [SerializeField] private GameObject barreVie;
    [SerializeField] private GameObject meleeRange;

    public static bool tookDamage = false;
    
    public static Vector2 spawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tookDamage = false;
        barreVie = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        barreVie.GetComponent<RectTransform>().anchoredPosition = new Vector3(transform.position.x + 1.93f, transform.position.y + 10, 0);

        if (tookDamage)
        {
            Move(2);
            tookDamage = false;
        }
    }

    void MeleeAttack()
    {
        spawnPos = new Vector2(transform.position.x, transform.position.y);
        Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);
    }

    void Move(float distance)
    {
        transform.position += new Vector3(distance, 0, 0);
    }
}
