using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// James Quinney - QUI16000158
// This script will make one object
// look towards another on the Y-axis
public class LookTowards : MonoBehaviour
{
    [SerializeField]
    Transform lookAt; // The object to look at

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAt); // Make the object look at the other object
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f); // Reset X, and Z rotation
    }
}
