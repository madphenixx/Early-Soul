using UnityEngine;
using System.Collections;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueLucy;
    [SerializeField] private GameObject gate;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private Vector3 gatePos = new Vector3(-20,-3);

    public static bool soulCollected;
    public static bool dialogueLucyPlay;

    void Start()
    {
        gatePos = new Vector3(-20,-3);
    }
    
    // Update is called once per frame
    void Update()
    {
    //     if (soulCollected == true)
    //     {
    //         Transform playerTr = GameObject.Find("Player").GetComponent<Transform>();
    //         spawnPos = new Vector3(playerTr.position.x, playerTr.position.y);

    //         StartCoroutine(Wait());
    //         Instantiate(dialogueLucy, spawnPos, Quaternion.identity);

    //         soulCollected = false;
    //         dialogueLucyPlay = true;
    //     }

    //     if (dialogueLucyPlay && DialogueManager.dialogueActive == false)
    //     {
    //         Instantiate(gate, gatePos, Quaternion.identity);
    //         dialogueLucyPlay = false;
    //     }
    // }

    // private IEnumerator Wait()
    // {
    //     yield return new WaitForSeconds(0.5f);
    }
}
