using System.Collections;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public static AudioManager instance;

    [SerializeField] private AudioSource[] musicSource;

    [SerializeField] private AudioSource[] sfxSource;

    [SerializeField] private float fadeDuration = 0.25f;

    private bool isFading;

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
        StartCoroutine(FadeTrack(prevClip,nextClip));
    }

    public IEnumerator FadeTrack(AudioSource prevClip, AudioSource nextClip)
    {
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