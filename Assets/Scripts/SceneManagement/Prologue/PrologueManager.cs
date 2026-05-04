using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject dialogueLucy;

    private Vector3 spawnPos;

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
            spawnPos = new Vector3(playerTr.position.x, playerTr.position.y, -1);

            Instantiate(dialogueLucy, spawnPos, Quaternion.identity);
            dialogueLucyPlay = true;
        }
    }
}
