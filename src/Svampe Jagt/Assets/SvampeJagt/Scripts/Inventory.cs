using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Item
{
    public int Id;
    public string Name;

    public int Quantity;

    public Item(string name)
    {
        Name = name;
    }
}

public class Inventory : MonoBehaviour
{

    public List<Item> Items = new(); //TODO. Implement localstorage or something

    void Start()
    {


    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            foreach (var i in Items)
            {
                Debug.Log(i.Name + ": " + i.Quantity);
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
        var itemInList = Items.FirstOrDefault(x => x.Id == item.Id);
        if (itemInList != null)
        {
            itemInList.Quantity++;
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
            if (itemInList.Quantity <= 1)
                Items.Remove(itemInList);
            else
                itemInList.Quantity--;

        }

    }



}
