using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOptions", menuName = "Scriptable Objects/DialogueOptions")]
public class DialogueOptions : ScriptableObject
{
    [SerializeField]
    [TextArea(3, 10)] public string[] dialogues;

}
