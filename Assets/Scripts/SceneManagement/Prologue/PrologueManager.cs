using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueLucy;
    [SerializeField] private GameObject dialogueCanvas;

    [SerializeField] private Vector3 spawnPos;

    public static bool soulCollected;
    public static bool dialogueLucyPlay;

    void Start()
    {
        PlayerPrefs.SetInt("savedScene", SceneManager.GetActiveScene().buildIndex);
        
        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("progress") == false)
        {
            PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        }
    }

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
