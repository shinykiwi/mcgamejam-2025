using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    private Stack<PhysicalItem> items;
    private bool isHovering = false;
    
    private AudioSource audioSource;
    public PhysicalItem[] GetArrayItems()
    {
        return items.ToArray();
    }
    private void Awake()
    {
        items = new Stack<PhysicalItem>();
        isHovering = true;
        audioSource = GetComponent<AudioSource>();

    }

    public void InitializeBox(int numItems = 5)
    {
        for (int i = 0; i < numItems; i++)
        {
            items.Push(ItemSpawner.instance.GetRandomItem());
        }
    }

    public void Reset()
    {
        items.Clear();
        InitializeBox();
        
    }

    /// <summary>
    /// Takes one item from the box and returns it.
    /// </summary>
    /// <returns></returns>
    
    
    public void TakeOneItem()
    {
        // Play audio
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        
        //get item
        PhysicalItem item = items.Pop();
        
        //Give item to Inventory and BoxSpawner
        Inventory.instance.AddItem(item);
        BoxSpawner.instance.popupQueue.Enqueue(item);
        
        //Check if box is empty
        if (!NextExists())
        {
            gameObject.SetActive(false);
        }
    }

    public bool NextExists()
    {
        return items.Count > 0;
    }

    /// <summary>
    /// Toggle if the box is being hovered over or not. Should be called from within a raycast block in the Player.
    /// </summary>
    public void ToggleHovering()
    {
        isHovering = !isHovering;
    }

    public bool IsHovering()
    {
        return isHovering;
    }
}
