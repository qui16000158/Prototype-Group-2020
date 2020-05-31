using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This is the base behaviour script
// for all NPCs, this allows them to
// access the NPC passthrough script.
public class NPCBehaviourBase : StateMachineBehaviour
{
    public NPC_PassThrough npc; // The NPC passthrough for this NPC

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ensure the npc passthrough has not yet been set
        if(npc == null){
            npc = animator.GetComponent<NPC_PassThrough>(); // Grab the NPC passthrough from the object
        }

        LateStateEnter(animator, stateInfo, layerIndex); // Start late state enter
    }

    // Runs just after this class's state enter, easier to visualise this way
    public virtual void LateStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        // Will be overridden
    }
}
