using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOptions", menuName = "Scriptable Objects/DialogueOptions")]
public class DialogueOptions : ScriptableObject
{
    [SerializeField]
    [TextArea(3, 10)] public string[] dialogues;

    public string GetDialogueForObject(string objectName){

        string selectedDialogue = null;
        switch (objectName){
            case "Book":
                selectedDialogue = dialogues[0];
                break;
            case "Phone":
                selectedDialogue = dialogues[1];
                break;
            case "Wallet":
                selectedDialogue = dialogues[2];
                break;
            case "Ring":
                selectedDialogue = dialogues[3];
                break;
            case "Keys":
                selectedDialogue = dialogues[4];
                break;
            case "Headphones":
                selectedDialogue = dialogues[5];
                break;
            case "Glasses":
                selectedDialogue = dialogues[6];
                break;
            case "CreditCard":
                selectedDialogue = dialogues[7];
                break;
            case "Documents":
                selectedDialogue = dialogues[8];
                break;
            default:
                break;
        }

        return selectedDialogue;
    }
}
