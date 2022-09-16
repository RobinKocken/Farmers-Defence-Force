using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "Craft", order = 1)]
public class Craft : ScriptableObject
{
    public int[] iDS;
    public int[] iDSAmounts;
    public Requirements[] requirements;

    public GameObject outcome;
    public int outcomeAmount;
}

[System.Serializable]
public class Requirements
{
    public enum Ingredients
    {
        Wood,
        Stone,
        Flint,
        RandomStuff
    }

    public Ingredients ingredient;
    public int amount;
}
