using Unity.VisualScripting;
using UnityEngine;

public class GateSpawn : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject gate;

    [Header("Parameters")]
    [SerializeField] private Vector3 gatePos;

    void Start()
    {
        gatePos = new Vector3(-20.25f, -0.25f, 0); 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PrologueManager.dialogueLucyPlay)
        {
            Instantiate(gate, gatePos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
