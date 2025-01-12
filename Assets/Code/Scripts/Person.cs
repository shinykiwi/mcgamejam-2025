using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Linq;
using TMPro;
using Random = UnityEngine.Random;

public class Person : MonoBehaviour
{
    // Possible models
    [SerializeField] private GameObject[] models;
    [SerializeField] private GameObject modelSpawnPoint;
    
    [SerializeField] private PhysicalItem lostItem;
    [SerializeField] private float timeLeft = 60;
    [SerializeField] private PhysicalItem returnedItem = null;
    [SerializeField] public bool lostItemDoesNotExist = false;
    [SerializeField] public bool resolved = false;
    [SerializeField] private GameObject speachBubble;
    [SerializeField] private DialogueOptions dialogueOptions;
    [SerializeField] private TMP_Text timerText;
    public bool standing = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speachBubble = transform.GetChild(0).gameObject;
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
            } else {
                timerText.text = Mathf.FloorToInt(timeLeft).ToString();
            }
        }
        

        if(resolved){
            validateReturn();
            resolved = false;
        }
    }
    
    void assignDialogue(string selectedLostObject, string color){

        string currentItemDialogues = dialogueOptions.GetDialogueForObject(selectedLostObject);
        if (currentItemDialogues != null)
        {
            string[] variations = currentItemDialogues.Split('|');
            string selectedDialogue = variations[Random.Range(0, variations.Length)];
            string finalDialogue = selectedDialogue.Replace("_", color);

            Debug.Log("Assigned Dialogue: " + finalDialogue);
            speachBubble.GetComponent<DialogueBubble>().SetDialogue(finalDialogue);
        }
        else
        {
            Debug.Log("No dialogue available for " + selectedLostObject);
        }
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
        while (transform.position.x >  1.15)
        {
            transform.position += Vector3.left * (Time.deltaTime * 7f);
            yield return null;
        }
        transform.position = new Vector3(1.15f, transform.position.y, transform.position.z);
        
        StartCoroutine(TurnToTalk());
    }

    private float rotateTime = 0.5f;
    private IEnumerator TurnToTalk()
    {
        Vector3 startRotation = transform.rotation.eulerAngles;
        Vector3 endRotation = startRotation + new Vector3(0, 90, 0); 
        float elapsedTime = 0;

        while (elapsedTime < rotateTime)
        {
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / rotateTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.eulerAngles = endRotation;
        standing = true;
        revealSpeachBubble();
    }

    private IEnumerator turntoLeave()
    {
        
        standing = false;
        disableSpeachBubble();
        Vector3 startRotation = transform.rotation.eulerAngles;
        Vector3 endRotation = startRotation - new Vector3(0, 90, 0);
        float elapsedTime = 0;

        while (elapsedTime < rotateTime)
        {
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / rotateTime);
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

        PeopeSpawner.instance.SetPersonAtCounter(false);
        PeopeSpawner.instance.SetCurrentPerson(null);
        Debug.Log("Destroyed person");
        Destroy(this.gameObject);
    }


    private void validateReturn()
    {
        if(ReturnedCorrectItem()){
            Debug.Log("Returned correct item");
            GameManager.instance.UpdateScore(1000);
            
        } else {
            Debug.Log("Return wrong item");
            
            GameManager.instance.UpdateStrikesLeft();
        }

        if(returnedItem != null)
            Debug.Log("Lost item is " + lostItem.GetDescription() + "Returned item is " + returnedItem.GetDescription());
        
        StartCoroutine(turntoLeave());
    }

    void selectLostObject(){

       float randomValue = Random.value;
       

        if (randomValue < 0.50 && Inventory.instance.hasItem())
        {
            Debug.Log("Item picked from inventory");
            //pick from item from inventory and assign to person
            lostItem = Inventory.instance.GetRandomItem();

        }
        else if (randomValue < 0.90f)
        {
            if (BoxSpawner.instance.GetArrayItems().Length > 0)
            {
                Debug.Log("Item picked from box");
                //pick item from boxes and assign to person
                lostItem = BoxSpawner.instance.GetRandomItemFromBox();    
            }
            else if (Inventory.instance.hasItem())
            {
                Debug.Log("Item picked from inventory");
                //pick from item from inventory and assign to person
                lostItem = Inventory.instance.GetRandomItem();
            }
            else
            {
                Debug.Log("Item picked from non existing items");
                //pick item that "does not currently exist" and assign to person
                lostItem = ItemSpawner.instance.GetRandomItem();
            }
        }
        else
        {
            Debug.Log("Item picked from non existing items");
            //pick item that "does not currently exist" and assign to person
            lostItem = ItemSpawner.instance.GetRandomItem();
        }

        Debug.Log("Lost item is " + lostItem.GetDescription());
        assignDialogue(lostItem.ItemName, lostItem.color);
    }

    private void TimerOver(){
        LeaveCounter();
        //penalty
        GameManager.instance.UpdateStrikesLeft();
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

    private void OnEnable()
    {
        GameObject model = 
            Instantiate(models[Random.Range(0, models.Length - 1)], modelSpawnPoint.transform);
    }
}
