using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreAdd : MonoBehaviour
{
    [Header("Lists")]
    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> scoreAdd = new List<GameObject>();
    
    [Header("Prefabs")]
    public GameObject scoreAddPrefab;

    public static Transform scoreTr;

    [Header("Debug: slots")]
    public int nextFreeSlot = 0;
    public int slotCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreTr = GameObject.Find("Score").transform;
        slotCount = slots.Count;
    }

    public void DeleteScoreAdd(GameObject amount)
    {
        scoreAdd.Remove(amount);
        Destroy(scoreTr.GetChild(slotCount).gameObject);
        nextFreeSlot = nextFreeSlot - 1;
    }
        
    public void AddScoreAdd(float value, bool isPositive)
    {
        
        GameObject amount = Instantiate(scoreAddPrefab, scoreTr);
        scoreAdd.Add(amount);
        Text text = amount.GetComponent<Text>();
            
        if (isPositive)
        {
            text.text = "+" + value.ToString();
        }

        else
        {
            text.text = "-" + value.ToString();
        }

        if (scoreAdd.Count > slotCount)
        {
            amount.GetComponent<RectTransform>().anchoredPosition = slots[nextFreeSlot].GetComponent<RectTransform>().anchoredPosition;
            nextFreeSlot = nextFreeSlot + 1;
            StartCoroutine(ScoreTime(amount));
        }
    }

    public IEnumerator ScoreTime(GameObject amount)
    {
        yield return new WaitForSeconds(1);
        DeleteScoreAdd(amount);
    }
}
