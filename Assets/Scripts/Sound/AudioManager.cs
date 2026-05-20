using System.Collections;
using UnityEngine;
using System;
using JetBrains.Annotations;

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
}