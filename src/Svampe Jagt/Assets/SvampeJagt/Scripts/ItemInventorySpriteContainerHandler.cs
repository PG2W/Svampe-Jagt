using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemInventorySpriteContainerHandler : MonoBehaviour
{
    public Image spriteHolder;
    public TMP_Text quantityText; 

    public void SetSprite(Sprite sprite)
    {
        spriteHolder.sprite = sprite;
    }

    public void SetQuantity(int quantity)
    {
        quantityText.text = quantity.ToString();
    }

    public void InitializeInventoryItemContainer(Sprite sprite, int quantity)
    {
        SetSprite(sprite);
        SetQuantity(quantity);
    }
}
