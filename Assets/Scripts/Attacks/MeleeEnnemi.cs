using UnityEngine;
using System.Collections;

public class MeleeEnnemi : MonoBehaviour
{
    [Header("For Player.TookDamage")]
    public GameObject attacker;

    [Header("Settings")]
    [SerializeField] private int baseAttack = 1;
    [SerializeField] private int distanceTookDamage = 6;
    [SerializeField] private float meleeDuration = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MeleeDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerMovement.isInvicible == false)
        {
            GameManager.pv +=  -baseAttack;
            GameManager.pvSlider.value = GameManager.pv;

            GameManager.combo = 0;
            GameManager.comboText.text = "Combo: " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x1";

            GameManager.score = GameManager.score - 10;
            GameManager.scoreText.text = "Score: "+ GameManager.score.ToString();

            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.TookDamage(attacker, distanceTookDamage);

            Destroy(gameObject);
        }
    }

    private IEnumerator MeleeDestroy()
    {
        yield return new WaitForSeconds(meleeDuration);
        Destroy(gameObject);
    }
}
