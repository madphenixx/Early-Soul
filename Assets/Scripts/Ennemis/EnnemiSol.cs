using UnityEngine;
using System.Collections;

public class EnnemiSol : MonoBehaviour
{
    // [SerializeField] private GameObject barreVie;
    [SerializeField] private GameObject meleeRange;
    
    [SerializeField] private float attackTime;
    public static Vector2 spawnPos;
    [SerializeField] private GameObject player;

    public static bool tookDamage = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tookDamage = false;
        // barreVie = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        StartCoroutine(MeleeAttack());
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //barreVie.GetComponent<RectTransform>().anchoredPosition = new Vector3(transform.position.x, transform.position.y + 10, 0); //à fix

        if (tookDamage)
        {
            Move(2);
            tookDamage = false;
        }
    }

    private IEnumerator MeleeAttack()
    {
        while (true)
        {
            attackTime = Random.Range(Time.deltaTime, 3f);
            yield return new WaitForSeconds(attackTime);

            float distance = Vector2.Distance(player.transform.position, transform.position);
            StartCoroutine(MovementTime(distance - 2));

            spawnPos = new Vector2(transform.position.x, transform.position.y);
            Instantiate(meleeRange, spawnPos, Quaternion.identity, transform);
        }
    }

    private IEnumerator MovementTime(float distance)
    {
        float time = -distance;
        while (time < 0)
        {
            Move(-distance / 10); //a modif pour qu'il glisse pas dans la parade
            time += distance/10;
            yield return null;
        }
    }

    void Move(float distance)
    {
        transform.position += new Vector3(distance, 0, 0);
    }
}
