using UnityEngine;

public class Person : MonoBehaviour
{
    [SerializeField] private GameObject lostItem;
    [SerializeField] private float timeLeft = 10;
    [SerializeField] public GameObject returnedItem;
    [SerializeField] public bool lostItemDoesNotExist = false;
    [SerializeField] public bool resolved;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft < 0){
            TimerOver();
        }
    }

    private void TimerOver(){

    }

    private bool ReturnedCorrectItem(){
        if(returnedItem == null){
            return lostItemDoesNotExist;
        } else {
            //check if the IDs match and return true/false
            return true; //returns true for now
        }
    }

}
