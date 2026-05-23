using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeraphManager : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioSource musiqueCombat;
    [SerializeField] private AudioSource musiqueScene;
    [SerializeField] private AudioSource death;

    [Header("Objects")]
    [SerializeField] private GameObject tutoObject;
    [SerializeField] private GameObject seraph;
    [SerializeField] private GameObject seraphCine;
    private GameObject[] ennemies;
    [SerializeField] private GameObject[] interactions;
    [SerializeField] private GameObject gate;

    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] private int maxVagues = 3;
    private int currentVague = 1;
    [SerializeField] private int maxEnnemies = 5;
    private int currentEnnemiesNumber = 0;
    //[SerializeField] private int maxX = 130;
    //[SerializeField] private int minX = 86;
    //[SerializeField] private int maxY = 15;
    //[SerializeField] private int minY = -11;

    public static bool seraphStarted = false;
    public static bool boatTouched = false;
    private bool endStarted = false;
    private void Awake()
    {
        musiqueCombat.Stop();
        musiqueScene.Stop();
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        boatTouched = false;
        seraphStarted = false;
        endStarted = false;

        if (PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex)
        {
            foreach (GameObject interact in interactions)
            {
                interact.SetActive(false);
            }

            EnnemiVol.canAttack = true;
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
        if (seraphStarted == true && DialogueManager.dialogueActive == false && EnnemiVol.canAttack == false && currentVague == 1)
        {
            EnnemiVol.canAttack = true;

            tutoObject.SetActive(true);
            Time.timeScale = 0f;
        }

        if (currentVague == 2)
        {
            EnnemiVol.canAttack = true;
        }

        ennemies = GameObject.FindGameObjectsWithTag("Ennemi");

        if (ennemies.Length == 0 && currentVague < maxVagues)
        {
            currentVague += 1;
            NewVague();
        }

        if (ennemies.Length == 0 && currentVague >= maxVagues && endStarted == false && PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex)
        {
            End();
        }

        if (ennemies.Length == 0 && currentVague >= maxVagues && endStarted == false && PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene("VictoryScreen");
        }  

        if (boatTouched)
        {
            GameManager.movementAllowed = true;
            GameManager.canAttack = true;

            Vector3 spawnPos = new Vector3(player.position.x, player.position.y);

            death.Play();

            Instantiate(gate, spawnPos, Quaternion.identity);
        }
    }

    private void NewVague()
    {
        while (currentEnnemiesNumber < maxEnnemies)
        {
            currentEnnemiesNumber += 1;
            float spawnX = Random.Range(player.position.x + 15, player.position.x + 41);
            float spawnY = Random.Range(11, -5);

            Vector3 spawnPos = new Vector3(spawnX, spawnY);

            Instantiate(seraph, spawnPos, Quaternion.identity);
        }
    }

    private void End()
    {
        endStarted = true;
        Debug.Log("this is the end...");

        GameManager.movementAllowed = false;
        GameManager.canAttack = false;

        StartCoroutine(Endcor());

        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("progress") == false)
        {
            PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        }

        if (PlayerPrefs.GetInt("passedCombat") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("passedCombat") == false)
        {
            PlayerPrefs.SetInt("passedCombat", SceneManager.GetActiveScene().buildIndex);
        }
    }

    private IEnumerator Endcor()
    {
        yield return new WaitForSeconds(3);

        Transform player = GameObject.Find("Player").GetComponent<Transform>();
        Vector3 spawnPos = new Vector3(player.position.x + 20, player.position.y + 5);
        Instantiate(seraphCine, spawnPos, Quaternion.identity);
    }
}
