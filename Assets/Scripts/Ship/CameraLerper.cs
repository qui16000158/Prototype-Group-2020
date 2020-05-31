using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This will smoothly interpolate
// the camera's position towards an
// anchor point
public class CameraLerper : MonoBehaviour
{
    [SerializeField]
    Transform anchor; // The anchor object

    [SerializeField]
    float moveSpeed = 2.0f; // The speed the camera moves

    // Fixed Update is called once per physics frame
    void FixedUpdate()
    {
        // Move the camera towards the anchor's position using the move speed (adjusted for frame time)
        transform.position = Vector3.Slerp(transform.position, anchor.position, moveSpeed * Time.deltaTime);
        // Rotate the camera towards the anchor's angle using the move speed (adjusted for frame time)
        transform.rotation = Quaternion.Slerp(transform.rotation, anchor.rotation, moveSpeed * Time.deltaTime);
    }
}
