using UnityEngine;

public class SeraphStart : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SeraphManager.seraphStarted = true;
        }
    }
}
