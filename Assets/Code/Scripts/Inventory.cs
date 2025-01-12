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
        //Debug.Log($"{item.GetDescription()} added to inventory.");
        placeOnInventory(item);
    }

    public void placeOnInventory(PhysicalItem item)
    {
        item.transform.position = new Vector3(Random.Range(-3.5f, -2.5f), 2f, Random.Range(-8f, -6f));
        item.transform.rotation = Quaternion.Euler(0f, Random.Range(-45f, 45f), 0f);
        item.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void RemoveItem(PhysicalItem item)
    {
        items.Remove(item);
        
        //Debug.Log($"{item.GetDescription()} removed from inventory.");
    }
    
    public List<PhysicalItem> GetItems()
    {
        return items;
    }

    public bool hasItem()
    {
        return items.Count > 0;
    }

    public PhysicalItem GetRandomItem()
    {
        return items[UnityEngine.Random.Range(0, items.Count)];
    }
}
