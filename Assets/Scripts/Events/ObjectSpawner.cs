using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This will spawn an object when called upon
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    Transform spawnAt; // The transform to spawn at
    // This will spawn an object at the transform's origin
    public void Spawn(GameObject toSpawn){
        Instantiate(toSpawn, spawnAt.position, Quaternion.identity);
    }

    // This will set the spawn object
    public void SetSpawn(Transform spawnPos){
        spawnAt = spawnPos; // Change the spawn transform
    }
}
