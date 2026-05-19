using UnityEngine;

public class SeraphStart : MonoBehaviour
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
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boat"))
        {
            musiqueScène.Stop();
            musiqueCombat.Play();
            SeraphManager.seraphStarted = true;
            GameManager.canAttack = true;
        }
    }
}
