using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class PickupItem : MonoBehaviour
{
    public InventoryManager manager;
    public Item item;
    public int amount;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
    }

    public void PickUp()
    {
        manager.AddItem(item, amount);
        Destroy(gameObject);
    }
}
