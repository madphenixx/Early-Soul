using UnityEngine;

public class CameraAir : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float smoothSpeed = 0.15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.transform.position.x + 6, player.transform.position.y, -10);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // transform.position = new Vector3(player.transform.position.x + 6, player.transform.position.y, -10); 
    }
}
