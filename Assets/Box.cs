using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    // Should really be randomly generated
    public int numItems;
    private bool isHovering = false;

    private void Start()
    {
        
        // Delete this once you integrate with raycast
        
        isHovering = true;
    }

    private void InitializeBox()
    {
        // items.Push()
    }

    public void Reset()
    {
        numItems = 5;
        
    }

    /// <summary>
    /// Takes one item from the box and returns it.
    /// </summary>
    /// <returns></returns>
    public PhysicalItem TakeOneItem()
    {
        numItems--;
        return ItemSpawner.instance.GetRandomItem();
        
    }

    public bool NextExists()
    {
        return numItems > 0;
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
