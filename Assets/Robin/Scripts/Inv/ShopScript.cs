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
    public GameObject shop;

    public void Shopping()
    {
        shop.SetActive(true);
    }

    //public void ShoppingOn()
    //{
    //    mouse.visible = true;
    //    temp.mouseSens = 0;
    //    move.moving = false;

    //    bla = !bla;
    //    shop.SetActive(bla);

    //}

    //public void ShoppingOff()
    //{
    //    mouse.visible = false;
    //    temp.mouseSens = mouse.sensDefault;
    //    move.moving = true;

    //    bla = !bla;
    //    shop.SetActive(bla);
    //}
}
