using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;
public class Person : MonoBehaviour
{
    [SerializeField] private PhysicalItem lostItem;
    [SerializeField] private float timeLeft = 10;
    [SerializeField] private PhysicalItem returnedItem;
    [SerializeField] public bool lostItemDoesNotExist = false;
    [SerializeField] public bool resolved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectLostObject();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft < 0){
            TimerOver();
        }

        if(resolved){
            //validateReturn();
        }
    }

    private void validateReturn()
    {
        if(ReturnedCorrectItem()){
            //add points to score
        } else {
            //penalty
        }
    }

    void selectLostObject(){

       int randomValue = UnityEngine.Random.Range(0, 100); 

        if (randomValue < 70)
        {
            //pick from item from inventory and assign to person
            if(Inventory.instance != null){
               List<PhysicalItem> itemsInInventory = Inventory.instance.GetItems();
               lostItem = itemsInInventory[Random.Range(0, itemsInInventory.Count)];
            }

        }
        else if (randomValue < 85)
        {
            //pick item from boxes and assign to person
        }
        else
        {
            //pick item that "does not currently exist" and assign to person
            
        }
        
    }

    private void TimerOver(){
        resolved = true;
    }

    private bool ReturnedCorrectItem(){

        if(returnedItem.Equals(lostItem)){
            return true;
        } else if (returnedItem.Equals(null) && !Inventory.instance.GetItems().Contains(lostItem)){
            return true;
        } else {
            return false;
        }
    }

    void SetLostObject(PhysicalItem item){
        lostItem = item;
    }

    PhysicalItem GetLostObject(){
        return lostItem;
    }

    void SetReturnedObject(PhysicalItem item){
        returnedItem = item;
    }

    PhysicalItem GetReturnedObject(){
        return returnedItem;
    }
}
