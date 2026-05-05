using UnityEngine;
using UnityEngine.SceneManagement;

public class ArchangelManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject tutoObject;
    [SerializeField] private Gate gate;
    [SerializeField] private GameObject[] interactions;
    private GameObject[] ennemies;

    [Header("Prefabs")]
    [SerializeField] private GameObject postFightDialogue;

    public static bool archangelStarted = false;
    private bool endStarted = false;

    void Start()
    {
        archangelStarted = false;
        gate.enabled = false;

        if (PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex)
        {
            tutoObject.SetActive(false);

            foreach (GameObject interact in interactions)
            {
                interact.SetActive(false);
            }

            EnnemiSol.startAttack = true;
        }

        else
        {
            foreach (GameObject interact in interactions)
            {
                interact.SetActive(true);
            }

            GameManager.canAttack = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex && EnnemiSol.startAttack == false)
        {
            GameManager.canAttack = false;
        }

        if (archangelStarted == true && DialogueManager.dialogueActive == false  && EnnemiSol.startAttack == false)
        {
            GameManager.canAttack = true;
            EnnemiSol.startAttack = true;

            tutoObject.SetActive(true);
            Time.timeScale = 0f;
        }

        ennemies = GameObject.FindGameObjectsWithTag("EnnemiSol");

        if (ennemies.Length == 0 && endStarted == false && PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex)
        {
            End();
        }

        if (ennemies.Length == 0 && endStarted == false && PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene("VictoryScreen");
        }  
    }

    private void End()
    {
        endStarted = true;
        Debug.Log("this is the end...");

        Transform playerTr = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 spawnPos = new Vector3(playerTr.position.x, playerTr.position.y, -1);

        Instantiate(postFightDialogue, spawnPos, Quaternion.identity);

        gate.enabled = true;

        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("progress") == false)
        {
            PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        }
    }
}
