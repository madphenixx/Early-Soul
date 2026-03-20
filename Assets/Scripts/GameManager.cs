using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //Ne pas oublier d'équilibrer et de mettre à jour la valeur max du slider
    public static Slider pvSlider;
    public static Text scoreText;
    public static Text comboText;
    public static Transform scoreTr;
    [SerializeField] private GameObject bonusPrefab;

    public static float pv;
    [SerializeField] private float maxPv;
    public static int score;
    public static int combo;

    public static bool movementAllowed = true;

    // public List<GameObject> slots = new List<GameObject>();
    // public List<GameObject> scoreAdd = new List<GameObject>();
    // public int nextFreeSlot = 0;
    // public int slotCount;
    // public GameObject scoreAddPrefab;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        pvSlider = GameObject.Find("PVPlayer").GetComponent<Slider>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        comboText = GameObject.Find("Combo").GetComponent<Text>();

        // scoreTr = GameObject.Find("Score").transform;

        maxPv = 10;
        pv = 10;
        score = 0;
        combo = 0;
        // slotCount = slots.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (pv <= 0)
        {
            pv = 0;
            // Deathscreen et on peut reload la scene à laquelle on était avec le bouton retry
            // Garder le même GameManager pendant toute la partie ??? -> Dontdestroyonload
            // Faire une variable currentScene pour pouvoir sauvegarder la dernière scène
        }

        if (pv > maxPv)
        {
            pv = maxPv;
        }
    }

    // public void DeleteScoreAdd(GameObject amount)
    // {
    //     scoreAdd.Remove(amount);
    //     Destroy(scoreTr.GetChild(slotCount).gameObject);
    //     nextFreeSlot = nextFreeSlot - 1;
    // }
        
    // public void AddScoreAdd(float value, bool isPositive)
    // {
        
    //     GameObject amount = Instantiate(scoreAddPrefab, scoreTr);
    //     scoreAdd.Add(amount);
    //     Text text = amount.GetComponent<Text>();
            
    //     if (isPositive)
    //     {
    //         text.text = "+" + value.ToString();
    //     }

    //     else
    //     {
    //         text.text = "-" + value.ToString();
    //     }

    //     if (scoreAdd.Count > slotCount)
    //     {
    //         amount.GetComponent<RectTransform>().anchoredPosition = slots[nextFreeSlot].GetComponent<RectTransform>().anchoredPosition;
    //         nextFreeSlot = nextFreeSlot + 1;
    //         StartCoroutine(ScoreTime(amount));
    //     }
    // }

    // public IEnumerator ScoreTime(GameObject amount)
    // {
    //     yield return new WaitForSeconds(1);
    //     DeleteScoreAdd(amount);
    // }

    public void SpawnBonus(Vector2 spawnPos) //On l'utilisera en mode "si le combat est terminé et que la scéne est la l°blabla, on faitt swpawn à cette position
    {
        Instantiate(bonusPrefab, spawnPos, Quaternion.identity);
    }
}
