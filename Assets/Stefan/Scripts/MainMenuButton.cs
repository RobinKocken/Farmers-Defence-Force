using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuButton : BaseButton
{
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

    //True whenever the mouse is hovering over the button
    public override bool Hovered
    {
        get
        {
            if (startingGame) return false;

            return base.Hovered;
        }
    }

    //True whenever the player holds the LMB while hovering over the button
    public override bool Clicked
    {
        get
        {
            if (startingGame) return false;

            return base.Clicked;
        }
    }

    //True whenever the player releases the LMB while hovering over the button
    public override bool Released
    {
        get
        {
            if (startingGame) return false;

            return base.Released;
        }
    }

    void Start()
    {
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


    protected override void Update()
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

        base.Update();
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
}
