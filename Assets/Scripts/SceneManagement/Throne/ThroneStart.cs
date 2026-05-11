using UnityEngine;
using System.Collections;

public class ThroneStart : MonoBehaviour
{
    //[SerializeField] private GameObject throne;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ThroneManager.throneStarted == false)
        {
            ThroneManager.throneStarted = true;
            StartCoroutine(BossStart());
        }
    }

    private IEnumerator BossStart()
    {
        //throne.SetActive(true);
        yield return new WaitForSeconds(1);
        CameraGround camera = GameObject.Find("Main Camera").GetComponent<CameraGround>();
        camera.isShaking = true;
    }
}
