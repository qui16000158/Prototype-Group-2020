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
    [SerializeField]
    bool lateStart; // Only run on start

    // Runs when this object is enabled
    void OnEnable(){
        if(lateStart) return; // Don't run here, if late start is enabled
        onAwake.Invoke(); // Invoke the event
    }

    void Start(){
        if(!lateStart) return; // Only run here if late start is enabled
        onAwake.Invoke(); // Invoke the event
    }
}
