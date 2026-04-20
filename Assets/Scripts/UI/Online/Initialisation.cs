using UnityEngine;

public class Initialisation : MonoBehaviour
{
    private bool initialized = false; public bool IsInitialized { get { return initialized; } }
    
    public virtual void Awake()
    {
        Initialize();
    }

    public virtual void OnEnable()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if (initialized) { return; }
        initialized = true;
    }
}
