using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    //[SerializeField] private bool isZooming;

    //void FixedUpdate()
    //{
    //    if (isZooming == true)
    //    {
    //        mainCamera.GetComponent<CameraGround>().isFollowing = false;

    //        Vector3 desiredPosition = new Vector3(-58.55f, 3.11f, -3.7f);
    //        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.3f);
    //        mainCamera.transform.position = smoothedPosition;

    //        mainCamera.GetComponent<Camera>().orthographicSize = 2.73f;
    //    }
    //}

    //// Update is called once per frame
    //void LateUpdate()
    //{
    //    if (isZooming && !DialogueManager.dialogueActive)
    //    {
    //        mainCamera.GetComponent<CameraGround>().isFollowing = true;
    //        mainCamera.GetComponent<Camera>().orthographicSize = 5;
    //        isZooming = false;

    //        Destroy(gameObject);
    //    }
    //}

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainCamera.GetComponent<CameraGround>().isZooming = true;
            Destroy(gameObject);
        }
    }
}
