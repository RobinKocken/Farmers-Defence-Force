using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    InventoryManager manager;

    public Item itemData;
    public int amount;

    public Image iconRenderer;

    public TMP_Text amountText;

    public int iD;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();

        iconRenderer = iconRenderer.GetComponent<Image>();

        iconRenderer.enabled = false;
    }

    void Update()
    {
        if(itemData != null)
        {
            iconRenderer.enabled = true;
            iconRenderer.sprite = itemData.icon;

            if(amount > itemData.maxStack)
            {
                amount = itemData.maxStack;
            }
        }
        else
        {
            iconRenderer.sprite = null;
            iconRenderer.enabled = false;
        }

        if(amount <= 0)
        {
            itemData = null;
        }

        if(amount <= 1 )
        {
            amountText.enabled = false;
        }
        else
        {
            amountText.enabled = true;
        }

        amountText.text = amount.ToString();
    }

    public void SetID()
    {
        manager.currentSlot = iD;
        manager.PickupDropInventory();
    }
}
