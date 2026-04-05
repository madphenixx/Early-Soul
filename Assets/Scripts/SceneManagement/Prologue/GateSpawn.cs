using Unity.VisualScripting;
using UnityEngine;

public class GateSpawn : MonoBehaviour
{
    [SerializeField] private GameObject gate;

    [SerializeField] private Vector3 gatePos;

    void Start()
    {
       gatePos = new Vector3(-23,-1, -1); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PrologueManager.dialogueLucyPlay)
        {
            Instantiate(gate, gatePos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
