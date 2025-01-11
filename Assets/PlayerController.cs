using System;
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
            switch (ViewManager.Instance.GetCamView())
            {
             
                case 0:
                    Unbox();
                    break;
                
                case 1:
                    ReturnItem();
                    break;
                
                case 2:
                    if (itemHeld != null)
                    {
                        DropItem();
                    }
                    else
                    {
                        TryPickUpItem();
                    }
                    break;
                    
            }
        }
    }

    private void ReturnItem()
    {
        if(Input.GetMouseButtonDown(0)){
            Person currentPerson = PeopeSpawner.instance.GetCurrentPerson().GetComponent<Person>();
            currentPerson.SetReturnedObject(itemHeld);
            currentPerson.Resolve();
            Destroy(itemHeld.gameObject);
            itemHeld = null;
        }
    }

    void Unbox()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            if (hit.collider.CompareTag("Box"))
            {
                Box box = hit.collider.gameObject.GetComponent<Box>();
                box.TakeOneItem();
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
