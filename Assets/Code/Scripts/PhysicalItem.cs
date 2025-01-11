using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalItem : MonoBehaviour
{
    
    public ItemData itemData;
    public string color;
    
    public string ItemName => itemData.itemName;
    public Sprite Icon => itemData.icon;
    
    
    public Material material;

    public GameObject[] coloredParts; 
    //only used when picking up stuff
    private Vector3 itemSize;
    void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void Start()
    {
        itemSize = transform.localScale;
        setColor();
    }
    
    public void PickUp(Transform playerHand)
    {
        transform.localScale = itemSize*0.6f;
        Inventory.instance.RemoveItem(this);
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 90, 90);
        GetComponent<Rigidbody>().isKinematic = true;
        
        
    }

    void setColor()
    {
        material = new Material(material);
        material.color = ItemSpawner.instance.PossibleColors[color];
        foreach (GameObject coloredPart in coloredParts)
        {
            coloredPart.GetComponent<MeshRenderer>().material = material;
        }
    }
    
    public void Drop(Vector3 pos)
    {
        Inventory.instance.AddItem(this);   
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(0f, Random.Range(-45f, 45f), 0f);
        GetComponent<Rigidbody>().isKinematic = false;
        transform.position = pos;
        transform.localScale = itemSize;
    }

    public string GetDescription()
    {
        return "a " + color + " " + ItemName;
    }

    
    protected bool Equals(PhysicalItem other)
    {
        return ItemName == other.ItemName && color == other.color;
    }

    
}
