using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalItem : MonoBehaviour
{
    public ItemData itemData;
    public Color color; 
    public Material material;


    public string ItemName => itemData.itemName;
    public Sprite Icon => itemData.icon;
    public void PickUp(Transform playerHand)
    {
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void setColor()
    {
        material.color = color;
    }
    public void Drop()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
