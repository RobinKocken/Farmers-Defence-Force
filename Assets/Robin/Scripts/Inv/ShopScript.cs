using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    //public TempMove move;
    //public MouseVis mouse;
    //public TempMouse temp;
    //public GameObject shop;

    //bool bla;

    //public bool shopping;

    public InventoryManager inventory;
    public MouseVis mouse;
    public KeyCode openShop;
    public GameObject shop;

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
        Shopping();
    }

    public void Shopping()
    {
        
    }

    //public void ShoppingOn()
    //{
    //    mouse.visible = true;
    //    temp.mouseSens = 0;
    //    move.moving = false;

    //    bla = !bla;
    //    shop.SetActive(bla);

    //}
}
