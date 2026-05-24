using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioSource musiqueMenu;

    void Awake()
    {
        musiqueMenu.Stop();
    }

    void Start()
    {
        musiqueMenu.Play();
    }
}
