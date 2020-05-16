using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// James Quinney - QUI16000158
// This script simply finds the QuestUI instance
// caches it and tells it to display tasks when
// a button is clicked
public class QuestButton : MonoBehaviour
{
    QuestUI questInstance; // The quest UI's instance

    // Start is called before the first frame update
    void Start()
    {
        // Cache the quest instance
        questInstance = (QuestUI)FindObjectOfType(typeof(QuestUI));
    }

    // This will display tasks of this button's quest
    public void DisplayTasks(){
        questInstance.DisplayTasks(name);
    }
}
