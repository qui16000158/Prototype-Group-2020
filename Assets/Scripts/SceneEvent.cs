using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// QUI16000158 - James Quinney
// This script holds data for events
// which can be linked to quests
public class SceneEvent : MonoBehaviour
{
    [SerializeField]
    UnityEvent succeedEvent; // This event will run if the required quest is completed
    [SerializeField]
    UnityEvent failEvent; // This event will run if the required quest is not completed
    [SerializeField]
    string linkedQuest; // The quest linked to this event (Not required)
    [SerializeField]
    string requiredQuest; // A quest that is required to be completed run this event (leave blank for none)

    // Mark task in linked quest as completed
    public void CompleteTask(string task){
        Quest linked = Quests.Get(linkedQuest); // Attempt to get the linked quest
        // Check if the player has the linked quest ongoing
        if(linked != null){
            linked.CompleteTask(task); // Mark task as completed
        }
    }

    // Start the quest attached to this game object
    public void StartQuest(){
        Quest attachedQuest = GetComponent<QuestInfo>().quest; // Grab the quest from this game object
        // Ensure the attached quest has not yet been completed
        if(!Quests.IsCompleted(attachedQuest.questName)){
            // Ensure that the attached quest has not yet been completed
            if(!Quests.ongoingQuests.Contains(attachedQuest)){
                // Add this quest to the list of quests
                Quests.ongoingQuests.Add(attachedQuest); // Add this game object's quest to the list of ongoing quests
            }
        }
    }

    // Run the event (depending on required completion status)
    public void RunEvent(){
        // Check if there is no required quest, or if it is completed
        if(requiredQuest == "" || Quests.IsCompleted(requiredQuest)){
            succeedEvent.Invoke(); // Invoke the scene events
        }
        // This runs if the required quest is not completed
        else{
            failEvent.Invoke(); // Invoke the fail events
        }
    }
}
