using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public InventoryManager manager;
    public GameObject item;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InventoryManager>();
    }

    public void PickUp()
    {
        Destroy(gameObject);
        manager.AddItem(item);
    }
}
