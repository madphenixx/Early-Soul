using UnityEngine;
using UnityEngine.SceneManagement;

public class ThroneManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject bossUI;
    [SerializeField] private Gate gate;
    [SerializeField] private GameObject lightGate;
    [SerializeField] private GameObject[] boss;

    [SerializeField] private GameObject[] interactions;

    [Header("Prefabs")]
    [SerializeField] private GameObject goodbyes;
    

    [Header("Settings")]

    public static bool throneStarted = false;
    private bool endStarted = false;

    void Start()
    {
        throneStarted = false;
        gate.isEnabled = false;

        if (PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex)
        {
            foreach (GameObject interact in interactions)
            {
                interact.SetActive(false);
            }

            bossUI.SetActive(true);
            Boss.canAttack = true;
        }

        else
        {
            foreach (GameObject interact in interactions)
            {
                interact.SetActive(true);
            } 

            bossUI.SetActive(false);
            lightGate.SetActive(false);

            GameManager.canAttack = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex && Boss.canAttack == false)
        {
            GameManager.canAttack = false;
        }

        if (throneStarted == true && DialogueManager.dialogueActive == false && Boss.canAttack == false)
        {
            GameManager.canAttack = true;
            Boss.canAttack = true;

            bossUI.SetActive(true);
        }

        //ennemies = GameObject.FindGameObjectsWithTag("Ennemi");

        boss = GameObject.FindGameObjectsWithTag("Boss");

        if (boss.Length == 0 && endStarted == false && PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex)
        {
            bossUI.SetActive(false);
            End();
        }

        if (boss.Length == 0 && PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex && endStarted == false)
        {
            bossUI.SetActive(false);
            SceneManager.LoadScene("VictoryScreen");
        }  
    }

    private void End()
    {
        endStarted = true;

        Transform playerTr = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 spawnPos = new Vector3(playerTr.position.x, playerTr.position.y, -1);

        Instantiate(goodbyes, spawnPos, Quaternion.identity);

        gate.isEnabled = true;
        lightGate.SetActive(true);

        Debug.Log("this is the end...");

        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("progress") == false)
        {
            PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        }

        if (PlayerPrefs.GetInt("passedCombat") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("passedCombat") == false)
        {
            PlayerPrefs.SetInt("passedCombat", SceneManager.GetActiveScene().buildIndex);
        }
    }
}
