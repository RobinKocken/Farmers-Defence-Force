using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingButton : MonoBehaviour
{
    public Craft recipe;
    public InventoryManager manager;

    public Image icon;
    public TMP_Text craftName;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
        icon.sprite = recipe.outcome.icon;
        craftName.text = recipe.outcome.name;
    }

    public void CraftItem()
    {
        manager.CraftItem(recipe.requirements, recipe.outcome, recipe.outcomeAmount);
    }
}
