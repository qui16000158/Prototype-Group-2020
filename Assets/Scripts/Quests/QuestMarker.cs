using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// James Quinney - QUI16000158
// This is a quest marker, it will follow
// an object in the scene
public class QuestMarker : MonoBehaviour
{
    public string markerName;
    public Transform markerObject;
    [SerializeField]
    TMP_Text textObject; // The text object that shows the marker name

    // Runs before first frame update
    void Start(){
        textObject.text = markerName;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the marker object is valid
        if(markerObject != null && QuestMarkerManager.instance.markers.ContainsKey(markerName)){
            transform.position = markerObject.position + Vector3.up * 2f; // Move the quest marker to the object's position
            transform.LookAt(QuestMarkerManager.instance.player); // Face towards the player
        }
        else{
            // Destroy this quest marker
            QuestMarkerManager.instance.Remove(markerName);
            Destroy(gameObject); // Destroy the game object
        }
    }
}
