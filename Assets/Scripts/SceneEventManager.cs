using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This will cache all child objects with
// scene event components
public class SceneEventManager : MonoBehaviour
{
    // A dictionary to hold each scene event
    public static Dictionary<string, SceneEvent> events;

    // Start is called before the first frame update, awake is called before that
    void Awake()
    {
        events = new Dictionary<string, SceneEvent>(); // Initialize the dictionary

        // Grab all scene events from children
        SceneEvent[] found = gameObject.GetComponentsInChildren<SceneEvent>();

        // Loop through each scene event
        foreach(SceneEvent current in found){
            events[current.name] = current; // Store the event in the dictionary for fast access
        }
    }
}
