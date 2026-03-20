using UnityEngine;

public class EnnemiSol : MonoBehaviour
{
    [SerializeField] private GameObject barreVie;

    void Start()
    {
        barreVie = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        barreVie.GetComponent<RectTransform>().anchoredPosition = new Vector3(transform.position.x + 2, transform.position.y + 10, 0);
    }
}
