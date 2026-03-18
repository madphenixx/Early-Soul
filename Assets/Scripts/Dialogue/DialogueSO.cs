using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    public CharaSO[] characters;
    [InspectorTextArea]
    public string[] dialogues;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
