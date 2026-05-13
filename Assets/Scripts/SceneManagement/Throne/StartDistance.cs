using UnityEngine;

public class StartDistance : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        GameManager.canAttack = true;
    }
}
