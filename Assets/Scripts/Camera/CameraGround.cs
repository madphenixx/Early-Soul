using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraGround : MonoBehaviour
{
    [Header("Debug: detection")]
    [SerializeField] private GameObject player;

    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 0.125f;

    [Header("Debug: booleans")]
    public bool isFollowing = true;
    public bool isZooming = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if  (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (isFollowing == true)
            {
                Vector3 desiredPosition = new Vector3(player.transform.position.x + 5.1f, player.transform.position.y + 0.81f, - 10);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;

                gameObject.GetComponent<Camera>().orthographicSize = 6;
            }

            if (isZooming == true)
            {
                isFollowing = false;

                Vector3 desiredPosition = new Vector3(15, 2.730344f, 2.749996f);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * 0.4f);
                transform.position = smoothedPosition;

                gameObject.GetComponent<Camera>().orthographicSize = 4;
            }
        }

        else
        {
            Vector3 desiredPosition = new Vector3(player.transform.position.x + 6f, player.transform.position.y + 2, - 10);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            gameObject.GetComponent<Camera>().orthographicSize = 5;
        }
    }

    void LateUpdate()
    {
        if (isZooming && !DialogueManager.dialogueActive)
        {
            isFollowing = true;
            isZooming = false;
        }
    }
}
