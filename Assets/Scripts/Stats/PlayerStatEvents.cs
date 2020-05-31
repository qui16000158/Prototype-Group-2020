using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This script contains events and methods
// relating to player stats and leveling up
public class PlayerStatEvents : MonoBehaviour
{
    public static PlayerStatEvents instance; // An instance to this object

    [SerializeField]
    UnityEvent onLevelUp; // Runs on player level up
    [SerializeField]
    UnityEvent onHealthLevelUp; // Runs on player health level up
    [SerializeField]
    UnityEvent onStaminaLevelUp; // Runs on player stamina level up
    [SerializeField]
    UnityEvent onStrengthLevelUp; // Runs on player strength level up
    [SerializeField]
    float mainLevelXPRate = 0.4f; // The amount of XP from other stats that adds to the base level XP

    // Runs before first frame update
    void Start(){
        instance = this;
    }

    // This method will invoke the event of the same name
    static void OnLevelUp(){
        instance.onLevelUp.Invoke(); // Invoke the event
    }

    // This method will invoke the event of the same name
    static void OnHealthLevelUp(){
        instance.onHealthLevelUp.Invoke(); // Invoke the event
    }

    // This method will invoke the event of the same name
    static void OnStaminaLevelUp(){
        instance.onStaminaLevelUp.Invoke(); // Invoke the event
    }

    // This method will invoke the event of the same name
    static void OnStrengthLevelUp(){
        instance.onStrengthLevelUp.Invoke(); // Invoke the event
    }

    // This will add XP to the player
    public void AddLevelXP(float amount){
        int oldLevel = PlayerStats.Level; // Store player's level before adding XP
        PlayerStats.LevelXP += amount; // Increase player's level XP
        // Check if the player's level has increased
        if(oldLevel < PlayerStats.Level){
            OnLevelUp(); // Trigger level up event
        }
    }

    // This will add XP to the player
    public void AddHealthXP(float amount){
        int oldLevel = PlayerStats.HealthLevel; // Store player's level before adding XP
        PlayerStats.HealthXP += amount; // Increase player's health XP
        // Check if the player's level has increased
        if(oldLevel < PlayerStats.HealthLevel){
            OnHealthLevelUp(); // Trigger health level up event
        }
        AddLevelXP(amount * mainLevelXPRate); // Increase main XP too
    }

    // This will add XP to the player
    public void AddStaminaXP(float amount){
        int oldLevel = PlayerStats.StaminaLevel; // Store player's level before adding XP
        PlayerStats.StaminaXP += amount; // Increase player's Stamina XP
        // Check if the player's level has increased
        if(oldLevel < PlayerStats.StaminaLevel){
            OnStaminaLevelUp(); // Trigger Stamina level up event
        }
        AddLevelXP(amount * mainLevelXPRate); // Increase main XP too
    }

    // This will add XP to the player
    public void AddStrengthXP(float amount){
        int oldLevel = PlayerStats.StrengthLevel; // Store player's level before adding XP
        PlayerStats.StrengthXP += amount; // Increase player's Strength XP
        // Check if the player's level has increased
        if(oldLevel < PlayerStats.StrengthLevel){
            OnStrengthLevelUp(); // Trigger Strength level up event
        }
        AddLevelXP(amount * mainLevelXPRate); // Increase main XP too
    }
}
