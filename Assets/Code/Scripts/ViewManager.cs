using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ViewManager : MonoBehaviour
{
    private int camView = 0;

    [SerializeField] private CinemachineCamera counterCam;
    [SerializeField] private CinemachineCamera lostNFoundCam;
    [SerializeField] private CinemachineCamera boxCam;

    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private bool debugOn = true;

    private void Start()
    {
        if (!debugOn)
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }
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
            else
            {
                camView = 0;
            }
        }

        // Going left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (camView <= 2 && camView > 0)
            {
                camView--;
            }
            else
            {
                camView = 2;
            }
        }

        counterCam.enabled = true;
        lostNFoundCam.enabled = false;
        boxCam.enabled = false;
        
        switch (camView)
        {
            case 0:
                counterCam.enabled = true;
                debugText.text = "Counter";
                break;
            case 1:
                boxCam.enabled = true;
                debugText.text = "Box";
                break;
            case 2:
                lostNFoundCam.enabled = true;
                debugText.text = "Lost and Found";
                break;
        }
    }
}
