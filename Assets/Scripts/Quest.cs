using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class holds quest names, and their tasks
// We can also check if the quest is completed
[System.Serializable]
public class Quest{
    public string questName; // The name of the quest
    public List<string> tasks; // The tasks required to complete the quest

    public Quest(string _questName, params string[] _tasks){
        this.questName = _questName; // Set the name of the quest

        this.tasks = new List<string>(); // Create a new list to hold tasks

        // Loop through each task
        for(int i = 0;i<_tasks.Length;i+=1){
            this.tasks.Add(_tasks[i]); // Add each task to the list of tasks to complete
        }
    }

    // This will check whether the quest has been completed and add it to completed if it is
    public void CheckCompleted(){
        // Check if there are no tasks left
        if(tasks.Count == 0){
            Quests.ongoingQuests.Remove(this); // Remove this quest from the list of ongoing quests
            Quests.completedQuests.Add(questName); // Add this quest to the list of completed quests
        }
    }

    // This will mark a task as completed and then check if the quest is complete
    public void CompleteTask(string taskName){
        // Ensure the task is within the list
        if(tasks.Contains(taskName)){
            tasks.Remove(taskName); // Remove the task from the list
        }

        CheckCompleted(); // Check if the quest has been completed
    }

    // Override default equals operator
    public override bool Equals(System.Object other){
        // return false if other is not valid
        if(other == null){
            return false;
        }

        Quest otherQuest = other as Quest; // Cast other to a quest type

        // Return whether the other object's quest type is equal to this object's quest type
        return otherQuest.questName == this.questName;
    }
}

// James Quinney - QUI16000158
// This class handles the player's current
// and completed quests
public static class Quests
{
    // A list of ongoing quests
    public static List<Quest> ongoingQuests = new List<Quest>();
    // A list of all completed quests
    public static List<string> completedQuests = new List<string>();

    // This method will add a quest to the ongoing quests
    public static void Add(string questName, params string[] tasks){
        // Ensure the quest is not completed, and is also not ongoing
        if(!completedQuests.Contains(questName) && Get(questName) == null){
            ongoingQuests.Add(new Quest(questName, tasks)); // Add the new quest to the ongoing quests
        }
    }

    // This will attempt to get a specific ongoing quest
    public static Quest Get(string questName){
        // Loop through each ongoing quest
        foreach(Quest current in ongoingQuests){
            // Check if the current quest name is the one we are looking for
            if(current.questName == questName){
                return current; // Return that quest
            }
        }

        return null; // Return null if no quest was found
    }

    // This method will return whether a quest has been completed by name
    public static bool IsCompleted(string questName){
        return completedQuests.Contains(questName); // Return whether the name exists within the completed quests list
    }
}
