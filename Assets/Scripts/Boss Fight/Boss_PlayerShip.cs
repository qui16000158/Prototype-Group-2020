using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// James Quinney - QUI16000158
// This script controls the player's
// ship during the final boss fight
public class Boss_PlayerShip : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f; // The player's ship movement speed
    [SerializeField]
    Rigidbody rb; // The player's rigidbody

    float playerLaser = 0f;

    [SerializeField]
    float laserChargeRate = 0.1f; // 10 seconds to charge by default
    [SerializeField]
    Image laserImage; // The laser charge image
    [SerializeField]
    TMP_Text laserStatus; // The player's laser status text
    [SerializeField]
    Animator laserAnim; // The laser's animator

    // Update is called once per frame
    void Update()
    {
        // Store horizontal movement speed
        float horizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        rb.velocity = new Vector3(horizontal, vertical, 0f); // Set the player's velocity

        playerLaser += Time.deltaTime * laserChargeRate; // Charge player laser

        // Check if the player laser is fully charged
        if(playerLaser >= 1f){
            laserStatus.text = "Press Space to Fire!";

            // Check if jump is held (space by default)
            if(Input.GetButtonDown("Jump")){
                playerLaser = -.5f; // Reset laser charger (Ensure it doesn't start for a while again)
                laserAnim.SetTrigger("Fire Laser"); // Fire the laser
            }
        }
        else{
            laserStatus.text = "Laser Charging!";
        }

        laserImage.fillAmount = playerLaser; // Fill the laser charge image
    }
}
