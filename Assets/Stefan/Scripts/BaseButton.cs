using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    [SerializeField] protected float minInteractDst;

    [SerializeField] protected Vector2[] interactZones;

    protected CanvasScaler canvasScaler;

    private void OnValidate()
    {
        if(canvasScaler == null)
            canvasScaler = FindObjectOfType<CanvasScaler>();
    }

    //True whenever the mouse is hovering over the button
    public virtual bool Hovered
    {
        get
        {
            var mousePos = Input.mousePosition;
            for (int i = 0; i < interactZones.Length; i++)
            {
                var checkPos = transform.position + new Vector3(interactZones[i].x, interactZones[i].y);

                if (Vector3.Distance(checkPos, mousePos) < minInteractDst * canvasScaler.scaleFactor)
                {
                    return true;
                }
            }
            return false;
        }
    }

    //True whenever the player holds the LMB while hovering over the button
    public virtual bool Clicked
    {
        get
        {
            if (Hovered)
            {
                if (Input.GetMouseButton(0)) return true;
            }
            return false;
        }

    }

    //True whenever the player releases the LMB while hovering over the button
    public virtual bool Released
    {
        get
        {
            bool clicked = Clicked;

            if (clicked == false && previousClicked != clicked && Hovered) return true;
            return false;
        }
    }

    protected bool previousClicked;

    protected virtual void Update()
    {
        previousClicked = Clicked;
    }

    protected virtual void OnDrawGizmos()
    {
        for (int i = 0; i < interactZones.Length; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position + new Vector3(interactZones[i].x, interactZones[i].y), minInteractDst * canvasScaler.scaleFactor);
        }
    }
}
