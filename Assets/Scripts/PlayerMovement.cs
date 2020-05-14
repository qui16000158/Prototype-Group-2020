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
    float runSpeedMultiplier;

    [SerializeField]
    float jumpPower;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    Animator anim; // The player's animator

    [SerializeField]
    Transform groundChecker; // The ground checker's transform

    // This will disable both the running and walking booleans
    void SetIdle(){
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", false);
    }

    // This will enable the walking boolean whilst disabling the running boolean
    void SetWalking(){
        anim.SetBool("IsWalking", true);
        anim.SetBool("IsRunning", false);
    }

    // This will enable the running boolean whilst disabling the walking boolean
    void SetRunning(){
        anim.SetBool("IsRunning", true);
        anim.SetBool("IsWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueBox.activeSelf){return;} // Do not run the method if dialogue box is active

        // Our speed values are multiplied by Time.deltaTime which will equate to 1 every 1 second
        // This will help us to avoid speed being dependent on framerate

        // Set the speed multiplier to run speed if button is held, otherwise just regular speed
        float speedMultiplier = Input.GetButton("Speed") ? speed * runSpeedMultiplier : speed;

        // The player's horizontal/vertical axes
        float horizontal = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        float yVel = rb.velocity.y; // Cache the player's y velocity

        // Check if anything is within radius of the ground checker (Only checks default layer)
        bool touchingGround = Physics.CheckSphere(groundChecker.position, 0.2f, 1 << 0);

        // Check if the player is touching the ground
        if(touchingGround){
            // Check if the player is moving
            if(horizontal != 0.0f || vertical != 0.0f){
                // Check if the player is walking
                if(speedMultiplier == speed){
                    SetWalking(); // Set the player's walking animation
                }
                else{
                    SetRunning(); // Set the player's running animation
                }
            }
            else{
                SetIdle(); // Set the player's idle animation
            }

            // Check if the player is trying to jump
            if(Input.GetButtonDown("Jump")){
                yVel = jumpPower; // Apply jump power to y velocity
                anim.SetTrigger("DoJump"); // Play the jumping animation
            }
        }

        // Set the player's velocity by multiplying their transform's orientation by the respective axis value
        // We maintain the player's Y velocity by multiplying the cached version by "up" and multiplying it with ^
        rb.velocity = (
            transform.forward * vertical + 
            transform.right * horizontal
        ).normalized * speedMultiplier + Vector3.up * yVel;
        // Normalizing the left/right vector allows us to avoid diagonal being faster
    }
}
