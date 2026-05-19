using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ArchangelManager : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioSource musiqueCombat;
    [SerializeField] private AudioSource musiqueScène;

    [Header("Objects")]
    [SerializeField] private GameObject tutoObject;
    [SerializeField] private Gate gate;
    [SerializeField] private GameObject[] interactions;
    private GameObject[] ennemies;

    [Header("Prefabs")]
    [SerializeField] private GameObject postFightDialogue;

    public static bool archangelStarted = false;
    private bool endStarted = false;

    private void Awake()
    {
        musiqueCombat.Stop();
        musiqueScène.Stop();
    }

    void Start()
    {
        archangelStarted = false;
        gate.isEnabled = false;

        if (PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex)
        {
            tutoObject.SetActive(false);

            foreach (GameObject interact in interactions)
            {
                interact.SetActive(false);
            }

            EnnemiSol.startAttack = true;
            musiqueCombat.Play();
        }

        else
        {
            foreach (GameObject interact in interactions)
            {
                interact.SetActive(true);
            }

            GameManager.canAttack = false;
            musiqueScène.Play();
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
            musiqueCombat.Stop();
            musiqueScène.Play();
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

        // Instantiate(postFightDialogue, spawnPos, Quaternion.identity);

        gate.isEnabled = true;
        StartCoroutine(EndDialogue());

        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("progress") == false)
        {
            PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        }

        if (PlayerPrefs.GetInt("passedCombat") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("passedCombat") == false)
        {
            PlayerPrefs.SetInt("passedCombat", SceneManager.GetActiveScene().buildIndex);
        }
    }

    private IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(2);

        Transform playerTr = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 spawnPos = new Vector3(playerTr.position.x, playerTr.position.y, -1);

        Instantiate(postFightDialogue, spawnPos, Quaternion.identity);
    }
}
