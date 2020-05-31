using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// James Quinney - QUI16000158
// This will push inventory data onto
// UI elements for the inventory menu
public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryObject; // The in-scene inventory menu object
    [SerializeField]
    GameObject firstButton; // The first button in the list (placed in the menu itself)
    [SerializeField]
    RectTransform buttonPanel; // The panel the buttons are on

    List<GameObject> currentButtons = new List<GameObject>(); // Each of the buttons currently in the menu

    // This will push the player's inventory items onto the UI
    void GenerateInfo(){
        // Destroy any buttons previously in the menu
        foreach(GameObject button in currentButtons){
            Destroy(button);
        }

        currentButtons = new List<GameObject>(); // Reset the list of buttons

        // Ensure that there is at least one button
        if(Inventory.items.Count > 0 ){
            firstButton.SetActive(true); // Show the first button if it is needed

            // This will set up a button for use with the inventory system
            System.Action<GameObject, Item> SetupButton = (button, item) => {
                button.name = item.info.name; // Set the name of the button
                button.GetComponentInChildren<TMP_Text>().text = item.info.name; // Set the button's text to its name

                // Grab the drop button
                Button dropButton = button.transform.Find("Drop").GetComponent<Button>();
                // Grab the use button
                Button useButton = button.transform.Find("Use").GetComponent<Button>();

                dropButton.onClick.RemoveAllListeners(); // Clear all previous listeners from the object
                useButton.onClick.RemoveAllListeners(); // Clear all previous listeners from the object

                // Check if the item can be dropped
                if(item.info.droppable){
                    // Set the drop button to drop the item at the current index
                    dropButton.onClick.AddListener(
                        () => {
                            dropButton.onClick.RemoveAllListeners(); // This will stop the player from dropping multiple of the same item
                            Inventory.instance.DropItem(item);
                            ToggleMenu();
                        }
                    );
                }

                useButton.onClick.AddListener(
                    () => {
                        useButton.onClick.RemoveAllListeners(); // Stops player from using same item multiple times
                        item.info.onUse.Invoke(); // Invoke the on use event for the item
                        ToggleMenu();
                    }
                );
            };

            // Setup the first button
            SetupButton(firstButton, Inventory.items[0]);

            // Store the first button's rect transform
            RectTransform ft = firstButton.GetComponent<RectTransform>();
            // Store the first button's height
            float buttonHeight = ft.sizeDelta.y;

            // Reset the size of the button panel
            buttonPanel.sizeDelta = new Vector2(buttonPanel.sizeDelta.x, buttonHeight);

            // Loop through all but the first inventory item
            for(int i = 1;i<Inventory.items.Count;i+=1){
                // Create a new button, and attach it to the button panel
                GameObject newButton = Instantiate(firstButton, buttonPanel);
                // Store the new button's transform
                RectTransform bt = newButton.GetComponent<RectTransform>();
                // Offset the current button by the first button using the increment variable
                bt.anchoredPosition = ft.anchoredPosition - new Vector2(0f, buttonHeight * i);

                buttonPanel.sizeDelta += new Vector2(0f, buttonHeight); // Expand the button panel

                // Setup the new button
                SetupButton(newButton, Inventory.items[i]);

                // Add the new button the the list of buttons
                currentButtons.Add(newButton);
            }
        }
        else{
            firstButton.SetActive(false); // Hide the first button if it is not needed
        }
    }

    // This will toggle the menu's active status
    public void ToggleMenu(){
        inventoryObject.SetActive(!inventoryObject.activeSelf);

        // Check if the menu was opened
        if(inventoryObject.activeSelf){
            CursorManager.instance.Add("Inventory"); // Unlock the cursor

            GenerateInfo(); // Generate UI
        }
        else{
            CursorManager.instance.Remove("Inventory"); // Attempt to lock the cursor
        }
    }
}
