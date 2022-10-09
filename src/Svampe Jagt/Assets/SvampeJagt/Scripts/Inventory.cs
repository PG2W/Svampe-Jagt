using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Item
{
    public int Id;
    public string Name;

    public int quantity;

    public Item(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class Inventory : MonoBehaviour
{

    List<Item> Items = new(); //TODO. Implement localstorage or something


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            print("dick");
        }
    }


    public void AddItem(Item item)
    {
        //Add if not already in array otherwise increment count
        var itemInList = Items.FirstOrDefault(x => x.Id == item.Id);
        if (itemInList != null)
        {
            itemInList.quantity++;
            return;
        }

        Items.Add(item);
    }

    public void DecrementItemCount(int itemId)
    {
        //Decrement item count and if only 1 remove the item from the listd
        var itemInList = Items.FirstOrDefault(x => x.Id == itemId);
        if (itemInList != null)
        {
            if (itemInList.quantity <= 1)
                Items.Remove(itemInList);
            else
                itemInList.quantity--;

        }

    }



}
