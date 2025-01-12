using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handPosition;
    public PhysicalItem itemHeld;    
    private Camera cam;

    private AudioSource audioSource;
    public AudioClip putBackClip;
    public AudioClip pickUpClip;

    private Outline currentlyGlowingBox;
    void Start()
    {
        cam = GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
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
                    if (customerExists() && customerIsStanding()){
                        giveItemBack();
                    }
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

        if (Input.GetKeyDown(KeyCode.Z) && ViewManager.Instance.GetCamView() == 1 && customerExists() && customerIsStanding())
        {
            giveNothing();
        }

        if (ViewManager.Instance.GetCamView() == 0)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Box"))
            {
                
                Outline box = hit.collider.gameObject.GetComponent<Outline>();
                if (currentlyGlowingBox != null && box != currentlyGlowingBox )
                {
                    currentlyGlowingBox.enabled = false;
                    
                }
                currentlyGlowingBox = box;
                currentlyGlowingBox.enabled = true;
            }
            else if (currentlyGlowingBox != null)
            {
                currentlyGlowingBox.enabled = false;
                currentlyGlowingBox = null;
            }
        }
    }


    private bool customerExists(){
        return PeopeSpawner.instance.hasPerson();
    }

    private bool customerIsStanding(){
        return PeopeSpawner.instance.IsStanding();
    }

    void giveNothing()
    {
        Person currentPerson = PeopeSpawner.instance.GetCurrentPerson().GetComponent<Person>();
        currentPerson.SetReturnedObject(null);
        currentPerson.Resolve();
    }
    private void giveItemBack(){
        if (itemHeld == null) return;
        
        Person currentPerson = PeopeSpawner.instance.GetCurrentPerson().GetComponent<Person>();
        currentPerson.SetReturnedObject(itemHeld);
        currentPerson.Resolve();
        Destroy(itemHeld.gameObject);
        itemHeld = null;
    }
    private void ReturnItem()
    {
        
        if(PeopeSpawner.instance.GetCurrentPerson() != null){
            Person currentPerson = PeopeSpawner.instance.GetCurrentPerson().GetComponent<Person>();
            currentPerson.SetReturnedObject(itemHeld);
            currentPerson.Resolve();
            if(itemHeld != null)
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
                // play audo
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
                
                audioSource.clip = putBackClip;
                if (!audioSource.isPlaying)
                {
                   audioSource.Play(); 
                }
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
                
                audioSource.clip = pickUpClip;
                if (!audioSource.isPlaying)
                {
                    audioSource.Play(); 
                }
                
            }
        }
    }


}
