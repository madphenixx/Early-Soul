using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    [Header("Characters in dialogue")]
    public CharaSO[] characters;

    [Header("Conversation")]
    public string[] dialogues;
}
