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
    [SerializeField]
    Transform hand; // The player's hand transform

    public Weapon weapon; // The player's current weapon
    public GameObject weaponInstance; // The instance of the weapon's prefab
    float nextAttack; // The time the player can next attack

    public static PlayerAttack instance; // The current instance of the player's attack class

    // This will select the weapon the player is using
    public void SelectWeapon(Weapon toSelect){
        // Destroy currently held weapon instance if the player has one
        if(weaponInstance){
            Destroy(weaponInstance);
        }

        weapon = toSelect; // Overwrite previous weapon
        // Check if the player is now holding a weapon
        if(weapon){
            weaponInstance = Instantiate(weapon.weaponPrefab, hand, false); // Instantiate the weapon on the player's hand
        }
    }

    // Runs before first frame update
    void Start(){
        instance = this;

        // Auto select weapon on start if the player has one
        if(weapon){
            SelectWeapon(weapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CursorManager.instance.cursorLockers.Count > 0){return;} // Do not run code if mouse is unlocked

        // Ensure the player has a weapon and is able to attack
        if(weapon != null && Time.time > nextAttack){
            // Check if the player has pressed the attack button
            if(Input.GetButton("Fire1")){
                nextAttack = Time.time + weapon.delay; // Add the weapon's attack delay to the wait time
                anim.SetTrigger("DoAttack"); // Do the attack animation
                anim.SetInteger("AttackType", weapon.attackAnimation); // Set the attack type

                RaycastHit hit; // The hit info the for below spherecast
                if(Physics.SphereCast(transform.position, weapon.attackRadius, transform.forward, out hit, weapon.range)){
                    Health hitHealth = hit.collider.GetComponent<Health>(); // The health for the hit object
                    // Check that the hit object has health
                    if(hitHealth != null){
                        hitHealth.TakeDamage(weapon.damage * PlayerStats.StrengthAmount); // Deal damage to the hit object
                        PlayerStatEvents.instance.AddStrengthXP(weapon.damage / 10f); // Give the player strength depending on the damage dealt
                    }
                }
            }
        }
    }
}
