using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<ItemData> possibleItems;
    public Color[] possibleColors;
    public Transform spawnPoint;
    public float spawnInterval;

    public void SpawnItem()
    {
        ItemData randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
        Color randomColor = possibleColors[Random.Range(0, possibleColors.Length)];

        GameObject itemSpawned = Instantiate(randomItem.GameObject(), spawnPoint.position, spawnPoint.rotation);
        PhysicalItem physicalItem = itemSpawned.GetComponent<PhysicalItem>();
        
        if (physicalItem != null)
        {
            physicalItem.itemData = randomItem;
            physicalItem.color = randomColor;
        }
    }
}
