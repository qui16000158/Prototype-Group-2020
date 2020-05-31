using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This will run an event assuming
// a chance has passed
public class PlayOnChance : MonoBehaviour
{
    // Check/against values for random numbers
    [SerializeField]
    int checkValue = 1;
    [SerializeField]
    int againstValue = 2;

    [SerializeField]
    UnityEvent onSucceed; // The event to run when succeeding

    // Will attempt to run the event
    public void Attempt(){
        // The final value is never included (with integers), so we simply add one
        int num = Random.Range(1, againstValue+1); // Generate a random number
        // Check if the number is below the check value
        if(num<=checkValue){
            onSucceed.Invoke(); // Invoke on succeed event
        }
    }
}
