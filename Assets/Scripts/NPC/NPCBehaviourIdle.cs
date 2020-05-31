using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// Runs when an NPC has nothing better to do
// They will just move between waypoints and
// wait when reaching one
public class NPCBehaviourIdle : NPCBehaviourBase
{
    int currentWaypoint = 0; // The NPC's current waypoint

    // Runs every frame
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        // Ensure the NPC isn't waiting (Ensure agent is enabled)
        if(!animator.GetBool("IsWaiting") && npc.agent.enabled){
            if(npc.waypoints.Length == 0) return;
            // Direct the NPC towards the next waypoint
            npc.agent.SetDestination(npc.waypoints[currentWaypoint].position);
            
            // Check if the NPC has reached the waypoint
            if(Vector3.Distance(npc.transform.position, npc.waypoints[currentWaypoint].position) < 1.5f){
                currentWaypoint = (currentWaypoint + 1) % npc.waypoints.Length; // Change to next waypoint in array
                npc.StartWaiting(animator); // Start waiting
            }
        }
    }

    // Runs on state exit
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        animator.SetBool("IsWaiting", false); // Remove waiting flag
        npc.StopAllCoroutines(); // Ensure the NPC stops waiting
    }
}
