using Unity.VisualScripting;
using UnityEngine;

public class GateSpawn : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject props;

    [SerializeField] private Vector3 gatePos;

    void Start()
    {
        props = GameObject.Find("Props");
       gatePos = new Vector3(-19,-1, -1); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PrologueManager.dialogueLucyPlay)
        {
            Instantiate(gate, gatePos, Quaternion.identity, props.transform);
            Destroy(gameObject);
        }
    }
}
