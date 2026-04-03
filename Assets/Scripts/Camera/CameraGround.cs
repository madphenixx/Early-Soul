using UnityEngine;

public class CameraGround : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothSpeed = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.transform.position.x + 6.5f, player.transform.position.y + 2.5f, -10);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
