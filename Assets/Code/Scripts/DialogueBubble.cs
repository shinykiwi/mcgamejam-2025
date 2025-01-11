using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Collections;

public class DialogueBubble : MonoBehaviour
{

    [SerializeField] TextMeshPro dialogueTMP;
    [SerializeField] SpriteRenderer background;
    [SerializeField] TMP_Text textTMP;
    [SerializeField] TypeWriterEffect typeWriterEffect;
    [SerializeField] public bool typeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        typeWriterEffect = transform.GetComponent<TypeWriterEffect>();
        dialogueTMP = transform.Find("Dialogue").GetComponent<TextMeshPro>();
        background = transform.GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(typeText){
            revealBubble(dialogueTMP.text);
            typeText = false;
        }
    }
    public void revealBubble(string dialogue){
        StartCoroutine(typeDialogue(dialogue));

        //Ignore this, will use if we decide to resize bubble
        //dialogueTMP.SetText("Default text");
        // dialogueTMP.ForceMeshUpdate();
        //Vector2 textsize = dialogueTMP.GetRenderedValues(false);

        //Vector2 padding = new Vector2(0.5f, 0.5f);
        //background.size = textsize + padding;
        //Vector3 offset = new Vector3(0f, 0f);
        //background.transform.localPosition = new Vector3(0f, 0f, background.size.x/2f);
    }

    private IEnumerator typeDialogue(string text)
    {
      yield return typeWriterEffect.Run(text, dialogueTMP);

    }

    public void SetDialogue(string dialogue){
        dialogueTMP.text = dialogue;
    }
}
