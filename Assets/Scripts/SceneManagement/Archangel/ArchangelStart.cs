using UnityEngine;

public class ArchangelStart : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioSource musiqueCombat;
    [SerializeField] private AudioSource musiqueScene;
    private void Awake()
    {
        musiqueCombat.Stop();
        musiqueScene.Stop();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.FadeTrack(musiqueScene, musiqueCombat);
            ArchangelManager.archangelStarted = true;
        }
    }
}
