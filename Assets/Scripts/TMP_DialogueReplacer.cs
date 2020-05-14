using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

// James Quinney - QUI16000158
// This script will use regular
// expressions to replace text such
// as [this] into clickable links
// in TextMeshPro
public class TMP_DialogueReplacer : MonoBehaviour
{
    [SerializeField]
    TMP_Text meshText; // A reference to the TextMeshPro component
    [SerializeField]
    string inputText = "Enter Text"; // This is the current text being input into the on-screen text display
    [SerializeField]
    float charDelay = 1f; // The delay between characters appearing on screen

    bool skipWait; // Whether we should skip the wait time for the next loop iteration (defaults to false)

    string displayText = ""; // The text currently being displayed (This is the "stepped" through output)
    const string expression = @"(?<=\[)[\w ]+(?!=\])"; // Our regular expression, does not change

    // This will step through the text, one character at a time
    IEnumerator StepText(){

        // Loop through each character in the input text
        for(int i = 0;i<inputText.Length;i+=1){
            // Check if a tag is being applied
            if(inputText[i] == '<'){
                skipWait = true; // Skip wait time if a tag is being applied
            }
            // Check if there was a previous character, and that character ended a tag
            // It is important that this is else if, because if a new tag starts it can break waiting
            else if(i == 0 || inputText[i - 1] == '>'){
                skipWait = false; // Stop skipping the wait time
            }
            displayText += inputText[i]; // Append the input text to the display text
            meshText.text = displayText; // Push the display text onto the TMP component

            // Check if we are NOT skipping the wait time
            if(!skipWait && inputText[i] != ' '){
                yield return new WaitForSeconds(charDelay); // Wait before displaying next character
            }
        }
    }

    // This will feed text into the object to be displayed on-screen
    public void Feed(string text){
        inputText = text; // Replace our old text with the provided text

        Regex rx = new Regex(expression); // Create a new regular expression

        MatchCollection matches =  rx.Matches(inputText); // Find matches for our expression

        // Iterate through each match using our input text
        for(int i = 0;i<matches.Count;i++){
            Match match = matches[i]; // Store the current match
            inputText = inputText.Replace($"[{match.Value}]", 
                $"<color=\"yellow\"><u><link=\"{i}\">{match.Value}</link></u></color>");
        }

        displayText = ""; // Reset the display text

        // Stop the step coroutine if it is currently running
        StopCoroutine("StepText");
        // Restart the stepping coroutine
        StartCoroutine("StepText");
    }

    // Reset the text immediately after re enabling
    void OnEnable(){
        meshText.text = ""; // Reset mesh text
    }
}
