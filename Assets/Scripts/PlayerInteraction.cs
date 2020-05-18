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
    System.Action RunOnUse; // The delegate to run methods on use

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

    // This will update the look at object
    void UpdateLookAtObject(GameObject newObject, string text, System.Action onUse){
        // Check if we started looking at this NPC
        if(lookingAt != newObject){
            lookingAt = newObject; // Replace looking at with a new object
            popupBox.SetActive(true); // Show the popup box
            // Grab the NPCs name and set the talk to text
            talkToText.text = text; // Update look at text
            lookAtDecay = Time.time + 0.1f; // Increase decay time to 0.1 seconds from now
            RunOnUse = onUse; // Update run on use
        }
        // If we were already looking at this NPC
        else{
            lookAtDecay = Time.time + 0.1f; // Increase decay time to 0.1 seconds from now
        }
    }

    // Runs every frame object is triggered
    void OnTriggerStay(Collider other){
        // Check to see if we look at an NPC
        if(other.tag == "NPC"){
            // Ensure the dialogue box is not active
            if(!dialogueBox.activeSelf){
                // Update the look at object
                UpdateLookAtObject(
                    other.gameObject,
                    "Press e to talk to " + other.GetComponent<NPCInfo>().name,
                    () => {
                        UnlockCursor(); // Unlock the cursor
                        lookAtDecay = 0f; // Reset look at decay to hide the text
                        popupBox.SetActive(false); // Hide the popup box
                        dialogueBox.SetActive(true); // Activate the dialogue box
                        NPCInfo curNPC = lookingAt.GetComponent<NPCInfo>(); // Store info for current npc
                        nameText.text = curNPC.name; // Display the NPCs name
                        lookingAt.GetComponent<DialogueManager>().LoadNextConversation(curNPC.starterConversation); // Set a convertsation with the NPC
                    }
                );
            }
        }
        // If the object wasn't an NPC
        else{
            // Grab the play on use component (if available)
            PlayOnUse useObject = other.GetComponent<PlayOnUse>();

            // Ensure the use object is valid
            if(useObject != null){
                UpdateLookAtObject(
                    other.gameObject,
                    useObject.useText,
                    () => {
                        useObject.Use(); // Attempt to use the object
                    }
                );
            }
        }
    }

    // Runs every frame
    void Update(){
        // Check if the decay time has passed
        if(Time.time > lookAtDecay){
            popupBox.SetActive(false); // Hide the popup box
            lookingAt = null; // Forget about the last looked at NPC
        }
        // If we are still looking at an NPC
        else{
            // Check if the player has pressed the Use key "e"
            if(Input.GetButtonDown("Use")){
                // Ensure that there are use events
                if(RunOnUse != null){
                    RunOnUse.Invoke(); // Invoke on use events
                }
            }
        }
    }

    // Locks the player's cursor
    public void LockCursor(){
        CursorManager.instance.Remove("Player Interaction"); // Remove from list cursor unlockers
    }

    // Unlocks the player's cursor
    public void UnlockCursor(){
        CursorManager.instance.Add("Player Interaction"); // Add to list of cursor unlockers
    }

    // This will disable the dialogue box
    public void CloseDialogueBox(){
        dialogueBox.SetActive(false);
        DialogueManager.activeManager.anim?.SetBool("TalkingToPlayer", false); // Stop talking to the player
    }
}
