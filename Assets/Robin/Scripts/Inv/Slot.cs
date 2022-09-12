using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int iD;
    public InventoryManager manager;

    public Image boxImage;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
        boxImage = GetComponent<Image>();
    }

    void Update()
    {
        if(transform.childCount > 0)
        {
            boxImage.color = manager.rarityColors[transform.GetChild(0).GetComponent<InventoryItem>().itemData.rarity];
        }
        else
        {
            boxImage.color = manager.defaultColor;
        }
    }

    public void SetID()
    {
        manager.currentSlot = iD;
        manager.PickupDropInventory();
    }
}
