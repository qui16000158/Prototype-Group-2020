using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// James Quinney - QUI16000158
// This will allow the player to interact
// with an NPC by starting a conversation by 
// pressing the "Use" key
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueBox; // The dialogue box...
    [SerializeField]
    TMP_Text nameText; // The NPCs name text

    [SerializeField]
    GameObject popupBox; // The popup box for the "talk to" text
    [SerializeField]
    TMP_Text talkToText; // The text that tells the player to talk to an NPC

    GameObject lookingAt; // The npc the player is looking at
    float lookAtDecay; // When the looking at message disappears in seconds

    // Runs every frame object is triggered
    void OnTriggerStay(Collider other){
        // Check to see if we look at an NPC
        if(other.tag == "NPC" && !dialogueBox.activeSelf){
            // Check if we started looking at this NPC
            if(lookingAt != other.gameObject){
                lookingAt = other.gameObject;
                popupBox.SetActive(true); // Show the popup box
                // Grab the NPCs name and set the talk to text
                talkToText.text = "Press e to talk to " + lookingAt.GetComponent<NPCInfo>().name;
                lookAtDecay = Time.time + 0.1f; // Increase decay time to 0.1 seconds from now
            }
            // If we were already looking at this NPC
            else{
                lookAtDecay = Time.time + 0.1f; // Increase decay time to 0.1 seconds from now
            }
        }
    }

    // Runs before first frame update
    void Start(){
        StartCoroutine(OnUpdate()); // Start the on update coroutine
    }

    // Runs every frame
    IEnumerator OnUpdate(){
        // Check if the decay time has passed
        if(Time.time > lookAtDecay){
            popupBox.SetActive(false); // Hide the popup box
            lookingAt = null; // Forget about the last looked at NPC
        }
        // If we are still looking at an NPC
        else{
            // Check if the player has pressed the Use key "e"
            if(Input.GetButtonDown("Use")){
                UnlockCursor(); // Unlock the cursor
                lookAtDecay = 0f; // Reset look at decay to hide the text
                popupBox.SetActive(false); // Hide the popup box
                dialogueBox.SetActive(true); // Activate the dialogue box
                yield return null; // Wait until next frame
                NPCInfo curNPC = lookingAt.GetComponent<NPCInfo>(); // Store info for current npc
                nameText.text = curNPC.name; // Display the NPCs name
                lookingAt.GetComponent<DialogueManager>().LoadNextConversation(curNPC.starterConversation); // Set a convertsation with the NPC
            }
        }
        yield return null; // Wait 1 frame
        StartCoroutine(OnUpdate()); // Start the coroutine again
    }

    // Locks the player's cursor
    public void LockCursor(){
        CursorManager.instance.Remove("Player Interaction"); // Remove from list cursor unlockers
    }

    // Unlocks the player's cursor
    public void UnlockCursor(){
        CursorManager.instance.Add("Player Interaction"); // Add to list of cursor unlockers
    }
}
