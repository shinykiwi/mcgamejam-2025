using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalItem : MonoBehaviour
{
    public ItemData itemData;
    public Color color; 
    public Material material;


    public string ItemName => itemData.itemName;
    public Sprite Icon => itemData.icon;

    void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    public void PickUp(Transform playerHand)
    {
        
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 90, 90);
        GetComponent<Rigidbody>().isKinematic = true;
        
    }

    void setColor()
    {
        material.color = color;
    }
    public void Drop(Vector3 pos)
    {
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(0f, Random.Range(-45f, 45f), 0f);
        GetComponent<Rigidbody>().isKinematic = false;
        transform.position = pos;
        
    }
}
