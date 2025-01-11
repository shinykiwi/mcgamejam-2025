using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    // Should really be randomly generated
    private Stack<ItemData> items;
    private bool isHovering = false;

    private void Start()
    {
        items = new Stack<ItemData>();
        InitializeBox();

        // Delete this once you integrate with raycast
        isHovering = true;
    }

    private void InitializeBox()
    {
        // items.Push()
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
    public ItemData TakeOneItem()
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
