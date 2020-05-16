using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158 - James Quinney
// This script rotates the player's camera
// based on mouse input
public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueBox; // A reference to the dialogue box

    [SerializeField]
    Transform camera; // The player camera's transform

    [SerializeField]
    float ySpeed = 5f; // The look speed on the y axis (left/right)
    [SerializeField]
    float xSpeed = 5f; // The look speed on the x axis (up/down)

    float xRotation = 0f; // We store the total X rotation allowing us to clamp it

    // Update is called once per frame
    void Update()
    {
        if(dialogueBox.activeSelf){return;} // Do not run look code if dialogue box is active

        // Store the horizontal and vertical looking angles
        float horizontal = Input.GetAxisRaw("Mouse X") * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Mouse Y") * Time.deltaTime;

        xRotation += vertical * xSpeed; // Increase the x rotation by the vertical speed

        xRotation = Mathf.Clamp(xRotation, -30f, 30f); // Clamp the rotation at 30 degrees

        // Apply the x rotation to the camera's rotation 
        camera.localRotation = Quaternion.Euler(new Vector3(xRotation, 0f,0f));
        transform.Rotate(transform.up * horizontal * ySpeed);
    }
}
