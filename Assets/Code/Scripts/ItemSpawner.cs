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

        GameObject itemSpawned = Instantiate(randomItem.prefab,new Vector3( Random.Range(4,4.7f), 0.5f, Random.Range(-1.3f, 1.3f)) , Quaternion.identity);
        PhysicalItem physicalItem = itemSpawned.GetComponent<PhysicalItem>();
        
        if (physicalItem != null)
        {
            physicalItem.itemData = randomItem;
            physicalItem.color = randomColor;
        }
        return physicalItem;
    }
}
