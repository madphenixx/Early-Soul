using UnityEngine;

public class SoundGate : MonoBehaviour
{
    [SerializeField] private AudioSource gateOpening;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gateOpening.Play();
    }
}
