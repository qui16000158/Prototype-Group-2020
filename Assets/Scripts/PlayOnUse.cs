﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This will create a public method
// accessible by the player interaction
// script for running events on use
public class PlayOnUse : MonoBehaviour
{
    [SerializeField]
    UnityEvent onUse; // The event that runs when the object is used
    public string useText; // THe text that shows before you use the object

    // This will invoke the events
    public void Use(){
        onUse.Invoke(); // Invoke the on use event
    }
}
