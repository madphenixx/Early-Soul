using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject dialogueLucy;
    [SerializeField] private GameObject lucy;

    private Coroutine fade;

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

            lucy.SetActive(true);
            SpriteRenderer lucyRe = lucy.GetComponent<SpriteRenderer>();
            if (fade == null)
            {
                fade = StartCoroutine(FadeIn(lucyRe));
            }
            
            Instantiate(dialogueLucy, spawnPos, Quaternion.identity);
            dialogueLucyPlay = true;
        }
    }
    private IEnumerator FadeIn(SpriteRenderer image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < 1)
        {
            yield return null;
            elapsedTime += Time.deltaTime ;
            c.a = Mathf.Clamp01(elapsedTime / 1);
            image.color = c;
        }
    }
}
