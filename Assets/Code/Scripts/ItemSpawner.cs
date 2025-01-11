using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public static ItemSpawner instance;
    [HideInInspector]
    public ItemData[] possibleItems;
    public Dictionary<string, Color> PossibleColors;
    public Transform spawnPoint;
    public float spawnInterval;

    private List<string> colorList;
     
    
    
    void Awake()
    {
        possibleItems = Resources.LoadAll<ItemData>("Items");
        instance = this;
        
        PossibleColors = new Dictionary<string, Color>
        {
            { "yellow", Color.yellow },
            { "blue", Color.blue },
            { "red", Color.red },
            { "green", Color.green },
            { "black", Color.black },
            { "white", Color.white }
        };

        colorList = PossibleColors.Keys.ToList();
    }
    public PhysicalItem GetRandomItem()
    {
        ItemData randomItem = possibleItems[Random.Range(0, possibleItems.Length)];
        string randomColor = colorList[Random.Range(0, colorList.Count)];

        GameObject itemSpawned = Instantiate(randomItem.prefab, Vector3.zero , Quaternion.identity);
        PhysicalItem physicalItem = itemSpawned.GetComponent<PhysicalItem>();
        
        if (physicalItem != null)
        {
            physicalItem.itemData = randomItem;
            physicalItem.color = randomColor;
        }
        return physicalItem;
    }
}
