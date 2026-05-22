using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] musicSource;
    [SerializeField] private AudioSource[] sfxSource;

    public float fadeDuration = 1.5f;

    public bool isFading;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (!isFading)
        {
            for (int i = 0; i < musicSource.Length; i++)
            {
                musicSource[i].volume = PlayerPrefs.GetFloat("volume");
            }

            for (int i = 0; i < sfxSource.Length; i++)
            {
                sfxSource[i].volume = PlayerPrefs.GetFloat("SFXvolume");
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameObject[] ennemies = GameObject.FindGameObjectsWithTag("Ennemi");
            for (int i = 0; i < ennemies.Length; i++)
            {
                ennemies[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume");
            }
        }

        GameObject[] arrows = GameObject.FindGameObjectsWithTag("PlayerAttack");
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXvolume");
        }
    }

    public void SwapTrack(AudioSource prevClip, AudioSource nextClip)
    {
        //Debug.Log("aaaaasssssaaaaaaaaa");
        StartCoroutine(FadeTrack(prevClip,nextClip));
        //AudioManager.instance.isFading = true;
        //float timeToFade = AudioManager.instance.fadeDuration;
        //float timeRelaxed = 0;

        //nextClip.Play();

        //while (timeRelaxed < timeToFade)
        //{
        //    nextClip.volume = Mathf.Lerp(0, PlayerPrefs.GetFloat("volume"), timeRelaxed / timeToFade);
        //    prevClip.volume = Mathf.Lerp(PlayerPrefs.GetFloat("volume"), 0, timeRelaxed / timeToFade);
        //    timeRelaxed += Time.deltaTime;
        //}

        //prevClip.Stop();
        //AudioManager.instance.isFading = false;
    }

    public IEnumerator FadeTrack(AudioSource prevClip, AudioSource nextClip)
    {
        //Debug.Log("aaaaaaaaaaaaaa");
        isFading = true;
        float timeToFade = fadeDuration;
        float timeRelaxed = 0;

        nextClip.Play();

        while(timeRelaxed < timeToFade)
        {
            nextClip.volume = Mathf.Lerp(0,PlayerPrefs.GetFloat("volume"), timeRelaxed/timeToFade);
            prevClip.volume = Mathf.Lerp(PlayerPrefs.GetFloat("volume"), 0, timeRelaxed/timeToFade);
            timeRelaxed += Time.deltaTime;
            yield return null;
        }

        prevClip.Stop();
        isFading = false;
    }

    //public void FadeAudio(AudioSource clip)
    //{
    //    StartCoroutine(FadeClip(clip));
    //}

    //public IEnumerator FadeClip(AudioSource clip)
    //{
    //    //Debug.Log("aaaaaaaaaaaaaa");
    //    isFading = true;
    //    float timeToFade = fadeDuration * 0.5f;
    //    float timeRelaxed = 0;

    //    clip.Play();

    //    while (timeRelaxed < timeToFade)
    //    {
    //        clip.volume = Mathf.Lerp(PlayerPrefs.GetFloat("volume"), 0, timeRelaxed / timeToFade);
    //        timeRelaxed += Time.deltaTime;
    //        yield return null;
    //    }

    //    clip.Stop();
    //    isFading = false;
    //}
}