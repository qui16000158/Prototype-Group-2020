using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// James Quinney - QUI16000158
// This script contains public events
// for switching scenes, and quitting the game
public class SceneTransitioner : MonoBehaviour
{
    // This will load a scene using its name
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName); // Load the given scene
    }

    // This will quit the game
    public void Quit(){
        Application.Quit();
    }
}
