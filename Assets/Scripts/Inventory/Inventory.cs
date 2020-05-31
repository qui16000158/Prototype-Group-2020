using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This script keeps track of items
// in the player's inventory, it is
// also responsible for adding/removing
// these items.
public class Inventory : MonoBehaviour
{
    [SerializeField]
    ItemInfo[] itemTypes; // Each item type in the game
    public static Dictionary<string, ItemInfo> dictItemTypes; // Each item in the game, with their name as the key

    [SerializeField]
    Transform player;
    // The player's inventory items
    public static List<Item> items = new List<Item>();
    public static Inventory instance;

    // Runs before first frame update
    void Awake(){
        instance = this;

        dictItemTypes = new Dictionary<string, ItemInfo>(); // Initialize the dictionary

        // Loop through each item type
        foreach(ItemInfo item in itemTypes){
            dictItemTypes[item.itemName] = item; // Store the item in the dictionary
        }
    }

    // Adds an item to the inventory
    public void Add(ItemInfo toAdd){
        items.Add(new Item{Info = toAdd.itemName}); // Add a copy of the item to the player's inventory
        Hints._DisplayHint(toAdd.itemName + " added to inventory.");
    }

    // Removes an item from the inventory by name
    public void RemoveByName(string toRemove){
        // Loop through each item
        foreach(Item item in items){
            // Check if the item's name matches the given string
            if(item.info.itemName == toRemove){
                items.Remove(item); // Remove the item from the list
                Hints._DisplayHint(toRemove + " removed from inventory.");
                break;
            }
        }
    }

    // Removes an item from the inventory
    public void Remove(Item toRemove){
        items.Remove(toRemove); // Remove the item from the list
        Hints._DisplayHint(toRemove.info.name + " removed from inventory.");
    }

    // Drop an item using its 
    public void DropItem(Item toDrop){
        // Check if the weapon is currently equipped
        if(toDrop.info.weapon == PlayerAttack.instance.weapon){
            PlayerAttack.instance.SelectWeapon(null); // Unequip the weapon
        }

        Remove(toDrop); // Remove the item from the player's inventory
        // Spawn the item in front of the player
        GameObject prefab = Instantiate(toDrop.info.prefab, player.position + player.forward, Quaternion.identity);
    }
}
