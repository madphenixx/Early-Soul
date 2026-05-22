using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject cache;
    private void Start()
    {
        cache = GameObject.Find("Cache");
    }

    public bool isEnabled = true;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boat")) && isEnabled)
        {
            cache.GetComponent<Image>().enabled = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(gameObject);
        }
    }
}
