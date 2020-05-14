using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue{
    public string dialogueInput;
    public UnityEvent[] clickEvent;
}

// James Quinney - QUI16000158
// This script handles dialogue for a single
// NPC, it contains a dialogue class which holds
// both text and click events for links in that text
// and the main class simply loads/feeds dialogue
// to the text replacer.
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager activeManager; // The currently active dialogue manager

    // Store references to the dialogue clicker, and the dialogue replacer (in the inspector)
    [SerializeField]
    TMP_LinkClicker clicker;
    [SerializeField]
    TMP_DialogueReplacer replacer;

    [SerializeField]
    Conversation conversation; // A container holding each dialogue in the current conversation

    Dialogue currentDialogue; // The current dialogue
    Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();

    // This will load the conversation queue (If valid)
    public void Initialize(){
        // Ensure the conversation is valid
        if(conversation != null){
            dialogueQueue = new Queue<Dialogue>(); // Reset the dialogue queue

            // Loop through the conversation
            for(int i = 0;i<conversation.allDialogue.Length;i+=1){
                dialogueQueue.Enqueue(conversation.allDialogue[i]); // Add the current dialogue to the queue of dialogue
            }
        }
    }

    // This method will load a new conversation
    public void LoadNextConversation(Conversation toLoad){
        conversation = toLoad; // Store the new conversation
        activeManager = this; // Store this manager as the active manager

        Initialize(); // Initialize the new conversation queue

        LoadNextDialogue(); // Load the first dialogue message
    }

    // This will load the next dialogue in the queue
    public void LoadNextDialogue(){
        // Check that there is another dialogue before loading the next one
        if(dialogueQueue.Count > 0){
            currentDialogue = dialogueQueue.Dequeue(); // Grab the next dialogue from the queue
            replacer.Feed(currentDialogue.dialogueInput); // Feed the text to the dialogue replacer
        }
        else{
            EndDialogue(); // Run the dialogue end event
        }
    }

    // This will run the dialogue end event
    public void EndDialogue(){
        conversation.onDialogueEnd.Invoke();
    }

    // This will return the click event for the current dialogue
    public UnityEvent LinkEvent(int index){
        // Check if there is a current dialogue
        if(currentDialogue != null){
            return currentDialogue.clickEvent[index];
        }
        else{
            return null;
        }
    }
}
