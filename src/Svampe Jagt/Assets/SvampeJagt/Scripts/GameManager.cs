using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InventoryGUIController inventoryGUI;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            inventoryGUI.ToggleInventoryGUI();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && inventoryGUI.IsInventoryOpen){
            inventoryGUI.ToggleInventoryGUI(false);
        }
    }
}
