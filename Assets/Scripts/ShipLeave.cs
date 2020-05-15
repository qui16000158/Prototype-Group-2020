using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// James Quinney - QUI16000158
// This script will initiate the ship's take off
// when determined by the player pressing the Use
// button (E). The script also displays a message
// when the player is close to the ship
public class ShipLeave : MonoBehaviour
{
    [SerializeField]
    Animator shipAnimator; // The animator for the ship

    [SerializeField]
    GameObject player; // Allows us to disable the player before take off
    [SerializeField]
    GameObject shipCamera; // Allows us to enable the ship camera before take off
    [SerializeField]
    GameObject shipLeaveText; // This is the text that shows whether the player can leave
    [SerializeField]
    float takeOffTime = 4.5f; // The time it takes to load the new scene from entering the ship
    [SerializeField]
    int sceneToLoad = 0; // The scene that loads when entering the ship

    bool touchingPlayer; // Whether the ship is touching the player
    float touchDecay; // The time the ship will stop displaying the ship message

    // Check if actively triggered
    void OnTriggerStay(Collider other){
        // Check if being triggered by the player
        if(other.tag == "Player"){
            // Check if not already touching player
            if(!touchingPlayer){
                touchingPlayer = true; // Set as touching player
                touchDecay = Time.time + 0.1f; // Display the message for 0.1 seconds
                shipLeaveText.SetActive(true); // Enable ship leave text
            }
            // Runs if touching the player
            else{
                touchDecay = Time.time + 0.1f; // Extend touch decay time by 0.1 seconds (from current time)
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the touch decay has not yet passed
        if(touchDecay > Time.time){
            // Check if the player has pressed the use button
            if(Input.GetButtonDown("Use")){
                StartCoroutine(TakeOff()); // Start the takeoff coroutine
            }
        }
        // If the touch decay has passed and the ship leave text is still active
        else if(shipLeaveText.activeSelf){
            shipLeaveText.SetActive(false); // Disable ship leave text
            touchingPlayer = false; // Set as no longer touching the player
        }
    }

    // This runs on take off, disables the player, enables ship camera, players take off sequence and loads scene
    IEnumerator TakeOff(){
        player.SetActive(false); // Disable the player
        shipCamera.SetActive(true); // Enable the ship camera
        shipAnimator.SetTrigger("Take Off"); // Start take off animation
        yield return new WaitForSeconds(takeOffTime); // Wait for the take off time to pass
        SceneManager.LoadScene(sceneToLoad); // Load the scene
    }
}
