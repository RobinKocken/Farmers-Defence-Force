using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDescription : MonoBehaviour
{
    public InventoryManager inventory;

    public float minDst;
    public CraftingButton[] slots;

    public float minHoverTime;
    float hoverTime;
    CraftingButton previousHovered;

    public Vector3 offset;

    public GameObject description;

    [Header("UI")]
    public TMP_Text canName;
    public TMP_Text[] itemsNeeded;
    public Image[] icons;
    public Color[] color;

    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        var currentHovered = GetHoveredButton();

        if (currentHovered == previousHovered && currentHovered != null)
        {
            hoverTime -= Time.deltaTime;
            if(hoverTime <= 0)
            {
                EnableDescription(true,currentHovered);
            }
        }
        else
        {
            hoverTime = minHoverTime;
            EnableDescription(false, null);
        }
        
        if (description.activeSelf)
        {
            description.transform.position = Input.mousePosition + offset;
        }

        previousHovered = currentHovered;
    }

    void EnableDescription(bool value, CraftingButton hovered)
    {
        description.SetActive(value);
        if (value)
        {
            for(int i = 0; i < itemsNeeded.Length; i++)
            {
                int num = -1;
                int amount;
                num = inventory.CheckForItem(hovered.recipe.requirements[i].item, num);

                if(num != -1)
                {
                    if(inventory.slots[num].GetComponent<Slot>().amount >= hovered.recipe.requirements[i].amount)
                    {
                        itemsNeeded[i].color = color[0];
                        icons[i].color = color[0];

                        amount = hovered.recipe.requirements[i].amount;
                    }
                    else
                    {
                        itemsNeeded[i].color = color[1];
                        icons[i].color = color[1];

                        amount = inventory.slots[num].GetComponent<Slot>().amount;
                    }
                }
                else
                {
                    itemsNeeded[i].color = color[1];
                    icons[i].color = color[1];

                    amount = 0;
                }

                canName.text = hovered.recipe.outcome.name;
                itemsNeeded[i].text = hovered.recipe.requirements[i].item.name.ToString() + " " + amount + "/ " + hovered.recipe.requirements[i].amount.ToString();
                icons[i].sprite = hovered.recipe.requirements[i].item.icon;
            }
        }
    }

    CraftingButton GetHoveredButton()
    {
        var mousePos = Input.mousePosition;

        CraftingButton hovered = null;

        for (int i = 0; i < slots.Length; i++)
        {
            var pos = new Vector3(slots[i].transform.position.x, slots[i].transform.position.y);
            float dst = Vector3.Distance(pos, mousePos);

            if(dst < minDst)
            {
                hovered = slots[i];
                break;
            }
            
        }

        return hovered;
    }
}
