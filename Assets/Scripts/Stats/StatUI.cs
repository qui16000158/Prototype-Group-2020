using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// James Quinney - QUI16000158
// This assigns UI-specific values
// for the stat system.
public class StatUI : MonoBehaviour
{
    [SerializeField]
    GameObject statMenu; // This is the stat menu object

    // The header text that shows the current maximum for value for each stat
    [SerializeField]
    TMP_Text baseAmount;
    [SerializeField]
    TMP_Text healthAmount;
    [SerializeField]
    TMP_Text staminaAmount;
    [SerializeField]
    TMP_Text strengthAmount;

    // The percentage towards next level
    [SerializeField]
    TMP_Text basePercentText;
    [SerializeField]
    TMP_Text healthPercentText;
    [SerializeField]
    TMP_Text staminaPercentText;
    [SerializeField]
    TMP_Text strengthPercentText;

    // The current level for each stat
    [SerializeField]
    TMP_Text baseLevel;
    [SerializeField]
    TMP_Text healthLevel;
    [SerializeField]
    TMP_Text staminaLevel;
    [SerializeField]
    TMP_Text strengthLevel;

    // The next level for each stat
    [SerializeField]
    TMP_Text baseNextLevel;
    [SerializeField]
    TMP_Text healthNextLevel;
    [SerializeField]
    TMP_Text staminaNextLevel;
    [SerializeField]
    TMP_Text strengthNextLevel;

    // Progression image for next level
    [SerializeField]
    Image baseProgress;
    [SerializeField]
    Image healthProgress;
    [SerializeField]
    Image staminaProgress;
    [SerializeField]
    Image strengthProgress;

    // Pushes stat data onto UI
    public void GenerateInfo(){
        // Set the "amount" text values
        healthAmount.text = "Health: " + PlayerStats.HealthAmount;
        staminaAmount.text = "Stamina: " + PlayerStats.StaminaAmount;
        strengthAmount.text = "Strength: " + PlayerStats.StrengthAmount;
        baseAmount.text = "Level: " + PlayerStats.Level;

        // Store the player's next level for each stat
        int nextHealthLevel = PlayerStats.HealthLevel + 1;
        int nextStaminaLevel = PlayerStats.StaminaLevel + 1;
        int nextStrengthLevel = PlayerStats.StrengthLevel + 1;
        int nextBaseLevel = PlayerStats.Level + 1;

        // Work out percentage of the way from previous level, to current level for health
        float healthPercent = (
            (PlayerStats.HealthXP - 
            PlayerStats.GetXP(PlayerStats.HealthLevel, PlayerStats.HealthStartXP)) /
            (PlayerStats.GetXP(nextHealthLevel, PlayerStats.HealthStartXP) - 
            PlayerStats.GetXP(PlayerStats.HealthLevel, PlayerStats.HealthStartXP))
        );

        // Work out percentage of the way from previous level, to current level for stamina
        float staminaPercent = (
            (PlayerStats.StaminaXP - 
            PlayerStats.GetXP(PlayerStats.StaminaLevel, PlayerStats.StaminaStartXP)) /
            (PlayerStats.GetXP(nextStaminaLevel, PlayerStats.StaminaStartXP) - 
            PlayerStats.GetXP(PlayerStats.StaminaLevel, PlayerStats.StaminaStartXP))
        );

        // Work out percentage of the way from previous level, to current level for strength
        float strengthPercent = (
            (PlayerStats.StrengthXP - 
            PlayerStats.GetXP(PlayerStats.StrengthLevel, PlayerStats.StrengthStartXP)) /
            (PlayerStats.GetXP(nextStrengthLevel, PlayerStats.StrengthStartXP) - 
            PlayerStats.GetXP(PlayerStats.StrengthLevel, PlayerStats.StrengthStartXP))
        );

        // Work out percentage of the way from previous level, to current base level
        float basePercent = (
            (PlayerStats.LevelXP - 
            PlayerStats.GetXP(PlayerStats.Level, PlayerStats.LevelStartXP)) /
            (PlayerStats.GetXP(nextBaseLevel, PlayerStats.LevelStartXP) - 
            PlayerStats.GetXP(PlayerStats.Level, PlayerStats.LevelStartXP))
        );

        // Set the fill amount, to the percent amount
        healthProgress.fillAmount = healthPercent;
        staminaProgress.fillAmount = staminaPercent;
        strengthProgress.fillAmount = strengthPercent;
        baseProgress.fillAmount = basePercent;

        // Display percentage text "floored to convert to integer"
        healthPercentText.text = Mathf.Floor(healthPercent*100f) + "%";
        staminaPercentText.text = Mathf.Floor(staminaPercent*100f) + "%";
        strengthPercentText.text = Mathf.Floor(strengthPercent*100f) + "%";
        basePercentText.text = Mathf.Floor(basePercent*100f) + "%";

        // Display current level
        healthLevel.text = ""+PlayerStats.HealthLevel;
        staminaLevel.text = ""+PlayerStats.StaminaLevel;
        strengthLevel.text = ""+PlayerStats.StrengthLevel;
        baseLevel.text = ""+PlayerStats.Level;

        // Display next level
        healthNextLevel.text = ""+nextHealthLevel;
        staminaNextLevel.text = ""+nextStaminaLevel;
        strengthNextLevel.text = ""+nextStrengthLevel;
        baseNextLevel.text = ""+nextBaseLevel;
    }

    // This will toggle the stat menu
    public void ToggleStatMenu(){
        statMenu.SetActive(!statMenu.activeSelf); // Toggle active status for stat menu

        // Check if the stat menu is now active
        if(statMenu.activeSelf){
            CursorManager.instance.Add("Stat Menu"); // Unlock the cursor

            GenerateInfo(); // Generate stat info when menu opened
        }
        else{
            CursorManager.instance.Remove("Stat Menu"); // Attempt to lock the cursor
        }
    }
}
