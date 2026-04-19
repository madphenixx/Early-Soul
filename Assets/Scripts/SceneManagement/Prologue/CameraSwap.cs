using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    private GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainCamera.GetComponent<CameraGround>().isZooming = true;
            mainCamera.GetComponent<CameraGround>().isFollowing = false;
            Destroy(gameObject);
        }
    }
}
