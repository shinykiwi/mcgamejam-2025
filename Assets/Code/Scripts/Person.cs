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
    [SerializeField] private GameObject speachBubble;

    public bool standing = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speachBubble = transform.GetChild(0).gameObject;
        assignDialogue("Test dialogue bla bla bla bla bla bla bla bla bla bla");
        selectLostObject();
        StartCoroutine(WalkIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (standing){
            timeLeft -= Time.deltaTime;
            
            if(timeLeft < 0){
                TimerOver();
                standing = false;
            }
        }
        

        if(resolved){
            validateReturn();
            resolved = false;
        }
    }
    
    void assignDialogue(string dialogue){
        speachBubble.GetComponent<DialogueBubble>().SetDialogue(dialogue);
    }

    void revealSpeachBubble(){
        speachBubble.SetActive(true);
        speachBubble.GetComponent<DialogueBubble>().typeText = true;
    }

    void disableSpeachBubble(){
        speachBubble.SetActive(false);
        speachBubble.GetComponent<DialogueBubble>().typeText = false;
    }

    
    private IEnumerator WalkIn()
    {
        //yield return new WaitForSeconds(5);
        while (transform.position.x >  0)
        {
            transform.position += Vector3.left * (Time.deltaTime * 7f);
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

        while (elapsedTime < 0.2f)
        {
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.eulerAngles = endRotation;
        standing = true;
        revealSpeachBubble();
    }

    private IEnumerator turntoLeave()
    {

        disableSpeachBubble();
        Vector3 startRotation = new Vector3(0, 270, 0);
        Vector3 endRotation = new Vector3(0, 180, 0);
        float elapsedTime = 0;

        while (elapsedTime < 0.2f)
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
        while (elapsedTime < 1f)
        {
            transform.Translate(Vector3.right * (Time.deltaTime*7f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Destroyed person");
        PeopeSpawner.instance.SetPersonAtCounter(false);
        PeopeSpawner.instance.SetCurrentPerson(null);
        Destroy(this.gameObject);
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
        
        StartCoroutine(turntoLeave());
    }

    void selectLostObject(){

       int randomValue = UnityEngine.Random.Range(0, 100); 
       List<PhysicalItem> itemsInInventory = Inventory.instance.GetItems();

        if (randomValue < 80 && itemsInInventory.Count > 0)
        {
            Debug.Log("Item picked from inventory");
            //pick from item from inventory and assign to person
            lostItem = itemsInInventory[Random.Range(0, itemsInInventory.Count)];

        }
        else if (randomValue < 95 && BoxSpawner.instance.GetArrayItems().Length > 0)
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
        // PeopeSpawner.instance.SetPersonAtCounter(false);
        Debug.Log("Time up Destroyed person");
        StartCoroutine(turntoLeave());
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
