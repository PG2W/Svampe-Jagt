using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Item
{
    public string Name;
    public int Quantity;
    public GameObject Prefab;


}

public class Inventory : MonoBehaviour
{

    public List<Item> Items = new(); //TODO. Implement localstorage or something

    public Action OnInventoryUpdated;
    public ItemDictionaryScriptableObject itemDictionaryScriptableObject;


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            foreach (var i in Items)
            {
                Debug.Log(i.Name + ": " + i.Quantity + " + " + i.Prefab);
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            foreach (var i in Items)
            {
                ++i.Quantity;
            }
        }
    }


    public void AddItem(Item item)
    {
        //Add if not already in array otherwise increment count
        var itemInList = Items.FirstOrDefault(x => x.Name == item.Name);
        if (itemInList != null)
        {
            itemInList.Quantity++;
        }
        else
        {
            Items.Add(item);
        }

        OnInventoryUpdated?.Invoke();
    }

    public void DecrementItemCount(string name)
    {
        //Decrement item count and if only 1 remove the item from the listd
        var itemInList = Items.FirstOrDefault(x => x.Name == name);
        if (itemInList != null)
        {
            if (itemInList.Quantity <= 1)
                Items.Remove(itemInList);
            else
                itemInList.Quantity--;

            OnInventoryUpdated?.Invoke();
        }

    }



}
