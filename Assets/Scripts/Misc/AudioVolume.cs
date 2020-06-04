using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// James Quinney - QUI16000158
// Will allow us to logarithmically
// set the volume of an audio mixer
public class AudioVolume : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer; // The audio mixer
    static Dictionary<AudioMixer, float> sliderValues = new Dictionary<AudioMixer, float>(); // Cached values for sliders

    // Runs before first frame update
    private void Start()
    {
        // Check if a slider value has been stored
        if (sliderValues.ContainsKey(mixer))
        {
            GetComponent<Slider>().value = sliderValues[mixer]; // Restore slider value
        }
    }

    // Update the volume
    public void UpdateVolume(float level){
        sliderValues[mixer] = level; // Store the current value
        mixer.SetFloat("Volume", Mathf.Log10(level) * 20f); // Set the volume level logarithmically
    }
}
