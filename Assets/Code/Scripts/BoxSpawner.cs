
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoxSpawner : MonoBehaviour
{
    //Singleton
    public static BoxSpawner instance;
    // 3 boxes
    [SerializeField] private Box[] boxes;
    
    // UI components
    [SerializeField] private GameObject ui;
    [SerializeField] private RectTransform uiRect;
    private Image image;
    private TextMeshProUGUI text;

    
    
    
    public Queue<PhysicalItem> popupQueue;
    
    //Box spawn timers
    [SerializeField]
    private float boxSpawnInterval;
    [SerializeField]
    private float boxSpawnTime;


    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        image = ui.GetComponentInChildren<Image>();
        text = ui.GetComponentInChildren<TextMeshProUGUI>();
        
        // Initializing the queue
        popupQueue = new Queue<PhysicalItem>();
        
        // Hide the UI at first
        ui.SetActive(false);
        
    }
    
    

    void Update()
    {
//        print(Time.time + " " +  boxSpawnTime);
        if (Time.time > boxSpawnTime)
        {
            
            boxSpawnTime = Time.time + boxSpawnInterval;
            spawnBox();
            
        }   
    }

    public PhysicalItem GetRandomItemFromBox()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].gameObject.activeInHierarchy)
            {
                PhysicalItem[] items = boxes[i].GetArrayItems();
                return items[Random.Range(0, items.Length)];
            } 
        }
        return null;
    }

    public PhysicalItem[] GetArrayItems()
    {
        PhysicalItem[] allItems = { };
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].gameObject.activeInHierarchy)
            {
                PhysicalItem[] boxItems = boxes[i].GetArrayItems();
                allItems = allItems.Concat(boxItems).ToArray();
            } 
        }

        
        return allItems;
    }
    void spawnBox()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (!boxes[i].isActiveAndEnabled)
            {
                Box box = boxes[i];
                box.gameObject.SetActive(true);
                box.InitializeBox(Random.Range(2,6));
                
                break;
            }
        }
    }
    // Hides the UI when not in the Box view
    private void OnDisable()
    {
        ui.SetActive(false);
    }

    IEnumerator ShowPopup(PhysicalItem item)
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

    
/*
    public void Unbox()
    {
        // If there's already a box
        if (box && boxObject.activeSelf && box.IsHovering())
        {
            PhysicalItem itemTaken = box.TakeOneItem();
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Inventory.instance.AddItem(itemTaken);

                
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
    
*/
    private void SetUI(PhysicalItem item)
    {
        image.sprite = item.Icon;
        text.text = item.ItemName;
    }
}
