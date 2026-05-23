using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource death;

    //Ne pas oublier d'équilibrer et de mettre à jour la valeur max du slider
    public static Slider pvSlider;
    public static Text scoreText;
    public static Text comboText;
    public static Text multiplicateurText;

    [Header("Prefabs")]
    [SerializeField] private GameObject bonusPrefab;
    [SerializeField] private GameObject triggerPrefab;
    [SerializeField] private GameObject gatePrefab;

    [SerializeField] private Animator playerAnimator;

    private SpriteRenderer spriteRenderer;

    private Coroutine comboRoutine;
    private Coroutine deathRoutine;

    [Header("Settings")]
    [SerializeField] private float maxPv = 10;
    [SerializeField] private float comboMaxDuration = 5;
    public static float pv;
    public static int score;
    public static int combo;
    private int comboArch = 0;

    public static bool movementAllowed = true;
    public static bool canAttack = true;
    public static bool parrying = false;

    void Awake()
    {
        PlayerMovement.isInvicible = false;
        canAttack = true;
        movementAllowed = true;
        pv = 10;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("savedScene", SceneManager.GetActiveScene().buildIndex);

        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        spriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        pvSlider = GameObject.Find("PVPlayer").GetComponent<Slider>();
        pvSlider.maxValue = maxPv;
        pvSlider.value = maxPv;
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        comboText = GameObject.Find("Combo").GetComponent<Text>();
        multiplicateurText = GameObject.Find("Multiplicateur").GetComponent<Text>();

        score = 0;
        combo = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("currentScore", score);

        if (pv <= 0 && deathRoutine == null && SceneManager.GetActiveScene().buildIndex != 2)
        {
            deathRoutine = StartCoroutine(DeathPlayer());
            
            // pv = 0;
            // SceneManager.LoadScene("DeathScreen");
        }

        else if (pv <= 0 && deathRoutine == null)
        {
            pv = 0;
            SceneManager.LoadScene("DeathScreen");
        }

        if (pv > maxPv)
        {
            pv = maxPv;
        }

        if (score < 0)
        {
            score = 0;
            scoreText.text = "Score : " + score.ToString();
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
    }

    private IEnumerator comboTime()
    {
        if (combo == comboArch)
        {
            yield return new WaitForSeconds(comboMaxDuration);

            combo = 0;
            comboText.text = "Combo : 0";
            multiplicateurText.text = "x1";
            comboArch = combo;
        }

        else
        {
            yield return null;
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

    private IEnumerator DeathPlayer()
    {
        movementAllowed = false;
        canAttack = false;
        PlayerMovement.isInvicible = true;

        playerAnimator.SetTrigger("isDead");

        death.Play();

        yield return new WaitForSeconds(1.5f);

        pv = 0;
        SceneManager.LoadScene("DeathScreen");
    }
}
