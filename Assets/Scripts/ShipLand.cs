using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This script contains a method that
// will activate the player and disable the
// ship's camera upon landing
public class ShipLand : MonoBehaviour
{
    [SerializeField]
    GameObject player; // The player gameobject
    [SerializeField]
    GameObject shipCamera; // The ship's camera

    // This method runs when the ship lands
    public void OnLand(){
        player.SetActive(true); // Activate the player
        shipCamera.SetActive(false); // Deactivate the ship
    }
}
