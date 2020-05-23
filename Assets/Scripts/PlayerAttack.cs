using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This script will allow the player
// to attack enemies
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    Animator anim; // The player's animator

    public Weapon weapon; // The player's current weapon
    float nextAttack; // The time the player can next attack

    // Update is called once per frame
    void Update()
    {
        // Ensure the player has a weapon and is able to attack
        if(weapon != null && Time.time > nextAttack){
            // Check if the player has pressed the attack button
            if(Input.GetButton("Fire1")){
                nextAttack = Time.time + weapon.delay; // Add the weapon's attack delay to the wait time
                anim.SetTrigger("DoAttack"); // Do the attack animation

                RaycastHit hit; // The hit info the for below spherecast
                if(Physics.SphereCast(transform.position, weapon.attackRadius, transform.forward, out hit, weapon.range)){
                    Health hitHealth = hit.collider.GetComponent<Health>(); // The health for the hit object
                    // Check that the hit object has health
                    if(hitHealth != null){
                        hitHealth.TakeDamage(weapon.damage); // Deal damage to the hit object
                    }
                }
            }
        }
    }
}
