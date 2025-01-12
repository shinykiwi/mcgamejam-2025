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

    private bool personAtCounter = false;

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
        yield return new WaitForSeconds(2);
        spawnPerson(); 
    }
    private void spawnPerson()
    {
        personAtCounter = true;
        Debug.Log("Spawned person");
        currentPerson = Instantiate(personPrefab, new Vector3(7.5f, 1.8f, -11.7f), Quaternion.identity);
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
