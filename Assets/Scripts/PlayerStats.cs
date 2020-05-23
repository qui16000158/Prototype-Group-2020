using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float HealthXP{get;set;}
    public float StrengthXP{get;set;}
    public float StaminaXP{get;set;}

    const float HealthStartXP = 100;
    const float StrengthStartXP = 100;
    const float StaminaStartXP = 100;
    const float XPReqGrowth = 1.333f; // The additional XP required for each level
    const float IncPerLevel = 1.1f; // The increase in amount per level

    public float HealthLevel{
        get{
            int level = 0;
            float required = HealthStartXP;
            float xp = HealthXP;
            while(xp - required >= 0f){
                xp -= required;
                required += XPReqGrowth;
                level += 1;
            }

            return level;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
