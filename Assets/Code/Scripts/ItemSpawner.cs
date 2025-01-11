using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public static ItemSpawner instance;
    public ItemData[] possibleItems;
    public Color[] possibleColors;
    public Transform spawnPoint;
    public float spawnInterval;

    void Awake()
    {
        possibleItems = Resources.LoadAll<ItemData>("Items");
        instance = this;
    }
    public PhysicalItem GetRandomItem()
    {
        ItemData randomItem = possibleItems[Random.Range(0, possibleItems.Length)];
        Color randomColor = possibleColors[Random.Range(0, possibleColors.Length)];

        GameObject itemSpawned = Instantiate(randomItem.prefab,new Vector3( Random.Range(-3.5f, -2.5f) ,1.7f,Random.Range(-8f, -6f)) , Quaternion.identity);
        PhysicalItem physicalItem = itemSpawned.GetComponent<PhysicalItem>();
        
        if (physicalItem != null)
        {
            physicalItem.itemData = randomItem;
            physicalItem.color = randomColor;
        }
        return physicalItem;
    }
}
