using UnityEngine;

public class CameraGround : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float smoothSpeed = 0.125f;

    public bool isFollowing = true;
    public bool isZooming = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        if (isFollowing == true)
        {
            Vector3 desiredPosition = new Vector3(player.transform.position.x + 6f, player.transform.position.y + 2f, -10);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            gameObject.GetComponent<Camera>().orthographicSize = 5;
        }

        if (isZooming == true)
        {
            isFollowing = false;

            Vector3 desiredPosition = new Vector3(-58.55f, 3.11f, -3.7f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * 0.4f);
            transform.position = smoothedPosition;

            gameObject.GetComponent<Camera>().orthographicSize = 2.73f;
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
