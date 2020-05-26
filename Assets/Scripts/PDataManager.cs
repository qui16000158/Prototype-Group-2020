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
    public void Load(int slot = 0){
        PData currentData = PData.Load(DataPath+slot); // Load from data file

        // Assign values from data file
        Quests.ongoingQuests = currentData.ongoingQuests;
        Quests.completedQuests = currentData.completedQuests;

        PlayerStats.LevelXP = currentData.LevelXP;
        PlayerStats.HealthXP = currentData.LevelXP;
        PlayerStats.StaminaXP = currentData.StaminaXP;
        PlayerStats.StrengthXP = currentData.StrengthXP;

        Inventory.items = currentData.inventory;
    }

    // This will save game data to pdata, and then save the pdata
    public void Save(int slot = 0){
        PData currentData = new PData(); // Generate new pdata container

        // Store game data in pdata
        currentData.ongoingQuests = Quests.ongoingQuests;
        currentData.completedQuests = Quests.completedQuests;
        
        currentData.LevelXP = PlayerStats.LevelXP;
        currentData.HealthXP = PlayerStats.LevelXP;
        currentData.StaminaXP = PlayerStats.StaminaXP;
        currentData.StrengthXP = PlayerStats.StrengthXP;

        currentData.inventory = Inventory.items;

        currentData.Save(DataPath+slot); // Save pdata to disk
    }

    // This will clear your game data
    public void ClearData(int slot = 0){
        PData.ClearData(DataPath+slot); // Clear save data
    }
}
