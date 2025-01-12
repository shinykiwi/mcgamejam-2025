using System;
using DG.Tweening.Plugins;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PeopeSpawner : MonoBehaviour
{
    public static PeopeSpawner instance;
    [SerializeField] private GameObject personPrefab;
    //[SerializeField] private GameObject speachBubble;
    GameObject currentPerson;
    [SerializeField] TextMeshPro dialogueTMP;
    //public float initialTimer = 60f; 
    public float minTimer = 15f;  
    public float maxTimer = 30f;
    [SerializeField] float timerDecreaseRate = 1f; 
    public float maxTimerLimit = 10f;
    public float minTimerLimit = 5f;
    private bool personAtCounter = false;
    
    public float peopleSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        StartCoroutine(spawnFirstPerson());
        //currentPerson = Instantiate(personPrefab, new Vector3(7.5f, 1.8f, -11.7f), Quaternion.Euler(0,180,0));
    }

    // Update is called once per frame
    void Update()
    {
        if(!personAtCounter){
            spawnPerson();
        }
    }
    IEnumerator spawnFirstPerson(){

        personAtCounter = true;
        yield return new WaitForSeconds(3);
        spawnPerson(); 
    }
    private void spawnPerson()
    {
        personAtCounter = true;
        Debug.Log("Spawned person");
        currentPerson = Instantiate(personPrefab, new Vector3(7.5f, 1.8f, -11.7f), Quaternion.Euler(0,180,0));
        currentPerson.GetComponent<Person>().SetTimeLeft(UnityEngine.Random.Range(minTimer, maxTimer));

        // Reduce the initial timer for the next spawn
        maxTimer = Mathf.Max(maxTimer - timerDecreaseRate, maxTimerLimit);
        minTimer = Mathf.Max(minTimer - (timerDecreaseRate + 1), minTimerLimit);
        peopleSpawned++;

    }

    public bool hasPerson(){
        return currentPerson != null;
    }

    public bool IsStanding(){
        return currentPerson.GetComponent<Person>().standing;
    }

    public GameObject GetCurrentPerson(){
        return currentPerson;
    }

     public void SetCurrentPerson(GameObject person){
        currentPerson = person;
    }

    public void SetPersonAtCounter(bool personAtCounter){
        this.personAtCounter = personAtCounter;
    }
    
}
