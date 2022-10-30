using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public string Name;
    public GameObject Prefab;
    public int InitialQuantity;
    public Sprite Sprite;
}
