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

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.SwapTrack(musiqueScene, musiqueCombat);
            ArchangelManager.archangelStarted = true;
        }
    }
}
