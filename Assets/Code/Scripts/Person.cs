using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Linq;
public class Person : MonoBehaviour
{
    [SerializeField] private PhysicalItem lostItem;
    [SerializeField] private float timeLeft = 10;
    [SerializeField] private PhysicalItem returnedItem = null;
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
            validateReturn();
        }
    }

    private void validateReturn()
    {
        if(ReturnedCorrectItem()){
            Debug.Log("Returned correct item");
            //add points to score
        } else {
            Debug.Log("Return wrong item");
            //penalty
        }

        PeopeSpawner.instance.SetPersonAtCounter(false);
        PeopeSpawner.instance.SetCurrentPerson(null);
        Debug.Log("Destroyed person");
        Destroy(this.gameObject);
    }

    void selectLostObject(){

       int randomValue = UnityEngine.Random.Range(0, 100); 
       List<PhysicalItem> itemsInInventory = Inventory.instance.GetItems();

        if (randomValue < 85 && itemsInInventory.Count > 0)
        {
            Debug.Log("Item picked from inventory");
            //pick from item from inventory and assign to person
            if(Inventory.instance != null){
               lostItem = itemsInInventory[Random.Range(0, itemsInInventory.Count)];
            }

        }
        else if (randomValue < -1 && BoxSpawner.instance.GetArrayItems().Length > 0)
        {
            Debug.Log("Item picked from box");
            //pick item from boxes and assign to person
            lostItem = BoxSpawner.instance.GetRandomItemFromBox();
        }
        else
        {
            Debug.Log("Item picked from non existing items");
            //pick item that "does not currently exist" and assign to person
            lostItem = ItemSpawner.instance.GetRandomItem();
        }

        Debug.Log("Lost item is " + lostItem.GetDescription());
        
    }

    private void TimerOver(){
        LeaveCounter();
        //penalty
    }

    void LeaveCounter(){
        PeopeSpawner.instance.SetPersonAtCounter(false);
        Debug.Log("Time up Destroyed person");
        Destroy(this.gameObject);
    }

    private bool ReturnedCorrectItem(){

        if(returnedItem != null && returnedItem.Equals(lostItem)){
            return true;
        } else if (returnedItem == null && !Inventory.instance.GetItems().Contains(lostItem) && !BoxSpawner.instance.GetArrayItems().Contains(lostItem)){
            return true;
        } else {
            return false;
        }
    }

    public void SetLostObject(PhysicalItem item){
        lostItem = item;
    }

    public PhysicalItem GetLostObject(){
        return lostItem;
    }

    public void SetReturnedObject(PhysicalItem item){
        returnedItem = item;
    }

    public PhysicalItem GetReturnedObject(){
        return returnedItem;
    }

    public void Resolve(){
        resolved = true;
    }
}
