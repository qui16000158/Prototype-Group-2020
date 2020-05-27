using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This script will handle quest
// markers being placed in the scene
public class QuestMarkerManager : MonoBehaviour
{
    public static QuestMarkerManager instance;
    public Transform player; // The player's transform
    [SerializeField]
    GameObject markerPrefab;
    public Dictionary<string, QuestMarker> markers; // Each quest marker
    Transform nextMarker; // The next object to be used for a marker

    // Awake is called before the only start update 
    void Awake()
    {
        markers = new Dictionary<string, QuestMarker>(); // Initialize marker dictionary
        instance = this;
    }

    // Allows us to set a marker object
    public void SetMarkerObject(Transform markerObject){
        nextMarker = markerObject; // Set the object for the marker
    }

    // An overload with just the marker name, for unity events
    public void Add(string markerName){
        Add(markerName, nextMarker); // Add the quest marker
    }

    // Add a quest marker
    public void Add(string markerName, Transform markerObject){
        // Ensure the marker is not already in the scene
        if(!markers.ContainsKey(markerName)){
            // Create the quest marker
            QuestMarker marker = Instantiate(markerPrefab).GetComponent<QuestMarker>();
            // Assign values
            marker.markerName = markerName;
            marker.markerObject = markerObject;

            markers.Add(markerName, marker); // Add the marker
        }
    }

    // Removes a quest marker
    public void Remove(string markerName){
        // Ensure the marker exists
        if(markers.ContainsKey(markerName)){
            markers.Remove(markerName); // Remove it
        }
    }
}
