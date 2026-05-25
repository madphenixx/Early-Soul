using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MeleePlayer : MonoBehaviour
{
    [Header("Settings")]
    public int baseAttack = 1;
    [SerializeField] private float meleeDuration = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerAttack.isMeleeing = true;
        StartCoroutine(MeleeDestroy(meleeDuration));
    }

    private void Update()
    {
        if (PlayerAttack.isParrying == true)
        {
            PlayerAttack.isMeleeing = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemi") || collision.gameObject.CompareTag("EnnemiSol"))
        {
            if (collision.gameObject.CompareTag("EnnemiSol"))
            {
                EnnemiSol ennemiSol = collision.gameObject.GetComponent<EnnemiSol>();
                ennemiSol.tookDamage = true;
            }

            float produit = 1 + (GameManager.combo * 0.1f);
            Slider slEnnemi = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo : " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x" + produit.ToString();

            collision.gameObject.GetComponent<ClassEnnemi>().pv += - baseAttack * produit;
            slEnnemi.value = collision.gameObject.GetComponent<ClassEnnemi>().pv;

            GameManager.score = Mathf.RoundToInt(GameManager.score + 10 * produit);
            GameManager.scoreText.text = "Score : " + GameManager.score.ToString();

            PlayerAttack.isMeleeing = false;
            MeleeDestroy(0.18f);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            float produit = (1 + (GameManager.combo * 0.5f)) * collision.gameObject.GetComponent<Boss>().resistanceMelee;
            Slider slEnnemi = GameObject.Find("PVBoss").GetComponent<Slider>();

            GameManager.combo += 1;
            GameManager.comboText.text = "Combo : " + GameManager.combo.ToString();
            GameManager.multiplicateurText.text = "x" + produit.ToString();

            collision.gameObject.GetComponent<ClassEnnemi>().pv += - baseAttack * produit;
            slEnnemi.value = collision.gameObject.GetComponent<ClassEnnemi>().pv;

            GameManager.score = Mathf.RoundToInt(GameManager.score + 10 * produit);
            GameManager.scoreText.text = "Score : " + GameManager.score.ToString();

            collision.gameObject.GetComponent<Boss>().damageCount += produit;

            PlayerAttack.isMeleeing = false;
            MeleeDestroy(0.18f);
        }
    }

    private IEnumerator MeleeDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        PlayerAttack.isMeleeing = false;
        Destroy(gameObject);
    } 
}