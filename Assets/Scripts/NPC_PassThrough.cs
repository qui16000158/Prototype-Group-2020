using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// James Quinney - QUI16000158
// This script will pass values to
// the npc's animator for use in behaviour
public class NPC_PassThrough : MonoBehaviour
{
    [SerializeField]
    Animator anim; // The NPC's animator

    public NavMeshAgent agent; // The NPC's nav mesh agent
    public Transform player; // The player's transform
    public float waitTime = 0.0f; // How long this NPC will wait at waypoints
    [SerializeField]
    bool isHostile; // Whether this NPC is hostile toward the player
    public Transform[] waypoints; // Waypoints for patrolling

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("IsHostile", isHostile); // Pass the hostile value to the NPC
        anim.SetFloat("DistanceFromPlayer", int.MaxValue); // Set the max value for distance in case there is no player
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is valid
        if(player){
            // Pass the player's distance to the animator
            anim.SetFloat("DistanceFromPlayer", Vector3.Distance(player.position, transform.position));
        }
        // Mathf.Min will just stop the value from exceeding 1
        anim.SetFloat("MovementSpeed", Mathf.Min(1f,agent.velocity.magnitude)); // Pass through NPC's current speed
    }

    // Tells the coroutine to start the wait time
    public void StartWaiting(Animator animator){
        StartCoroutine(DoWait(animator)); // Starts the wait coroutine
    }

    // This will make the NPC wait
    IEnumerator DoWait(Animator animator){
        animator.SetBool("IsWaiting", true); // Set as waiting

        yield return new WaitForSeconds(waitTime); // Start waiting

        animator.SetBool("IsWaiting", false); // Stop waiting
    }
}
