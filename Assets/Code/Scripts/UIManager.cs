using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Spawn the main menu and the pause menu
        mainMenu = Instantiate(mainMenu, transform);
        pauseMenu = Instantiate(pauseMenu, transform);
    }
}