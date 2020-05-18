using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This script will make the NPC
// chase after the player
public class NPCBehaviourChase : NPCBehaviourBase
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc.agent.SetDestination(npc.player.position); // Set the NPC's destination to the player
    }
}
