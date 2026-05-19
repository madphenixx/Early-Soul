using UnityEngine;
using System.Collections;

public class ThroneStart : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] private AudioSource musiqueCombat;
    [SerializeField] private AudioSource musiqueScène;

    //[SerializeField] private GameObject throne;
    private Coroutine shake;

    private void Awake()
    {
        musiqueCombat.Stop();
        musiqueScène.Stop();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ThroneManager.throneStarted == false)
        {
            musiqueCombat.Play();
            musiqueScène.Pause();

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
