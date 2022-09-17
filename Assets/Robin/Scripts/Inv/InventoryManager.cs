using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    public KeyCode inv;
    bool bla;

    [Header("Inventory")]
    public GameObject inventory;

    public Transform inventorySlotHolder;
    public Transform inventoryHotbarSlotHolder;

    [Header("Cursor")]
    public Transform cursor;
    public Image boxCursor;
    public Vector3 offset;
    Item itemHolder;
    int itemAmount;
    public Image iconHolder;
    public TMP_Text itemAmountText;

    [Header("Lists")]
    //public List<bool> isFull;
    public List<Transform> slots;
    public List<Transform> hotbarSlots;

    [Header("Other")]
    public int currentSlot;

    public Color[] rarityColors;
    public Color defaultColor;

    void Start()
    {
        InitializeInventory();
        SetSlotIDS();
        //CheckSlots();

        boxCursor = boxCursor.GetComponent<Image>();
        iconHolder = iconHolder.GetComponent<Image>();
    }

    void Update()
    {
        if(Input.GetKeyDown(inv))
        {
            inventory.SetActive(bla = !bla);
        }

        if(inventory.activeSelf == true)
        {
            cursor.position = Input.mousePosition + offset;
        }

        if(itemHolder != null)
        {
            cursor.gameObject.SetActive(true);

            boxCursor.color = rarityColors[itemHolder.rarity];
            iconHolder.sprite = itemHolder.icon;
        }
        else
        {
            boxCursor.color = defaultColor;
            iconHolder.sprite = null;

            cursor.gameObject.SetActive(false);
        }

        if(itemAmount <= 1)
        {
            itemAmountText.enabled = false;
        }
        else
        {
            itemAmountText.enabled = true;
        }

        itemAmountText.text = itemAmount.ToString();
    }

    void InitializeInventory()
    {
        //Sets Slots
        for(int i = 0; i < inventorySlotHolder.childCount; i++)
        {
            slots.Add(inventorySlotHolder.GetChild(i));
            //isFull.Add(false);
        }
        //Sets Hotbar Slots
        for(int i = 0; i < inventoryHotbarSlotHolder.childCount; i++)
        {
            slots.Add(inventoryHotbarSlotHolder.GetChild(i));
            hotbarSlots.Add(inventoryHotbarSlotHolder.GetChild(i));
            //isFull.Add(false);
        }
    }

    void SetSlotIDS()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].GetComponent<Slot>() != null)
            {
                slots[i].GetComponent<Slot>().iD = i;
            }
        }
    }

    //public void CheckSlots()
    //{
    //    //Check if Slots are Full
    //    for(int i = 0; i < slots.Count; i++)
    //    {
    //        if(slots[i].childCount > 0)
    //        {
    //            isFull[i] = true;
    //        }
    //        else
    //        {
    //            isFull[i] = false;
    //        }
    //    }
    //}

    //public void CraftItem(int[] iDS, int[] iDSAmount, GameObject outcome, int outcomeAmount)
    //{
    //    //Collecting Info weather or not Item can be Crafted
    //    bool[] collected = new bool[iDS.Length];
    //    Transform[] collectedSlots = new Transform[iDS.Length];

    //    CheckSlots();
    //    for(int x = 0; x < iDS.Length; x++)
    //    {
    //        for(int i = 0; i < slots.Count; i++)
    //        {

    //            if(isFull[i] == true)
    //            {
    //                if(slots[i].GetChild(0).GetComponent<InventoryItem>().itemData.iD == iDS[x] && slots[i].GetChild(0).GetComponent<InventoryItem>().amount >= iDSAmount[x])
    //                {
    //                    collected[x] = true;
    //                    collectedSlots[x] = slots[i].GetChild(0);
    //                }
    //            }
    //        }
    //    }

    //    for(int i = 0; i < collected.Length; i++)
    //    {
    //        if(collected[i] == false)
    //        {
    //            return;
    //        }
    //    }

    //    for(int i = 0; i < collectedSlots.Length; i++)
    //    {
    //        collectedSlots[i].GetComponent<InventoryItem>().amount -= iDSAmount[i];
    //        CheckSlots();
    //    }

    //    for(int i = 0; i < outcomeAmount; i++)
    //    {
    //        AddItem(outcome);
    //    }
    //}

    //public void AddItem(GameObject item)
    //{
    //    for(int i  = 0; i < slots.Count; i++)
    //    {
    //        if(isFull[i] == true)
    //        {
    //            if(isFull[i] == true && slots[i].GetChild(0).GetComponent<InventoryItem>().itemData.iD == item.GetComponent<InventoryItem>().itemData.iD)
    //            {
    //                if(item.GetComponent<InventoryItem>().amount <= slots[i].GetChild(0).GetComponent<InventoryItem>().itemData.maxStack - slots[i].GetChild(0).GetComponent<InventoryItem>().amount)
    //                {
    //                    slots[i].GetChild(0).GetComponent<InventoryItem>().amount += item.GetComponent<InventoryItem>().amount;
    //                    CheckSlots();
    //                    return;
    //                }
    //                return;
    //            }
    //        }
    //    }

    //    for(int x = 0; x < slots.Count; x++)
    //    {
    //        if(isFull[x] == false)
    //        {
    //            //Add Item
    //            Instantiate(item, slots[x]);
    //            CheckSlots();
    //            return;
    //        }
    //        else
    //        {
    //            Debug.Log("Slot is Full");
    //        }
    //    }

    //    Debug.Log("All Slots are Full");
    //}

    public void PickupDropInventory()
    {
        if(slots[currentSlot].GetComponent<Slot>().itemData != null && itemHolder == null)
        {
            //Put inside Cursor
            itemHolder = slots[currentSlot].GetComponent<Slot>().itemData;
            itemAmount = slots[currentSlot].GetComponent<Slot>().amount;

            slots[currentSlot].GetComponent<Slot>().amount = 0;
            slots[currentSlot].GetComponent<Slot>().itemData = null;
        }
        else if(slots[currentSlot].GetComponent<Slot>().itemData == null && itemHolder != null)
        {
            //Put inside Slot
            slots[currentSlot].GetComponent<Slot>().itemData = itemHolder;
            slots[currentSlot].GetComponent<Slot>().amount = itemAmount;

            itemAmount = 0;
            itemHolder = null;
        }
        else if(slots[currentSlot].GetComponent<Slot>().itemData != null && itemHolder != null)
        {
            //Stack Item
            if(slots[currentSlot].GetComponent<Slot>().itemData.iD == itemHolder.iD)
            {
                if(itemAmount <= slots[currentSlot].GetComponent<Slot>().itemData.maxStack - slots[currentSlot].GetComponent<Slot>().amount)
                {
                    slots[currentSlot].GetComponent<Slot>().amount += itemAmount;

                    itemAmount = 0;
                    itemHolder = null;
                }
            }
        }
    }
}
