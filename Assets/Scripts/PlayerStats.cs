using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This is a container class for the
// player's stats: Health, Stamina, Strength
public static class PlayerStats
{
    public static float LevelXP{get;set;}
    public static float HealthXP{get;set;}
    public static float StrengthXP{get;set;}
    public static float StaminaXP{get;set;}

    public const float HealthStartXP = 100;
    public const float StrengthStartXP = 100;
    public const float StaminaStartXP = 100;
    public const float LevelStartXP = 100;
    const float XPReqGrowth = 1.333f; // The additional XP required for each level
    public const float IncPerLevel = 1.1f; // The increase in total amount per level (e.g lv1: 100hp, lvl2: 110hp)

    // Gets the player's level
    public static int Level{
        get{
            return GetLevel(LevelStartXP, LevelXP); // Returns the player's level
        }
    }

    // Gets the player's health level
    public static int HealthLevel{
        get{
            return GetLevel(HealthStartXP, HealthXP); // Returns the player's health level
        }
    }

    // Gets the player's strength level
    public static int StrengthLevel{
        get{
            return GetLevel(StrengthStartXP, StrengthXP); // Returns the player's strength level
        }
    }

    // Gets the player's stamina level
    public static int StaminaLevel{
        get{
            return GetLevel(StaminaStartXP, StaminaXP); // Return the player's stamina level
        }
    }

    // Gets the player's max health based on stats
    public static float HealthAmount{
        get{
            return Mathf.Floor(100f*Mathf.Pow(IncPerLevel, HealthLevel)); // Return 100 (default) adjusted for level changes
        }
    }

    // Gets the player's max stamina based on stats
    public static float StaminaAmount{
        get{
            return Mathf.Floor(100f*Mathf.Pow(IncPerLevel, StaminaLevel)); // Return 100 (default) adjusted for level changes
        }
    }

    // Gets the player's max strength based on stats
    public static float StrengthAmount{
        get{
            return Mathf.Floor(Mathf.Pow(IncPerLevel, StrengthLevel)); // Return 100 (default) adjusted for level changes
        }
    }

    // This will take in a start XP, and a current XP, and return the level for that amount of XP
    public static int GetLevel(float startXP, float currentXP){
        return Mathf.FloorToInt( // Return an integer value
            Mathf.Pow(currentXP/startXP, 1f/XPReqGrowth) // Return inverse of curve
        );
    }

    // This will take in a level, and return the XP required for that level
    public static float GetXP(int level, float startXP){
        return Mathf.Pow(level, XPReqGrowth) * startXP; // Return point on curve f(x) = x^y * z
    }
}
