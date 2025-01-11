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
    [SerializeField] private GameObject speachBubble;
    GameObject currentPerson;
    [SerializeField] TextMeshPro dialogueTMP;
    [SerializeField] private String[] dialogueOptions;

    private bool noPersonAtCounter = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(noPersonAtCounter){
            spawnPerson();
            noPersonAtCounter = false;
        }
    }

    private IEnumerator spawnPerson()
    {
        yield return new WaitForSeconds(2);
        currentPerson = Instantiate(personPrefab, new Vector3(0.72f,2.11f,-11.13f), Quaternion.identity);
        speachBubble = currentPerson.transform.GetChild(0).gameObject;
        //assign dialogue based on the lost object
        assignDialogue("Test dialogue bla bla bla bla bla bla bla bla bla bla");
        revealSpeachBubble();
    }

    void assignDialogue(String dialogue){
        speachBubble.GetComponent<DialogueBubble>().SetDialogue(dialogue);
    }

    void revealSpeachBubble(){
        speachBubble.SetActive(true);
        speachBubble.GetComponent<DialogueBubble>().typeText = true;
    }

    public GameObject GetCurrentPerson(){
        return currentPerson;
    }
}
