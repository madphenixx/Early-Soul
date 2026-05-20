using UnityEngine;
using System.Collections;

public class BlinkingSprite : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioSource sound;
    
    private SpriteRenderer image;

    [Header("Sprites")]
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Glitch2());
    }

    private IEnumerator Glitch1()
    {
        yield return new WaitForSeconds(0.2f);
        image.sprite = sprite1;

        StartCoroutine(Glitch2());

        //backgroundImage.sprite = sprite2;
        //yield return new WaitForSeconds(10f);
    }

    private IEnumerator Glitch2()
    {
        int time = Random.Range(1, 5);
        yield return new WaitForSeconds(time);
        sound.Play();
        image.sprite = sprite2;
        
        StartCoroutine(Glitch1());
    }
}
