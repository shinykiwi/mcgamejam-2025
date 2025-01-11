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
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private GameObject speachBubble;
    [SerializeField] TextMeshPro dialogueTMP;
    [SerializeField] private String[] dialogueOptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject currentPerson = Instantiate(personPrefab, new Vector3(-1f,0.6f,-0.75f), Quaternion.identity);
        currentPerson.GetComponent<Person>();
        speachBubble = currentPerson.transform.GetChild(0).gameObject;
        //assign dialogue based on the lost object
        assignDialogue("Test dialogue bla bla bla bla bla bla bla bla bla bla");
        revealSpeachBubble();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void assignDialogue(String dialogue){
        speachBubble.GetComponent<DialogueBubble>().SetDialogue(dialogue);
    }

    void revealSpeachBubble(){
        speachBubble.SetActive(true);
        speachBubble.GetComponent<DialogueBubble>().typeText = true;
    }


}
