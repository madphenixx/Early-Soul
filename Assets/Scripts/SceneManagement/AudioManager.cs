using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public static AudioManager instance;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource sfxSource;


    public float musicVolume;
    public float sfxVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //    else
        //    {
        //        Destroy(gameObject);
        //    }
    }

    private void Start()
    {
        musicSource.volume = PlayerPrefs.GetFloat("volume");
        sfxSource.volume = PlayerPrefs.GetFloat("SFXvolume");
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayLoopSFX(AudioClip clip)
    {
        if (sfxSource.isPlaying && sfxSource.clip == clip) return;

        sfxSource.clip = clip;
        sfxSource.loop = true;
        sfxSource.volume = sfxVolume;
        sfxSource.Play();
    }

    public void StopLoopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }
}