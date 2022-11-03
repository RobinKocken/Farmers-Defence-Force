using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DeleteButton : MonoBehaviour
{
    public InventoryManager inventory;
    public PickUpManager notificationManager;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
    }

    public void Delete()
    {
        if(inventory.itemHolder != null)
        {
            if(notificationManager != null)
            {
                notificationManager.AddNotification2(inventory.itemHolder.itemName, inventory.itemAmount, inventory.itemHolder.icon);
            }
            inventory.itemHolder = null;
            inventory.itemAmount = 0;
        }
    }
}
