
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxSpawner : MonoBehaviour
{
    // Box components
    [SerializeField] private GameObject boxObject;
    
    // UI components
    [SerializeField] private GameObject ui;
    [SerializeField] private RectTransform uiRect;
    private Image image;
    private TextMeshProUGUI text;
    private Box box;

    private Queue<ItemData> popupQueue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxObject = Instantiate(boxObject, transform);
        box = boxObject.GetComponent<Box>();
        image = ui.GetComponentInChildren<Image>();
        text = ui.GetComponentInChildren<TextMeshProUGUI>();
        
        // Initializing the queue
        popupQueue = new Queue<ItemData>();
        
        // Hide the UI at first
        ui.SetActive(false);
        
    }

    // Hides the UI when not in the Box view
    private void OnDisable()
    {
        ui.SetActive(false);
    }

    IEnumerator ShowPopup(ItemData item)
    {
        ui.SetActive(true);
        SetUI(item);
        uiRect.DOPunchAnchorPos(Vector2.down * 4, 0.8f, 5);
        int i = 0;
        
        // Wait X seconds
        while (i < 2)
        {
            i++;
            yield return null;
        }
        
        ui.SetActive(false);
        
        // Done
    }

    IEnumerator ControlPopups()
    {
        if (popupQueue.Count > 0)
        {
            StartCoroutine(ShowPopup(popupQueue.Dequeue()));
        }
        
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        // On Left Click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // If there's already a box
            if (box && boxObject.activeSelf && box.IsHovering())
            {
                ItemData itemTaken = box.TakeOneItem();
                Debug.Log(itemTaken);
                
                // Should play the "take" sound here 
                
                // Should show the popup UI here, use DotWeen to animate it
                // Add to stack on click, coroutine that handles the popups
                // See Red Dead for inspo
                
                // Add item to the queue
                popupQueue.Enqueue(itemTaken);
                
                // Start coroutine to display popup
                
                ui.SetActive(true);
                
                if (!box.NextExists())
                {
                    // Hide the box
                    boxObject.SetActive(false);
                    ui.SetActive(false);
                    box.Reset();
                    StopCoroutine(nameof(ControlPopups));
                }
            }
            
            // If there's no box
            else
            {
                boxObject.SetActive(true);
                StartCoroutine(nameof(ControlPopups));
            }
        }
    }

    private void SetUI(ItemData itemData)
    {
        image.sprite = itemData.icon;
        text.text = itemData.name;
    }
}
