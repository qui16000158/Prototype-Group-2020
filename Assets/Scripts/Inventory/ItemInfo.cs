﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// A simple container class for in-game
// items.
[CreateAssetMenu, System.Serializable]
public class ItemInfo : ScriptableObject
{
    public string itemName; // The name of the item
    public GameObject prefab; // The prefab that spawns when the item is dropped
    public bool droppable; // Whether this item can be dropped

    public UnityEvent onUse; // The event that runs when this item is used

    public Weapon weapon; // The weapon linked to this item (if any)

    // Invoke a scene event
    public void InvokeSceneEvent(string eventName){
        SceneEventManager.events[eventName].RunEvent(); // Run the scene event
    }

    // Equip a weapon
    public void EquipWeapon(Weapon weapon){
        PlayerAttack.instance.SelectWeapon(weapon); // Select the weapon
    }

    // Removes an item from the player's inventory
    public void RemoveFromInventory(string toRemove){
        Inventory.instance.RemoveByName(toRemove); // Remove the item from the inventory
    }
}

// An individual item (added to the player's inventory)
[System.Serializable]
public class Item{
    public string Info; // The info for this item
    public ItemInfo info{
        get{
            return Inventory.dictItemTypes[Info]; // Return the stored item type for this item
        }
    }
}
