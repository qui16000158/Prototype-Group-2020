﻿using System.Collections;
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
        SceneEventManager.events[eventName].RunEvent(); // Invoke the scene event
    }

    // Displays a hint
    public void DisplayHint(string hint){
        Hints._DisplayHint(hint); // Display hint to player
    }
}
