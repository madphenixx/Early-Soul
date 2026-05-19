using UnityEngine;

public class SeraphStart : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boat"))
        {
            AudioManager.instance.FadeTrack(musiqueScene, musiqueCombat);
            SeraphManager.seraphStarted = true;
            GameManager.canAttack = true;
        }
    }
}
