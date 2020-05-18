using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// James Quinney - QUI16000158
// This script will manage hints
// public methods are there to be
// used by the event system
public class Hints : MonoBehaviour
{
    // These only need to be set once as they are cached as static values
    [SerializeField]
    Animator hintAnimator;
    [SerializeField]
    TMP_Text hintText;
    static Animator _hintAnimator;
    static TMP_Text _hintText;

    // Runs before first frame update
    void Awake(){
        // Check if the provided animator, and text are valid
        if(hintAnimator != null && hintText != null){
            _hintAnimator = hintAnimator; // Cache as static
            _hintText = hintText; // Cache as static
        }
    }

    public void DisplayHint(string hint){
        _hintAnimator.Play("Empty"); // Cancel current state
        _hintAnimator.Play("Hint Fade"); // Play the hint fade state
        _hintText.text = hint; // Set the hint's text
    }

    // It displays a hint, but from any class
    public static void _DisplayHint(string hint){
        _hintAnimator.Play("Empty"); // Cancel current state
        _hintAnimator.Play("Hint Fade"); // Play the hint fade state
        _hintText.text = hint; // Set the hint's text
    }
}
