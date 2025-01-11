using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    // Should really be randomly generated
    private Stack<string> items;
    private bool isHovering = false;

    private void Start()
    {
        items = new Stack<string>();
        InitializeBox();

        // Delete this once you integrate with raycast
        isHovering = true;
    }

    private void InitializeBox()
    {
        items.Push("sock");
        items.Push("shoe");
        items.Push("phone");
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
    public string TakeOneItem()
    {
        if (items.Count > 1)
        {
            Debug.Log(items.Peek());
            return items.Pop();
        }

        return null;
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
