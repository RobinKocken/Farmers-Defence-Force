using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour
{
    public Item itemData;
    public TMP_Text amountText;

    Image iconRenderer;

    public int amount;

    void Update()
    {
        if(amount <= 1)
        {
            amountText.gameObject.SetActive(false);
        }
        else
        {
            amountText.gameObject.SetActive(true);
        }

        amountText.text = amount.ToString();
    }

    void OnValidate()
    {
        if(iconRenderer == null)
        {
            iconRenderer = gameObject.GetComponent<Image>();
        }

        iconRenderer.sprite = itemData.icon;
    }
}
