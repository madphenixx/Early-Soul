using UnityEngine;

public class Bonus : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int heal = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.pv += heal;
            GameManager.pvSlider.value = GameManager.pv;
            Destroy(gameObject);
        }
    }
}
