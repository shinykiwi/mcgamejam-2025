using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handPosition;
    public PhysicalItem itemHeld;
    
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Picking Up");
            if (itemHeld != null)
            {
                DropItem();
            }
            else
            {
                TryPickUpItem();
            }
        }
    }

    void DropItem()
    {
        itemHeld.Drop();
        itemHeld = null;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void TryPickUpItem()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            print(hit.collider.name);
            PhysicalItem item = hit.collider.GetComponent<PhysicalItem>();
            if (item != null)
            {
                itemHeld = item;
                item.PickUp(handPosition);
            }
        }
    }
}
