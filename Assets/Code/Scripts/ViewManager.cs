using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ViewManager : MonoBehaviour
{
    private int camView = 0;

    public static ViewManager Instance;
    [SerializeField] private CinemachineCamera counterCam;
    [SerializeField] private CinemachineCamera lostNFoundCam;
    [SerializeField] private CinemachineCamera boxCam;
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private bool debugOn = true;
    
    private BoxSpawner boxSpawner;

    void Awake()
    {
        Instance = this;
    }

    public int GetCamView()
    {
        return camView;
    }
    private void Start()
    {
        if (!debugOn)
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }
        
        boxSpawner = FindFirstObjectByType<BoxSpawner>();
        boxSpawner.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Going right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (camView >= 0 && camView < 2)
            {
                camView++;
            }
        }

        // Going left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (camView <= 2 && camView > 0)
            {
                camView--;
            }
        }

        counterCam.enabled = true;
        lostNFoundCam.enabled = false;
        boxCam.enabled = false;
        
        switch (camView)
        {
            case 0:
                boxSpawner.enabled = true;
                boxCam.enabled = true;
                instructionText.text = "Click on box to unpack";
                break;
            case 1:
                boxSpawner.enabled = false;
                counterCam.enabled = true;
                instructionText.text = "Click to give item, [Z] if not found";
                break;
            case 2:
                boxSpawner.enabled = false;
                lostNFoundCam.enabled = true;
                instructionText.text = "Click to pick up or put back item";
                break;
        }
    }
}
