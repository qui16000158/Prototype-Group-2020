using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This script loads the player's data
// and decides exactly what to do with it
public class PDataManager : MonoBehaviour
{
    // This is the path files are stored in
    static string DataPath{
        get{
            return Application.persistentDataPath + "/pdata.bin";
        }
    }
    
    // This will load player data from a file, and then assign its values
    public void Load(){
        PData currentData = PData.Load(DataPath); // Load from data file

        // Assign values from data file
        Quests.ongoingQuests = currentData.ongoingQuests;
        Quests.completedQuests = currentData.completedQuests;
    }

    // This will save game data to pdata, and then save the pdata
    public void Save(){
        PData currentData = new PData(); // Generate new pdata container

        // Store game data in pdata
        currentData.ongoingQuests = Quests.ongoingQuests;
        currentData.completedQuests = Quests.completedQuests;

        currentData.Save(DataPath); // Save pdata to disk
    }

    // This will clear your game data
    public void ClearData(){
        PData.ClearData(DataPath); // Clear save data
    }
}
