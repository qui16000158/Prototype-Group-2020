using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This script will make an NPC stop
// moving and face towards the player
// when talking to the player
public class NPCBehaviourTalking : NPCBehaviourBase
{
    float storedSpeed; // Will cache the speed when overriding

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void LateStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        storedSpeed = npc.agent.speed; // Cache NPC's speed
        npc.agent.speed = 0.05f; // Stop agent from moving
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc.agent.SetDestination(npc.player.position); // Turn towards the player
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc.agent.speed = storedSpeed; // Restore npc's speed
    }
}
