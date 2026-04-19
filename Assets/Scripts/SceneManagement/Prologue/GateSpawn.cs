using Unity.VisualScripting;
using UnityEngine;

public class GateSpawn : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject gate;
    [Header("Location")]
    [SerializeField] private GameObject props;

    private Vector3 gatePos;

    void Start()
    {
        props = GameObject.Find("Props");
        gatePos = new Vector3(-21.39573f, -1, -1); 
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PrologueManager.dialogueLucyPlay)
        {
            Instantiate(gate, gatePos, Quaternion.identity, props.transform);
            Destroy(gameObject);
        }
    }
}
