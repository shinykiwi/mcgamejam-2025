using System.Collections;
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
        //selectLostObject();
        StartCoroutine(WalkIn());

    }

    // Update is called once per frame
    void Update()
    {
        /*
        timeLeft -= Time.deltaTime;
  
        if(timeLeft < 0){
            TimerOver();
        }

        if(resolved){
            validateReturn();
        }
        */
    }
    
    
    private IEnumerator WalkIn()
    {
        //yield return new WaitForSeconds(5);
        while (transform.position.x >  0)
        {
            transform.position += Vector3.left * (Time.deltaTime * 5f);
            yield return null;
        }
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        
        StartCoroutine(TurnToTalk());
    }
    
    private IEnumerator TurnToTalk()
    {
        Vector3 startRotation = new Vector3(0, 180, 0);
        Vector3 endRotation = new Vector3(0, 270, 0);
        float elapsedTime = 0;

        while (elapsedTime < 0.5f)
        {
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.eulerAngles = endRotation;
        
        yield return new WaitForSeconds(1f);
        StartCoroutine(turntoLeave());
    }

    private IEnumerator turntoLeave()
    {
        Vector3 startRotation = new Vector3(0, 270, 0);
        Vector3 endRotation = new Vector3(0, 180, 0);
        float elapsedTime = 0;

        while (elapsedTime < 0.5f)
        {
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.eulerAngles = endRotation;
        StartCoroutine(walkOut());
    }

    private IEnumerator walkOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < 4f)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 5f));
            elapsedTime += Time.deltaTime;
            yield return null;
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

        if(returnedItem != null)
         Debug.Log("Lost item is " + lostItem.GetDescription() + "Returned item is " + returnedItem.GetDescription());
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

        if(returnedItem != null && returnedItem.ItemEquals(lostItem)){
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
