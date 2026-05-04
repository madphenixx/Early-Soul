using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SeraphManager : MonoBehaviour
{
    [SerializeField] private GameObject tutoObject;
    [SerializeField] private GameObject[] ennemies;
    [SerializeField] private GameObject[] interactions;
    [SerializeField] private GameObject seraph;

    [SerializeField] private int maxVagues = 3;
    private int currentVague = 1;
    [SerializeField] private int maxEnnemies = 5;
    private int currentEnnemiesNumber = 0;
    [SerializeField] private int maxX = 130;
    [SerializeField] private int minX = 86;
    [SerializeField] private int maxY = 15;
    [SerializeField] private int minY = -11;

    public static bool seraphStarted = false;
    private bool endStarted = false;

    void Start()
    {
        seraphStarted = false;
        endStarted = false;

        if (PlayerPrefs.GetInt("progress") >= SceneManager.GetActiveScene().buildIndex)
        {
            tutoObject.SetActive(false);
            foreach (GameObject interact in interactions)
            {
                interact.SetActive(false);
            }

            EnnemiVol.canAttack = true;
        }

        else
        {
            foreach (GameObject interact in interactions)
            {
                interact.SetActive(true);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (seraphStarted == true && DialogueManager.dialogueActive == false && EnnemiVol.canAttack == false)
        {
            EnnemiVol.canAttack = true;

            tutoObject.SetActive(true);
            Time.timeScale = 0f;
        }

        ennemies = GameObject.FindGameObjectsWithTag("Ennemi");

        if (ennemies.Length == 0 && currentVague < 3)
        {
            currentVague += 1;
            NewVague();
        }

        if (ennemies.Length == 0 && currentVague >= maxVagues && endStarted == false && PlayerPrefs.GetInt("viewedTutos") <= SceneManager.GetActiveScene().buildIndex)
        {
            End();
        }

        if (ennemies.Length == 0 && currentVague >= maxVagues && endStarted == false && PlayerPrefs.GetInt("viewedTutos") > SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene("VictoryScreen");
        }  
    }

    private void NewVague()
    {
        while (currentEnnemiesNumber < maxEnnemies)
        {
            currentEnnemiesNumber += 1;
            float spawnX = Random.Range(minX, maxX);
            float spawnY = Random.Range(minY, maxY);

            Vector3 spawnPos = new Vector3(spawnX, spawnY);

            Instantiate(seraph, spawnPos, Quaternion.identity);
        }
    }

    private void End()
    {
        endStarted = true;
        Debug.Log("this is the end...");

        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("progress") == false)
        {
            PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        }
    }
}
