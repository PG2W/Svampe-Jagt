using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGUIController : MonoBehaviour
{
    public Transform itemsContainer;

    public GameObject inventoryItemContainerPrefab;

    public Inventory inventory;

    public GameObject inventoryUIGameObject;

    public PlayerMovement playerMovement;

    public bool IsInventoryOpen => inventoryUIGameObject.activeSelf;



    private void Start(){
        inventory.OnInventoryUpdated += UpdateInventoryGUI;

        ToggleInventoryGUI(false);
    }

    public void ToggleInventoryGUI(bool? force = null){
        if(force != null){
            inventoryUIGameObject.SetActive((bool)force);
            playerMovement.enabled = !(bool)force;
        }
        else{
            inventoryUIGameObject.SetActive(!inventoryUIGameObject.activeSelf);
            playerMovement.enabled = !inventoryUIGameObject.activeSelf;
        }
    }

    private void UpdateInventoryGUI(){
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in inventory.Items)
        {
            var itemContainer = Instantiate(inventoryItemContainerPrefab, itemsContainer);
            var itemContainerHandler = itemContainer.GetComponent<ItemInventorySpriteContainerHandler>();
            itemContainerHandler.InitializeInventoryItemContainer(inventory.itemDictionaryScriptableObject.GetItemSprite(item.Name), item.Quantity);
        }
    }



}
