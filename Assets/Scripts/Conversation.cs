using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This script is a container for
// conversations, it will also contain
// methods for modifying the game using
// events.
[CreateAssetMenu]
public class Conversation : ScriptableObject
{
    public Dialogue[] allDialogue; // All of the dialogue in the conversation
    public UnityEvent onDialogueEnd; // Invoked when we run out of dialogue

    // This will load the next conversation into the active conversation manager
    public void LoadNextConversation(Conversation next){
        // ?. will check if the manager is valid before running the method
        DialogueManager.activeManager?.LoadNextConversation(next); // Load the next conversation into the manager
    }

    // Invoke a scene event
    public void InvokeSceneEvent(string eventName){
        GameObject found = GameObject.Find(eventName); // Try to find a game object with the event name

        // Check that the object was found
        if(found != null){
            DialogueEvent foundEvent = found.GetComponent<DialogueEvent>(); // Grab the dialogue event from the game object

            foundEvent.RunEvent(); // Run the dialogue event
        }
    }
}
