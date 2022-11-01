using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public InventoryManager inventory;
    public MouseVis mouse;
    public KeyCode openShop;
    public GameObject shop;
    public GameObject res;
    public GameObject ammo;

    public bool bla;

    void Update()
    {
        if(Input.GetKeyDown(openShop))
        {
            bla = !bla;

            if(inventory.inventory.activeSelf == true)
            {
                inventory.bla = false;
            }
            else
            {
                mouse.visible = bla;
            }
        }

        shop.SetActive(bla);
        inventory.shopActive = bla;
    }

    public void Resources()
    {
        res.SetActive(true);
        ammo.SetActive(false);
    }

    public void Ammo()
    {
        ammo.SetActive(true);
        res.SetActive(false);
    }
}
