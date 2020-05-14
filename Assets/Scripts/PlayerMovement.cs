using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney (QUI16000158)
// This is the player's movement script
// It moves the player based on input
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueBox; // A reference to the dialogue box

    [SerializeField]
    float speed;

    [SerializeField]
    Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        if(dialogueBox.activeSelf){return;} // Do not run the method if dialogue box is active

        // Our speed values are multiplied by Time.deltaTime which will equate to 1 every 1 second
        // This will help us to avoid speed being dependent on framerate

        // The player's horizontal/vertical axes
        float horizontal = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        float yVel = rb.velocity.y; // Cache the player's y velocity

        // Set the player's velocity by multiplying their transform's orientation by the respective axis value
        // We maintain the player's Y velocity by multiplying the cached version by "up" and multiplying it with ^
        rb.velocity = (
            transform.forward * vertical + 
            transform.right * horizontal
        ).normalized * speed + Vector3.up * yVel;
        // Normalizing the left/right vector allows us to avoid diagonal being faster
    }
}
