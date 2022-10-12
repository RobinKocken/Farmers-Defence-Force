using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDescription : MonoBehaviour
{
    public float minDst;
    public Transform[] slots;

    public float minHoverTime;
    float hoverTime;
    Transform previousHovered;

    public Vector3 offset;

    public GameObject description;
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
                EnableDescription(true);
            }
        }
        else
        {
            hoverTime = minHoverTime;
            EnableDescription(false);
        }
        
        if (description.activeSelf)
        {
            description.transform.position = Input.mousePosition + offset;
        }

        previousHovered = currentHovered;
    }

    void EnableDescription(bool value)
    {
        description.SetActive(value);
        if (value)
        {
            //Set the descriptions' value
        }
    }

    Transform GetHoveredButton()
    {
        var mousePos = Input.mousePosition;

        Transform hovered = null;

        for (int i = 0; i < slots.Length; i++)
        {
            var pos = new Vector3(slots[i].position.x, slots[i].position.y);
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
