using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    private Stack<PhysicalItem> items;
    private bool isHovering = false;

    private void Awake()
    {
        items = new Stack<PhysicalItem>();
        isHovering = true;
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
    public PhysicalItem TakeOneItem()
    {
        
        return items.Pop();
        
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
