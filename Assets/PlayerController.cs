using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handPosition;
    public PhysicalItem itemHeld;
    
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
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
        
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            if (hit.collider.name == "InventoryTable")
            {
                itemHeld.Drop(hit.point + Vector3.up*0.1f);
                itemHeld = null;                
            }
            
        }
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void TryPickUpItem()
    {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            
            PhysicalItem item = hit.collider.GetComponent<PhysicalItem>();
            if (item != null)
            {
                itemHeld = item;
                item.PickUp(handPosition);
            }
        }
    }
}
