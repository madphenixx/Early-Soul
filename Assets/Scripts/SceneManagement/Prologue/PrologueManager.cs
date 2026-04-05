using UnityEngine;
using System.Collections;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueLucy;
    [SerializeField] private GameObject dialogueCanvas;

    [SerializeField] private Vector3 spawnPos;

    public static bool soulCollected;
    public static bool dialogueLucyPlay;

    void FixedUpdate()
    {
        if (soulCollected == true)
        {
            soulCollected = false;

            Transform playerTr = GameObject.Find("Player").GetComponent<Transform>();
            spawnPos = new Vector3(playerTr.position.x, playerTr.position.y);

            Instantiate(dialogueLucy, spawnPos, Quaternion.identity);
            dialogueLucyPlay = true;
        }
    }
}
