using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "Craft", order = 1)]
public class Craft : ScriptableObject
{
    public Requirements[] requirements;

    public Item outcome;
    public int outcomeAmount;
}

[System.Serializable]
public class Requirements
{
    public int id;
    public int amount;
}
