using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// James Quinney - QUI16000158
// This will invoke an event whenever
// the attached object is triggered by
// a correctly tagged object.
public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    UnityEvent onTrigger; // The event to run on trigger
    [SerializeField]
    Animator fadeAnimator; // The animator for the fade to black animation
    [SerializeField]
    string triggerTag; // The tag required of the colliding object

    // Runs on trigger
    IEnumerator OnTriggerEnter(Collider other){
        if(other.tag == triggerTag){
            // Check if a fade animator is present
            if(fadeAnimator != null){
                fadeAnimator.SetTrigger("Fade"); // Start fade animation
                yield return new WaitForSeconds(0.5f); // Wait before invoking
            }
            onTrigger.Invoke(); // Run the on trigger events
        }
        yield return null;
    }
}
