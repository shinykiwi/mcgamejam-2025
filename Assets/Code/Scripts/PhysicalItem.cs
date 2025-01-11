using UnityEngine;

public class PhysicalItem : MonoBehaviour
{
    public ItemData itemData;
    public Color color; 
    
    public void PickUp(Transform playerHand)
    {
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Drop()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
