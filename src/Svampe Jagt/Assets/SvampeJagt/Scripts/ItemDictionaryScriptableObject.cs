using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDictionary", menuName = "Items/ItemDictionary", order = 1)]
public class ItemDictionaryScriptableObject : ScriptableObject
{
    public List<ItemScriptableObject> itemDictionary = new ();

    public Item GetItem(string name)
    {
        var item = itemDictionary.FirstOrDefault(x => x.Name == name);
        if (item == null)
        {
            Debug.LogError("Item not found in dictionary");
            return null;
        }

        return new Item{
            Name = item.Name,
            Prefab = item.Prefab,
            Quantity = item.InitialQuantity
        };
    }
}
