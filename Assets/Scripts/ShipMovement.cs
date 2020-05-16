using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This script simply handles the
// ship's movement in space.
public class ShipMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb; // The ship's rigidbody
    [SerializeField]
    float speed = 2.0f; // The default speed of the ship
    [SerializeField]
    float boostSpeedMultiplier = 5.0f; // The speed multiplier when holding the speed button (shift default)

    [SerializeField]
    float turnSpeed = 5.0f; // The turning speed

    // Update is called once per frame
    void Update()
    {
        // We can now grab the user's vertical/horizontal inputs and adjust them for frame time
        float horizontal = Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * turnSpeed * Time.deltaTime;

        // Push the ship forward by its base speed if the speed button is not pressed
        // otherwise, multiply default speed by the modifier, we also adjust the speed
        // for frame time
        rb.velocity = (
            -transform.up *
            (Input.GetButton("Speed") ? speed * boostSpeedMultiplier : speed) *
            Time.deltaTime
        );

        transform.Rotate(new Vector3(-vertical, 0f, horizontal)); // Apply rotation
    }
}
