using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Craft", order = 1)]
public class Item : ScriptableObject
{
    public int iD;
    public string itemName;
    public Sprite icon;
    public int rarity;

    public int maxStack;
}
