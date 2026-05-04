using UnityEngine;

public class ThroneStart : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ThroneManager.throneStarted = true;
            CameraGround camera = GameObject.Find("Main Camera").GetComponent<CameraGround>();
            camera.isShaking = true;
        }
    }
}
