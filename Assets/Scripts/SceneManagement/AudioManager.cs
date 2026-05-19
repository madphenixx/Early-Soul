using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public static AudioManager instance;

    [SerializeField] private AudioSource[] musicSource;

    [SerializeField] private AudioSource[] sfxSource;

    public float musicVolume;
    public float sfxVolume;

    private void Update()
    {
        for (int i = 0; i < sfxSource.Length; i++)
        {
            //musicSource[i].volume = PlayerPrefs.GetFloat("volume");
        }

        for (int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i].volume = PlayerPrefs.GetFloat("SFXvolume");
        }
    }
}