using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This script contains a method that
// will activate the player and disable the
// ship's camera upon landing
public class ShipLand : MonoBehaviour
{
    [SerializeField]
    UnityEvent onLand;

    // This method runs when the ship lands
    public void OnLand(){
        onLand.Invoke(); // Invoke the on land event
    }
}
