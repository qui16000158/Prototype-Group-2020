using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// James Quinney - QUI16000158
// This script simply keeps track
// of the player's persistent data
// this will allow for saving/loading too
[System.Serializable]
public class PData
{
    // Data that will allow us to interface with the quest system across save games
    public List<Quest> ongoingQuests = new List<Quest>();
    public List<string> completedQuests = new List<string>();

    public void Save(string dataPath){
        // Create a new file in the save directory
        using(FileStream fs = new FileStream(dataPath, FileMode.Create)){
            BinaryFormatter bin = new BinaryFormatter(); // Create a new binary formatter
            bin.Serialize(fs, this); // Serialize this class into our data file
        }
    }

    public static void ClearData(string dataPath){
        // Create a new file in the save directory
        using(FileStream fs = new FileStream(dataPath, FileMode.Create)){
            BinaryFormatter bin = new BinaryFormatter(); // Create a new binary formatter
            bin.Serialize(fs, new PData()); // Serialize blank pdata into our data file
        }
    }

    public static PData Load(string dataPath){
        if(File.Exists(dataPath)){
            // Open the data file
            using(FileStream fs = new FileStream(dataPath, FileMode.Open)){
                BinaryFormatter bin = new BinaryFormatter(); // Create a new binary formatter
                return (PData)bin.Deserialize(fs); // Deserialize the data file, cast to pdata, and return
            }
        }
        return new PData();
    }
}
