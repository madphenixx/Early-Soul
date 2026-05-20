using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ArchangelManager : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioSource musiqueCombat;
    [SerializeField] private AudioSource musiqueScene;

    [Header("Objects")]
    [SerializeField] private GameObject tutoObject;
    [SerializeField] private Gate gate;
    [SerializeField] private GameObject[] interactions;
    private GameObject[] ennemies;

    [SerializeField] private Transform player;

    [Header("Prefabs")]
    [SerializeField] private GameObject postFightDialogue;
    [SerializeField] private GameObject archangel;

    [Header("Settings")]
    [SerializeField] private int maxVagues = 3;
    private int currentVague = 1;
    public static bool archangelStarted = false;
    private bool endStarted = false;
    [SerializeField] private int maxEnnemies = 5;
    private int currentEnnemiesNumber = 0;

    private void Awake()
    {
        musiqueCombat.Stop();
        musiqueScene.Stop();
    }

    void Start()
    {
        archangelStarted = false;
        gate.isEnabled = false;
        player = GameObject.Find("Player").GetComponent<Transform>();

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
            musiqueScene.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ennemies = GameObject.FindGameObjectsWithTag("EnnemiSol");

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

        if (ennemies.Length == 0 && currentVague < maxVagues)
        {
            currentVague += 1;
            NewVague();
        }

        if (ennemies.Length == 0 && currentVague >= maxVagues  && endStarted == false && PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex)
        {
            End();
        }

        if (ennemies.Length == 0 && currentVague >= maxVagues && endStarted == false && PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene("VictoryScreen");
        }  
    }

    private void NewVague()
    {
        while (currentEnnemiesNumber < maxEnnemies)
        {
            currentEnnemiesNumber += 1;
            float spawnX = Random.Range(player.position.x + 7, player.position.x + 11);

            Vector3 spawnPos = new Vector3(spawnX, 0.36f);

            Instantiate(archangel, spawnPos, Quaternion.identity);
        }
    }

    private void End()
    {
        AudioManager.instance.SwapTrack(musiqueCombat,musiqueScene);

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
