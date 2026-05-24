using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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
            Image img = cache.GetComponent<Image>();
            // StartCoroutine(Cache(img));
            img.enabled = true;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(gameObject);
        }
    }

    private IEnumerator Cache(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < 0.2f)
        {
            yield return null;
            elapsedTime += Time.deltaTime ;
            c.a = Mathf.Clamp01(elapsedTime / 0.2f);
            image.color = c;
        }
    }
}
