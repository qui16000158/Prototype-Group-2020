using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

// James Quinney - QUI16000158
// This script will allow the player to
// click on links in TextMeshPro text when
// set up correctly to load event or advance
// dialogue
public class TMP_LinkClicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    TMP_Text linkText; // The text that contains our clickable links

    // Runs when clicked
    public void OnPointerClick(PointerEventData data){
        DialogueManager manager = DialogueManager.activeManager; // Cache active dialogue manager

        // Try to grab the index of the clicked link (-1 if none were clicked)
        int index = TMP_TextUtilities.FindIntersectingLink(linkText,data.position,null);
        // Check to ensure a link was clicked
        if(index != -1){
            manager?.LinkEvent(index).Invoke(); // Invoke the on click event for that index
        }
        // If a link was not clicked
        else{
            manager?.LoadNextDialogue(); // Load the next dialogue
        }
    }
}
