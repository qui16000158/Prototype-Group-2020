using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// James Quinney - QUI16000158
// This will fade the player's screen
// if they leave the map bounds and teleport
// them back to where they started leaving
public class DistanceFader : MonoBehaviour
{
    [SerializeField]
    Image fadeImage; // The image that will fade based on red zone
    [SerializeField]
    Transform player; // The player's transform
    
    [SerializeField]
    float radius = 50f; // The radius of the safe zone
    [SerializeField]
    float redZone = 10f; // The safe zone outside of the main radius, once this area is left the player will teleport
    Vector3 entryPoint; // Where the player entered the red zone
    bool inRedZone; // Whether the player is in the red zone
    
    // Update is called once per frame
    void Update()
    {
        if(player == null || !player.gameObject.activeSelf) return; // Return if there is no player

        // Check if the player has entered the red zone
        if(!inRedZone && Vector3.Distance(player.position, transform.position) > radius){
            inRedZone = true; // Mark the player as being in the red zone
            entryPoint = player.position; // Mark the position the player entered the red zone
            Hints._DisplayHint("You are leaving the safe zone, please turn around");
        }

        // Check if the player is in the red zone
        if(inRedZone){
            // The player's distance (within the red zone)
            float dist = Vector3.Distance(player.position, transform.position) - radius;
            // Check if the player has left the red zone
            if(dist < 0f){
                inRedZone = false; // Mark the player has not being in the red zone
            }
            else{
                float percent = dist / redZone; // Get the player's percentage into the red zone

                fadeImage.color = new Color(0f,0f,0f,percent); // Set the fade image colour based on percentage into red zone

                // Check if the player has left the safe zone
                if(percent >= 1f){
                    player.position = entryPoint; // Put the player back to their entry point

                }
            }
        }
        else{
            fadeImage.color = new Color(0f,0f,0f,0f); // Make the fade image invisible
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.green; // Make the safe zone green
        Gizmos.DrawWireSphere(transform.position, radius); // Draw the safe zone

        Gizmos.color = Color.red; // Make the red zone red
        Gizmos.DrawWireSphere(transform.position, radius + redZone); // Draw the red zone
    }
}
