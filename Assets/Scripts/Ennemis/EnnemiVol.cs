using UnityEngine;
using System.Collections;

public class EnnemiVol : ClassEnnemi
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject barreVie;
    
    private Vector2 spawnPos;
    [SerializeField] private float spawnTime;

    void Start()
    {
        StartCoroutine(LaunchProjectiles());
        barreVie = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        barreVie.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, gameObject.transform.position.y + 2, 0);
    }

    public IEnumerator LaunchProjectiles()
    {
        while (true)
        {
            spawnTime = Random.Range(Time.deltaTime, 1.7f);
            yield return new WaitForSeconds(spawnTime);
            
            spawnPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Instantiate(projectile, spawnPos, Quaternion.identity);
        }
    }
}
