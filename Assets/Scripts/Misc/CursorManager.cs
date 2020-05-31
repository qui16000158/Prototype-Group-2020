using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This class contains an instance that
// keeps track of what is locking/unlocking
// the cursor and only locks the cursor if
// there is nothing still using it
public class CursorManager : MonoBehaviour
{
    public List<string> cursorLockers; // A list containing things that might unlock the cursor
    public static CursorManager instance; // The instance of the cursor manager

    void Start(){
        instance = this; // Cache this instance
        cursorLockers = new List<string>(); // Initialize cursor locker list

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Set cursor to invisible
    }

    // Adds a type of unlocker to the list
    public void Add(string locker){
        // Ensure this object isn't already unlocking the curosr
        if(!cursorLockers.Contains(locker)){
            cursorLockers.Add(locker); // Add the unlocker to the list
        }

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Set cursor to visible
    }

    public void Remove(string locker){
        // Ensure the unlocker is in the list
        if(cursorLockers.Contains(locker)){
            cursorLockers.Remove(locker); // Remove the unlocker from the list
        }

        // Check if there is no longer anything unlocking the cursor
        if(cursorLockers.Count == 0){
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Set cursor to invisible
        }
    }
}
