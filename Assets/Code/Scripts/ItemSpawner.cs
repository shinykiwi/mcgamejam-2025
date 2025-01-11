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
            { "yellow" , new Color(0.8588235294117647f, 0.8705882352941177f, 0.12156862745098039f)} ,
            { "blue", new Color(0.07450980392156863f, 0.06274509803921569f, 0.7803921568627451f) } ,
            { "red", new Color(0.5294117647058824f, 0.03529411764705882f, 0.06274509803921569f) },
            { "green", new Color(0.08627450980392157f, 0.45098039215686275f, 0.011764705882352941f) },
            { "black", new Color(0.058823529411764705f, 0.058823529411764705f, 0.058823529411764705f) },
            { "white", new Color(0.8588235294117647f, 0.792156862745098f, 0.792156862745098f) },
            { "pink", new Color(0.611764705882353f, 0.047058823529411764f, 0.30196078431372547f) },
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
