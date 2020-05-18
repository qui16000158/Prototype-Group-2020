using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This will run a unity event
// when this object is enabled
public class PlayOnAwake : MonoBehaviour
{
    [SerializeField]
    UnityEvent onAwake; // The event to invoke on awake

    // Runs when this object is enabled
    void OnEnable(){
        onAwake.Invoke(); // Invoke the event
    }
}
