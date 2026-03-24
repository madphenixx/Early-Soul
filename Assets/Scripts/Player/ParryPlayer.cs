using UnityEngine;
using System.Collections;

public class Parry : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ParryDestroy());
    }

    private IEnumerator ParryDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
