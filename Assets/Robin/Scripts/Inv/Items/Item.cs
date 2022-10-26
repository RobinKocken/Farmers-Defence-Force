using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public int iD;
    public string itemName;
    public Sprite icon;
    public int maxStack;
    public int price;

    public bool placeable;
    public GameObject prefab;
}
