using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public void Awake()
    {
        instance = this;
    }
    
    List<PhysicalItem> items = new List<PhysicalItem>();
    
    public void AddItem(PhysicalItem item)
    {
        items.Add(item);
        Debug.Log($"{item.itemData.itemName} added to inventory.");
    }
    
    public void RemoveItem(PhysicalItem item)
    {
        items.Remove(item);
        Debug.Log($"{item.itemData.itemName} removed from inventory.");
    }
    
    public List<PhysicalItem> GetItems()
    {
        return items;
    }
}
