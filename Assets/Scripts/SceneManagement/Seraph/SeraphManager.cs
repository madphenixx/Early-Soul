using UnityEngine;
using UnityEngine.UIElements;

public class SeraphManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ennemies;
    [SerializeField] private GameObject seraph;

    [SerializeField] private int maxVagues = 3;
    private int currentVague = 1;
    [SerializeField] private int maxEnnemies = 5;
    private int currentEnnemiesNumber = 0;
    [SerializeField] private int maxX = 130;
    [SerializeField] private int minX = 86;
    [SerializeField] private int maxY = 15;
    [SerializeField] private int minY = -11;

    public static bool seraphStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (seraphStarted == true && DialogueManager.dialogueActive == false)
        {
            EnnemiVol.canAttack = true;
        }

        ennemies = GameObject.FindGameObjectsWithTag("Ennemi");

        if (ennemies.Length == 0 && currentVague < 3)
        {
            currentVague += 1;
            NewVague();
        }
    }

    private void NewVague()
    {
        while (currentEnnemiesNumber < maxEnnemies)
        {
            currentEnnemiesNumber += 1;
            float spawnX = Random.Range(minX, maxX);
            float spawnY = Random.Range(minY, maxY);

            Vector3 spawnPos = new Vector3(spawnX, spawnY);

            Instantiate(seraph, spawnPos, Quaternion.identity);
        }
    }
}
