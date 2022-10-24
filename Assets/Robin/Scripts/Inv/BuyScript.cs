using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    public InventoryManager inventory;

    public Item item;
    public int amount;
    public Item currency;
    public int price;

    int slotNumber;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
    }

    public void Buy()
    {
        slotNumber = inventory.CheckForItem(currency, slotNumber);

        if(inventory.slots[slotNumber].GetComponent<Slot>().itemData != null)
        {
            if(price <= inventory.slots[slotNumber].GetComponent<Slot>().itemData.maxStack - inventory.slots[slotNumber].GetComponent<Slot>().amount)
            {
                inventory.RemoveItem(currency, price);
                inventory.AddItem(item, amount);
            }
        }
    }
}
