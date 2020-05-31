using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This will make the NPC chase the player
// And also deal damage if close enough
public class NPCBehaviourAttack : NPCBehaviourChase
{
    [SerializeField]
    float attackStrength = 25;

    [SerializeField]
    float attackDelay = 1f;
    float nextAttack; // The next attack time relative to Time.time

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex); // Run chase's update

        // Check if it is time to attack again
        if(Time.time > nextAttack){
            nextAttack = Time.time + attackDelay; // Set when the next attack will be

            npc.playerHealth.TakeDamage(attackStrength); // Deal damage to the player
        }
    }
}
