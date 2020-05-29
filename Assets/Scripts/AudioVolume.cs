using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// James Quinney - QUI16000158
// Will allow us to logarithmically
// set the volume of an audio mixer
public class AudioVolume : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer; // The audio mixer

    // Update the volume
    public void UpdateVolume(float level){
        mixer.SetFloat("Volume", Mathf.Log10(level) * 20f); // Set the volume level logarithmically
    }
}
