using System.Collections;
using UnityEngine;

public class BakeKujira : MonoBehaviour
{
    [SerializeField] private AudioSource whaleSource;
    [SerializeField] private AudioClip[] whaleSounds;

    private Animator animator;

    [Header("Settings")]
    [SerializeField] private int min = 1;
    [SerializeField] private int max = 15;
    [SerializeField] private float cooldown = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        whaleSource = GetComponent<AudioSource>();
        StartCoroutine(AnimationCoolDown1());
    }


    private IEnumerator AnimationCoolDown1()
    {
        int time = Random.Range(min, max);
        yield return new WaitForSeconds(time);
        
        animator.SetBool("Swim", true);

        StartCoroutine(AnimationCoolDown2());
    }

    private IEnumerator AnimationCoolDown2()
    {
        int randInt = Random.Range(0, whaleSounds.Length);

        whaleSource.clip = whaleSounds[randInt];

        whaleSource.Play();

        yield return new WaitForSeconds(cooldown);

        animator.SetBool("Swim", false);
        
        StartCoroutine(AnimationCoolDown1());
    }
}
