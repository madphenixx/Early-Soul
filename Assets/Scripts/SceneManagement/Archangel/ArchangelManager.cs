using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ArchangelManager : MonoBehaviour
{
    [SerializeField] private GameObject tutoObject;
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject postFightDialogue;
    [SerializeField] private GameObject[] ennemies;
    [SerializeField] private GameObject[] interactions;

    public static bool archangelStarted = false;
    private bool endStarted = false;

    void Start()
    {
        archangelStarted = false;

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
        if (archangelStarted == true && DialogueManager.dialogueActive == false  && EnnemiSol.startAttack == false)
        {
            EnnemiSol.startAttack = true;
            GameManager.canAttack = true;

            tutoObject.SetActive(true);
            Time.timeScale = 0f;
        }

        ennemies = GameObject.FindGameObjectsWithTag("EnnemiSol");

        if (ennemies.Length == 0 && endStarted == false && PlayerPrefs.GetInt("viewedTutos") <= SceneManager.GetActiveScene().buildIndex)
        {
            End();
        }

        if (ennemies.Length == 0 && endStarted == false && PlayerPrefs.GetInt("viewedTutos") > SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene("VictoryScreen");
        }  
    }

    private void End()
    {
        endStarted = true;
        Debug.Log("this is the end...");

        StartCoroutine(EndDialogue());
        gate.SetActive(true);

        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("progress") == false)
        {
            PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        }
    }

    private IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(1);

        Transform playerTr = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 spawnPos = new Vector3(playerTr.position.x, playerTr.position.y, -1);

        Instantiate(postFightDialogue, spawnPos, Quaternion.identity);
    }
}
