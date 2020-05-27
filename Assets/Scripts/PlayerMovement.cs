using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    Image staminaBar; // The player's stamina bar
    [SerializeField]
    float staminaDepletionRate = 1f; // The rate in which the player's stamina decreases

    float stamina; // The player's stamina
    float maxStamina; // The player's max stamina
    bool staminaReplenish; // Whether the player needs to replenish stamina before running again

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

    // Runs before first frame update
    void Start(){
        UpdateMaxStamina(); // Update the player's max stamina

        stamina = maxStamina; // Set the player's stamina to max
    }

    // This will update the player's max stamina
    public void UpdateMaxStamina(){
        maxStamina = PlayerStats.StaminaAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if(CursorManager.instance.cursorLockers.Count > 0){return;} // Do not run the code is cursor is unlocked

        // Our speed values are multiplied by Time.deltaTime which will equate to 1 every 1 second
        // This will help us to avoid speed being dependent on framerate

        // Set the speed multiplier to run speed if button is held, otherwise just regular speed
        float speedMultiplier = Input.GetButton("Speed") ? speed * runSpeedMultiplier : speed;

        // Check if the player's stamina needs to replenish
        if(staminaReplenish){
            // Check if the player has recovered 25% of their stamina
            if(stamina > maxStamina/4f){
                staminaReplenish = false; // Allow the player to run again
                staminaBar.color = Color.green; // Set the stamin bar to green
            }
        }
        // Check if player is running
        else if(speed != speedMultiplier){
            // Deplete the player's stamina with a minumum of 0
            stamina = Mathf.Max(0f, stamina - Time.deltaTime * staminaDepletionRate);
            PlayerStatEvents.instance.AddStaminaXP(Time.deltaTime); // Increate player stamina by 1 every second of running
        }

        // Check if the player is running
        if(speed != speedMultiplier){
            // Check if the player has no stamina left
            if(stamina <= 0){
                staminaReplenish = true; // Stop the player from running
                staminaBar.color = Color.red; // Set the stamina bar to red
            }
        }
        // If the player is not running
        else{
            // Increase stamina by depletion rate with a max of the max stamina
            stamina = Mathf.Min(maxStamina, stamina + Time.deltaTime * staminaDepletionRate);
        }

        // Check if the player needs to replenish stamina
        if(staminaReplenish){
            speedMultiplier = speed; // Remove any speed boosts from running
        }

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

        staminaBar.fillAmount = stamina/maxStamina; // Update stamina bar
    }
}
