using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuButton : MonoBehaviour
{
    public float minInteractDst;

    public Vector2[] interactZones;

    private Vector3 defaultSize;
    private Color defaultColor;

    private Vector3 sizeVelocity;
    private Vector3 targetSize;
    private Color targetColor;

    public float colorChangeRate, sizeSmoothTime;
    private float currentColorTimer;
    public TextMeshProUGUI buttonText;
    public Image image;
    [Header("Hovered")]
    public Vector3 hoveredSize;
    public Color hoveredColor;

    [Header("Clicked")]
    public Color clickedColor;

    public delegate void OnButtonClicked();

    public OnButtonClicked onButtonClicked;
    
    private bool previousHovered;

    public static bool startingGame = false;

    CanvasScaler canvasScaler;

    //True whenever the mouse is hovering over the button
    public bool Hovered
    {
        get
        {
            if (startingGame) return false;

            var mousePos = Input.mousePosition;
            for (int i = 0; i < interactZones.Length; i++)
            {
                var checkPos = transform.position + new Vector3(interactZones[i].x, interactZones[i].y);

                if (Vector3.Distance(checkPos, mousePos) < minInteractDst * canvasScaler.scaleFactor )
                {
                    return true;
                }
            }
            return false;
        }
    }

    //True whenever the player holds the LMB while hovering over the button
    public bool Clicked
    {
        get
        {
            if (startingGame) return false;

            if (Hovered)
            {
                if (Input.GetMouseButton(0)) return true;
            }
            return false;
        }

    }

    //True whenever the player releases the LMB while hovering over the button
    public bool Released
    {
        get
        {
            if (startingGame) return false;

            bool clicked = Clicked;

            if (clicked == false && previousClicked != clicked && Hovered) return true;
            return false;
        }
    }

    bool previousClicked;

    void Start()
    {
        canvasScaler = FindObjectOfType<CanvasScaler>();

        if(buttonText != null) 
        {
            defaultColor = buttonText.color;
        }
        if(image != null)
        {
            defaultColor = image.color;
        }

        defaultSize = transform.localScale;
    }


    void Update()
    {
        //Check if button is clicked on

        if (Released) OnClick();

        if (Clicked)
        {
            targetColor = clickedColor;
            targetSize = hoveredSize;
        }
        //Check if button is hovered
        else if (Hovered)
        {
            Debug.Log("Hover");
            ButtonHovered();
        }
        else
        {
            previousHovered = false;

            targetColor = defaultColor;
            targetSize = defaultSize;
        }

        previousClicked = Clicked;

        //Update button color and size

        if(buttonText)
            buttonText.color = Color.Lerp(buttonText.color, targetColor, currentColorTimer);
        if(image)
            image.color = Color.Lerp(image.color, targetColor, currentColorTimer);
        currentColorTimer += Options.deltaTime * colorChangeRate;

        transform.localScale = Vector3.SmoothDamp(transform.localScale,targetSize, ref sizeVelocity, sizeSmoothTime,Mathf.Infinity, Options.deltaTime);
    }

    public virtual void OnClick()
    {
        onButtonClicked?.Invoke();
    }

    void ButtonHovered()
    {
        if(previousHovered == false)
        {
            OnHover();
            previousHovered = true;
        }

        //button gets bigger and changes color
        targetSize = hoveredSize;
        targetColor = hoveredColor;
    }

    /// <summary>
    /// Called as soon as the mouse started hovering over the button
    /// </summary>
    void OnHover() => currentColorTimer = 0;

    private void OnDrawGizmos()
    {
        if (canvasScaler == null)
            canvasScaler = FindObjectOfType<CanvasScaler>();
        for (int i = 0; i < interactZones.Length; i++)
        {
            Gizmos.DrawWireSphere(transform.position + new Vector3(interactZones[i].x,interactZones[i].y) ,minInteractDst * canvasScaler.scaleFactor);
        }
    }
}
