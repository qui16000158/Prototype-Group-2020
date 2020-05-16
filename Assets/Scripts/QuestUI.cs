using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// James Quinney - QUI16000158
// This will generate buttons to display
// information about the player's quests
// and the tasks needed to complete them
public class QuestUI : MonoBehaviour
{
    [SerializeField]
    GameObject questCanvas; // The quest canvas gameobject

    [SerializeField]
    GameObject questPanel; // The quest panel gameobject

    [SerializeField]
    GameObject buttonPrefab; // The quest button prefab

    [SerializeField]
    Text taskText; // The task display text

    // A list of all active quest buttons
    List<GameObject> questButtons = new List<GameObject>();

    // This will display the tasks or a specific quest
    public void DisplayTasks(string questName){
        Quest quest = Quests.Get(questName); // Attempt to grab the quest

        // Check that the quest is valid
        if(quest != null){
            string tasks = $"Tasks for {questName}\n\n"; // Create a string to hold the tasks for the quest

            // Loop through each of the quest's tasks
            for(int i = 0;i<quest.tasks.Count;i+=1){
                tasks += $"{i+1}. {quest.tasks[i]}\n"; // Append the task name, and number
            }

            taskText.text = tasks; // Set the task text
        }
    }

    // This will remove all quest buttons and replace them
    void ResetButtons(){
        // Loop through eacj button
        foreach(GameObject toDestroy in questButtons){
            Destroy(toDestroy); // Destroy the current button
        }

        for(int i = 0;i<Quests.ongoingQuests.Count;i+=1){
            // Cache the quest panel's rect transform
            RectTransform qt = questPanel.GetComponent<RectTransform>();

            // Instantiate button prefab, and attach to quest panel
            GameObject newButton = Instantiate(buttonPrefab, qt);

            // Cache the current button's rect transform
            RectTransform bt = newButton.GetComponent<RectTransform>();

            // Offset based on current iteration
            bt.anchoredPosition = new Vector2(
                (bt.sizeDelta.x/2f),
                -(bt.sizeDelta.y/2f) - bt.sizeDelta.y * i
            );
            // Set the button's text the the name of its quest
            newButton.transform.GetChild(0).GetComponent<Text>().text = Quests.ongoingQuests[i].questName;
            newButton.name = Quests.ongoingQuests[i].questName; // Set the button name, to the quest name
            questButtons.Add(newButton); // Store the new quest button
        }
    }

    // This will toggle the quest canvas
    public void ToggleCanvas(){
        questCanvas.SetActive(!questCanvas.activeSelf); // Toggle the quest canvas
        // Lock the cursor if quest canvas closed, otherwise open it
        if(questCanvas.activeSelf){
            CursorManager.instance.Add("Quest UI"); // Add quest ui to list of cursor unlockers
        }
        else{
            CursorManager.instance.Remove("Quest UI"); // Remove quest ui from list of cursor unlockers.
        }

        ResetButtons(); // Reset quest buttons when canvas is toggled

        // Check if there are ongoing quests
        if(Quests.ongoingQuests.Count > 0){
            DisplayTasks(Quests.ongoingQuests[0].questName); // Display information for the first quest
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has pressed the quest button (default TAB)
        if(Input.GetButtonDown("Quest")){
            ToggleCanvas(); // Toggle the quest canvas
        }
    }
}
