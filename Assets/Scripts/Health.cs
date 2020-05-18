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
    [SerializeField]
    float max; // The maximum amount of health
    [SerializeField]
    float amount; // The amount of health
    [SerializeField]
    Image healthBar; // The health bar (if any)
    [SerializeField]
    UnityEvent onDeath; // The event to run on death

    // Runs before first frame update
    void Start(){
        amount = max; // Set health to maximum
    }

    // This will allow outside classes to access the health
    public float Get(){
        return amount; // Return the player's health
    }
    
    // This will damage the object
    public void TakeDamage(float damageAmount){
        amount = Mathf.Max(amount - damageAmount, 0f); // Reduce health by damage amount (min 0 total health)

        // If it exists, update the health bar
        if(healthBar != null){
            healthBar.fillAmount = amount / max;
        }

        // Check if the object has no health left
        if(amount == 0f){
            onDeath.Invoke(); // Run death event
        }
    }

    // This will allow enemies to destroy themselves when killed
    public void Destroy(){
        Destroy(gameObject); // Destroy itself
    }
}
