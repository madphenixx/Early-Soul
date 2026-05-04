using UnityEngine;

public class ArchangelStart : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ArchangelManager.archangelStarted = true;
        }
    }
}
