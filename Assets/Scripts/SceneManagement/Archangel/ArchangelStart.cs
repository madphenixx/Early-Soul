using UnityEngine;

public class ArchangelStart : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioSource musiqueCombat;
    [SerializeField] private AudioSource musiqueScène;
    private void Awake()
    {
        musiqueCombat.Stop();
        musiqueScène.Stop();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            musiqueScène.Stop();
            musiqueCombat.Play();
            ArchangelManager.archangelStarted = true;
        }
    }
}
