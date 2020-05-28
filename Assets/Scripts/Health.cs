using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This script manages health for
// any object that needs it, it also
// determined what should happen upon
// death using a Unity Event
public class Health : MonoBehaviour
{
    public static Health player; // The player's health instance

    public float max; // The maximum amount of health
    public float amount; // The amount of health
    public float armour; // The amount of armour
    [SerializeField]
    Image healthBar; // The health bar (if any)
    [SerializeField]
    Image armourBar; // The armour bar (if any)
    [SerializeField]
    UnityEvent onDeath; // The event to run on death
    [SerializeField]
    UnityEvent onTakeDamage; // The event to run on take damage
    [SerializeField]
    UnityEvent onHeal; // The event to run on heal

    // Runs before start
    void Awake(){
        // Store the player's health instance
        if(tag == "Player"){
            player = this;
        }
    }

    // Runs before first frame update
    void Start(){
        UpdateMaxHealth(); // Update the player's max health (If attached to the player)

        amount = max; // Set health to maximum

        if(tag == "Player"){
            // Load player's health and armour
            amount = PDataManager.playerHealth;
            armour = PDataManager.playerArmour;

            UpdateUI(); // Update the UI
        }
    }

    // This will update the player's maximum health
    public void UpdateMaxHealth(){
        if(tag == "Player"){
            max = PlayerStats.HealthAmount;
        }
    }

    // This will allow outside classes to access the health
    public float Get(){
        return amount; // Return the player's health
    }

    // This will update the UI
    public void UpdateUI(){
        // If it exists, update the health bar
        if(healthBar != null){
            healthBar.fillAmount = amount / max;
        }

        // If it exists, update the armour bar
        if(armourBar != null){
            armourBar.fillAmount = armour / max;
        }
    }
    
    // This will damage the object
    public void TakeDamage(float damageAmount){
        float armourCache = armour;
        armour = Mathf.Max(armour - damageAmount, 0f); // Reduce armour by damage amount (min 0 total armour)
        damageAmount = Mathf.Max(damageAmount - armourCache, 0f); // Reduce damage amount by the armour (min 0 damage)

        amount = Mathf.Max(amount - damageAmount, 0f); // Reduce health by damage amount (min 0 total health)

        UpdateUI(); // Update the UI

        onTakeDamage.Invoke(); // Invoke take damage event

        // Check if the object has no health left
        if(amount == 0f){
            onDeath.Invoke(); // Run death event
        }
    }

    // This will heal the player
    public void HealPlayer(float healAmount){
        // Set the player's new health, with the maximum being determined by stats
        amount = Mathf.Min(amount + healAmount, max);

        UpdateUI(); // Update the UI

        onHeal.Invoke(); // Invoke on heal events
    }

    // This will add armour
    public void AddArmour(float addAmount){
        // Set the new armour amount, with the maximum being determined by max health
        armour = Mathf.Min(armour + addAmount, max);

        UpdateUI(); // Update the UI
    }

    // This will allow enemies to destroy themselves when killed
    public void Destroy(){
        Destroy(gameObject); // Destroy itself
    }
}
