using System.Collections;
using TMPro;
using UnityEngine;

public class FadingTextBox : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        
    }


    public void flash(int strike)
    {
        StopAllCoroutines();
        textMesh.text = strike.ToString() + "/3 Strikes";
        StartCoroutine(flashError());
    }
    public IEnumerator flashError()
    {
        for (int i = 0; i < 5; i++)
        {
            
            while (textMesh.color.a < 1)
            {
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a + (Time.deltaTime / 0.25f));
                yield return null;
            } 
            
            while (textMesh.color.a >= 0)
            {
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - (Time.deltaTime / 0.25f));
                yield return null;
            } 
        }
    }

    
}
