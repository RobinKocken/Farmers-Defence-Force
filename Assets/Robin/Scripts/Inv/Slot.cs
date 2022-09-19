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
    public Image boxImage;

    public TMP_Text amountText;

    public int iD;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();

        boxImage = gameObject.GetComponent<Image>();
        iconRenderer = iconRenderer.GetComponent<Image>();

        iconRenderer.enabled = false;
    }

    void Update()
    {
        if(itemData != null)
        {
            iconRenderer.enabled = true;
            iconRenderer.sprite = itemData.icon;

            boxImage.color = manager.rarityColors[itemData.rarity];
        }
        else
        {
            iconRenderer.sprite = null;
            iconRenderer.enabled = false;

            boxImage.color = manager.defaultColor;
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
