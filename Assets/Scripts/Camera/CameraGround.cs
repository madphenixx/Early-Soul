using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraGround : MonoBehaviour
{
    [Header("Debug: detection")]
    [SerializeField] private GameObject player;

    private Coroutine shakeTime;
    private Coroutine shaker;

    [Header("Settings")]
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] float shakeForce = 0.05f;
    [SerializeField] private float shakeTimer = 1;
    [SerializeField] private float time = 0;

    [Header("Debug: booleans")]
    public bool isFollowing = true;
    public bool isZooming = false;
    public bool isShaking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        //time = 0;
    }

    void FixedUpdate()
    {
        if  (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (isFollowing == true)
            {
                //time = 0;
                Vector3 desiredPosition = new Vector3(player.transform.position.x + 5.1f, player.transform.position.y + 0.81f, - 10);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;


                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, 6, Time.deltaTime);
            }

            if (isZooming == true)
            {
                isFollowing = false;

                Vector3 desiredPosition = new Vector3(15, 2.730344f, 2.749996f);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * 0.05f);
                transform.position = smoothedPosition;

                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, 4, Time.deltaTime);

                //if (time <= 0)
                //{
                //    time = Time.time;
                //}

                //while (time < 4)
                //{
                //    gameObject.GetComponent<Camera>().orthographicSize = 6-time;
                //}
            }
        }

        else if (isShaking == false)
        {
            Vector3 desiredPosition = new Vector3(player.transform.position.x + 3.5f, player.transform.position.y + 2.7f, - 10);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            gameObject.GetComponent<Camera>().orthographicSize = 5;
        }

        else if (isShaking == true)
        {
            isFollowing = false;

            if (shakeTime == null)
            {
                StartCoroutine(ShakeTime());
            }
           
            if (shaker == null)
            {
                StartCoroutine(Shaker1());
            }
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

    private IEnumerator ShakeTime()
    {
        yield return new WaitForSeconds(shakeTimer);
        
        isShaking = false;
        isFollowing = true;
        StopAllCoroutines();
    }

    private IEnumerator Shaker1()
    {
        transform.position += new Vector3(shakeForce, 0, 0);

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(Shaker2());
    }

    private IEnumerator Shaker2()
    {
        transform.position = new Vector3(player.transform.position.x + 3.5f, player.transform.position.y + 2.7f, - 10);

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(Shaker1());
    }
}
