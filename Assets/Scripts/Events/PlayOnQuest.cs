using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This script will allow us to load
// specific events depending on which
// quest has been completed, quests
// are checked in ascending 0, 1, 2
// order
public class PlayOnQuest : MonoBehaviour
{
    // Holds info about a quest, and the event to run if completed
    [System.Serializable]
    public class QuestEvent{
        public string questName; // Quests to check
        public UnityEvent runIfCompleted; // Events to run if the quest of the same index is completed
    }

    [SerializeField]
    QuestEvent[] questEvents; // The quests and events to check
    [SerializeField]
    UnityEvent runOnFailed; // The event to run if all others fail

    // Run the events
    public void RunEvents(){
        // Loop through each quest event
        for(int i = questEvents.Length - 1;i>=0;i-=1){
            // Check if the quest is completed
            if(Quests.IsCompleted(questEvents[i].questName)){
                questEvents[i].runIfCompleted.Invoke(); // Run the "if completed" event
                return; // End execution
            }
        }

        runOnFailed.Invoke(); // Run the on failed event
    }
}
