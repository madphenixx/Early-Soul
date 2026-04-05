using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject zoomCamera;

    [SerializeField] private bool isZooming;

    // Update is called once per frame
    void LateUpdate()
    {
        if (isZooming && !DialogueManager.dialogueActive)
        {
            mainCamera.SetActive(true);
            zoomCamera.SetActive(false);
            isZooming = false;

            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            isZooming = true;;
            zoomCamera.SetActive(true);
            mainCamera.SetActive(false);
        }
    }
}
