
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxObject;
    
    // UI components
    [SerializeField] private GameObject ui;
    private Image image;
    private TextMeshProUGUI text;
    private Box box;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxObject = Instantiate(boxObject, transform);
        box = boxObject.GetComponent<Box>();
        image = ui.GetComponentInChildren<Image>();
        text = ui.GetComponentInChildren<TextMeshProUGUI>();
        
        // Hide the UI at first
        ui.SetActive(false);
        
    }

    // Hides the UI when not in the Box view
    private void OnDisable()
    {
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (box && boxObject.activeSelf && box.IsHovering())
            {
                string output = box.TakeOneItem();
                Debug.Log(output);
                
                // Should play the click sound here 
                
                // Should show the popup UI here, use DotWeen to animate it
                // Add to stack on click, coroutine that handles the popups
                // See Red Dead for inspo
                
                // set UI image
                //image.sprite = 
                
                // set UI text
                text.text = output;
                // call function to add to pile
                
                ui.SetActive(true);
                
                if (!box.NextExists())
                {
                    // Hide the box
                    boxObject.SetActive(false);
                    ui.SetActive(false);
                    box.Reset();
                }
            }
            else
            {
                boxObject.SetActive(true);
            }
        }
    }
}
