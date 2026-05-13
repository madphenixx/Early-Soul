using UnityEngine;
using System.Collections;

public class ThroneStart : MonoBehaviour
{
    //[SerializeField] private GameObject throne;
    private Coroutine shake;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ThroneManager.throneStarted == false)
        {
            ThroneManager.throneStarted = true;
            //if (shake == null)
            //{
            //    shake = StartCoroutine(BossStart());
            //}

            CameraGround camera = GameObject.Find("Main Camera").GetComponent<CameraGround>();
            camera.isShaking = true;
        }
    }

    private IEnumerator BossStart()
    {
        Debug.Log("raaaaaaa");
        //throne.SetActive(true);
        yield return new WaitForSeconds(1);
        Debug.Log("fffffffffffffffff");
        CameraGround camera = GameObject.Find("Main Camera").GetComponent<CameraGround>();
        camera.isShaking = true;
    }
}
