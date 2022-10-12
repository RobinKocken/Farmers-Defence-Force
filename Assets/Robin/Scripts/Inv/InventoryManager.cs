using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;

public class InventoryManager : MonoBehaviour
{
    public KeyCode inv;
    bool bla;

    public RaycastPlayer blueprint;

    [Header("Inventory")]
    public GameObject inventory;
    public GameObject backEffects;

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
    public List<Transform> slots;
    public List<Transform> hotbarSlots;

    [Header("Other")]
    public int currentSlot;
    public float mouseWheel;
    float oldMouse;
    public bool canScroll;

    [Header("Colour")]
    public Color defaultColour;
    public Color SelectedColour;

    [Header("Gas")]
    public Slider sliderGas;
    public float maxGas;
    public float currentGas;
    public float gasPerSecond;
    public float waitForSeconds;
    float startTime;
    public TMP_Text gasText;

    [Header("Pickup Notification")]
    public PickUpManager notificationManager;

    void Start()
    {
        InitializeInventory();
        SetSlotIDS();

        boxCursor = boxCursor.GetComponent<Image>();
        iconHolder = iconHolder.GetComponent<Image>();

        sliderGas.maxValue = maxGas;
        sliderGas.value = currentGas;

        canScroll = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(inv))
        {
            bla = !bla;

            inventory.SetActive(bla);
            backEffects.SetActive(bla);
        }

        if(inventory.activeSelf == true)
        {
            cursor.position = Input.mousePosition + offset;
        }

        if(itemHolder != null)
        {
            cursor.gameObject.SetActive(true);

            iconHolder.sprite = itemHolder.icon;
        }
        else
        {
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

        HotbarFunction();
        Gas();
    }

    void InitializeInventory()
    {
        //Sets Slots
        for(int i = 0; i < inventorySlotHolder.childCount; i++)
        {
            slots.Add(inventorySlotHolder.GetChild(i));
        }
        //Sets Hotbar Slots
        for(int i = 0; i < inventoryHotbarSlotHolder.childCount; i++)
        {
            slots.Add(inventoryHotbarSlotHolder.GetChild(i));
            hotbarSlots.Add(inventoryHotbarSlotHolder.GetChild(i));
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

    void Gas()
    {
        if(currentGas >= maxGas)
        {
            currentGas = maxGas;
        }

        gasText.text = sliderGas.value.ToString();
        sliderGas.value = currentGas;
    }

    public void AddGas()
    {
        if(Time.time - startTime > waitForSeconds)
        {
            currentGas += gasPerSecond;

            startTime = Time.time;
        }
    }

    void HotbarFunction()
    {
        if(!inventory.activeSelf)
        {
            if(canScroll)
            {
                mouseWheel += Input.mouseScrollDelta.y;
            }
            
            if(mouseWheel < 0)
            {
                mouseWheel = 0;
            }
            else if(mouseWheel > hotbarSlots.Count - 1)
            {
                mouseWheel = hotbarSlots.Count - 1;
            }

            if(oldMouse != mouseWheel)
            {
                hotbarSlots[(int)oldMouse].GetComponent<Image>().color = defaultColour;
                oldMouse = mouseWheel;
            }
            else if(oldMouse == mouseWheel)
            {
                hotbarSlots[(int)mouseWheel].GetComponent<Image>().color = SelectedColour;
            }

            if(hotbarSlots[(int)mouseWheel].GetComponent<Slot>().itemData != null)
            {
                if(hotbarSlots[(int)mouseWheel].GetComponent<Slot>().itemData.placeable == true && bla == false && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    blueprint.prefab = hotbarSlots[(int)mouseWheel].GetComponent<Slot>().itemData;
                    hotbarSlots[(int)mouseWheel].GetComponent<Slot>().amount -= 1;
                    //hotbarSlots[(int)mouseWheel].GetComponent<Slot>().itemData = null;
                }
            }
        }
        else if(inventory.activeSelf)
        {
            hotbarSlots[(int)mouseWheel].GetComponent<Image>().color = defaultColour;
        }

    }

    public void CraftItem(Requirements[] reqs, Item outcome, int outcomeAmount)
    {
        //Collecting Info what Items can be collected
        Item[] collectedItem = new Item[reqs.Length];
        int[] collectedID = new int[reqs.Length];
        
        for(int x =  0; x < reqs.Length; x++)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if(slots[i].GetComponent<Slot>().itemData != null)
                {
                    if(slots[i].GetComponent<Slot>().itemData.iD == reqs[x].id && slots[i].GetComponent<Slot>().amount >= reqs[x].amount)
                    {
                        collectedItem[x] = slots[i].GetComponent<Slot>().itemData;
                        collectedID[x] = slots[i].GetComponent<Slot>().iD;
                    }
                }
            }
        }

        for(int i = 0; i < collectedItem.Length; i++)
        {
            if(collectedItem[i] == null)
            {
                return;
            }
        }

        for(int i = 0; i < collectedID.Length; i++)
        {
            slots[collectedID[i]].GetComponent<Slot>().amount -= reqs[i].amount;
        }

        AddItem(outcome, outcomeAmount);
    }

    public void AddItem(Item item, int itemAmount)
    {
        if(notificationManager != null)
        {
            notificationManager.AddNotification(item.itemName, itemAmount, item.icon);
        }

        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].GetComponent<Slot>().itemData != null)
            {
                if(slots[i].GetComponent<Slot>().itemData.iD == item.iD)
                {
                    if(itemAmount <= slots[i].GetComponent<Slot>().itemData.maxStack - slots[i].GetComponent<Slot>().amount)
                    {
                        slots[i].GetComponent<Slot>().amount += itemAmount;
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].GetComponent<Slot>().itemData == null)
            {
                //AddItem
                slots[i].GetComponent<Slot>().itemData = item;
                slots[i].GetComponent<Slot>().amount = itemAmount;
                return;
            }
        }

        Debug.Log("All Slots are full");
    }

    public int CheckForItem(Item item, int slotNumber)
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].GetComponent<Slot>().itemData != null)
            {
                if(slots[i].GetComponent<Slot>().itemData.iD == item.iD)
                {
                    slotNumber = i;

                    return slotNumber;
                }
            }
        }

        return slotNumber = -1;
    }

    public void RemoveItem()
    {

    }

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
