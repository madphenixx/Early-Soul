using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Ne pas oublier d'équilibrer et de mettre à jour la valeur max du slider
    public static Slider pvSlider;
    public static Text scoreText;
    public static Text comboText;
    public static Text multiplicateurText;

    [Header("Prefabs")]
    [SerializeField] private GameObject bonusPrefab;
    [SerializeField] private GameObject triggerPrefab;
    [SerializeField] private GameObject gatePrefab;

    private Coroutine comboRoutine;

    [Header("Settings")]
    [SerializeField] private float maxPv;
    [SerializeField] private float comboMaxDuration = 5;
    public static float pv;
    public static int score;
    public static int combo;
    private int comboArch = 0;

    public static bool movementAllowed = true;
    public static bool parrying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("savedScene", SceneManager.GetActiveScene().buildIndex);
        
        if (PlayerPrefs.GetInt("progress") < SceneManager.GetActiveScene().buildIndex || PlayerPrefs.HasKey("progress") == false)
        {
            PlayerPrefs.SetInt("progress", SceneManager.GetActiveScene().buildIndex);
        }

        pvSlider = GameObject.Find("PVPlayer").GetComponent<Slider>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        comboText = GameObject.Find("Combo").GetComponent<Text>();
        multiplicateurText = GameObject.Find("Multiplicateur").GetComponent<Text>();

        maxPv = 10;
        pv = 10;
        score = 0;
        combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("currentScore", score);

        if (pv <= 0)
        {
            pv = 0;
            SceneManager.LoadScene("DeathScreen");
        }

        if (pv > maxPv)
        {
            pv = maxPv;
        }

        comboCheck();
    }

    private void comboCheck()
    {
        if (comboRoutine == null)
        {
            comboArch = combo;
            comboRoutine = StartCoroutine(comboTime());
        }

        // else
        // {
        //     StopCoroutine(comboRoutine);
        //     comboRoutine = null;
        //     comboArch = combo;
        // }    
    }

    private IEnumerator comboTime()
    {
        while (combo == comboArch)
        {
            yield return new WaitForSeconds(comboMaxDuration);

            combo = 0;
            comboText.text = "Combo: 0";
            multiplicateurText.text = "x1";
            comboArch = combo;
        }
    }

    public void SpawnBonus(Vector2 spawnPos) //On l'utilisera en mode "si le combat est terminé et que la scéne est la l°blabla, on faitt swpawn à cette position
    {
        Instantiate(bonusPrefab, spawnPos, Quaternion.identity);
    }

    public void SpawnDialogueTrigger(Vector2 spawnPos) //On l'utilisera en mode "si le combat est terminé et que la scéne est la l°blabla, on faitt swpawn à cette position
    {
        Instantiate(triggerPrefab, spawnPos, Quaternion.identity);
    }

    public void SpawnGate(Vector2 spawnPos) //On l'utilisera en mode "si le combat est terminé et que la scéne est la l°blabla, on faitt swpawn à cette position
    {
        Instantiate(gatePrefab, spawnPos, Quaternion.identity);
    }
}
