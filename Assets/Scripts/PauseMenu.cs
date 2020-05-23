using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This will contain methods for opening
// and closing the pause menu
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu; // The pause menu game object

    // Opens the pause menu
    public void Open(){
        Time.timeScale = 0.1f; // Slow down gameplay
        CursorManager.instance.Add("Pause Menu"); // Unlock the cursor
        pauseMenu.SetActive(true);
    }

    // Closes the pause menu
    public void Close(){
        Time.timeScale = 1f; // Return to normal speed
        CursorManager.instance.Remove("Pause Menu"); // Potentially lock the cursor
        pauseMenu.SetActive(false);
    }

    // Runs every frame
    void Update(){
        // Check if the escape key has been pressed
        if(Input.GetKeyDown(KeyCode.Escape)){
            Open(); // Open the pause menu
        }
    }

    // Runs before first frame update
    void Start(){
        Time.timeScale = 1f; // Ensure time starts correctly
    }
}
