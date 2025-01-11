using System;
using UnityEngine;

public class Person : MonoBehaviour
{
    [SerializeField] private GameObject lostItem;
    [SerializeField] private float timeLeft = 10;
    [SerializeField] public GameObject returnedItem;
    [SerializeField] public bool lostItemDoesNotExist = false;
    [SerializeField] public bool resolved;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0){
            TimerOver();
        }
    }

    private void Start()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Animates the person walking into frame.
    /// </summary>
    private void WalkIn()
    {
        
    }

    /// <summary>
    /// Animates the person walking out of frame.
    /// </summary>
    private void WalkOut()
    {
        
    }
    
    private void TimerOver(){
        // Person leaves
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
