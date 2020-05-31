using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This quest is specific to the mechanic
// it counts upward and runs an event on
// completion
public class EventCounter : MonoBehaviour
{
    [SerializeField]
    int completionAmount;
    [SerializeField]
    UnityEvent onComplete; // The event to run on completion
    int currentAmount; // The current amount killed

    // Increases the kill counter
    public void IncreaseCounter(){
        currentAmount += 1; // Increase current amount by 1
        // Check if target has been reached
        if(currentAmount >= completionAmount){
            onComplete.Invoke(); // Run the on complete event
        }
    }
}
