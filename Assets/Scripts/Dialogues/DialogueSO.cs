using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    public CharaSO[] characters;
    [InspectorTextArea]
    public string[] dialogues;
}
