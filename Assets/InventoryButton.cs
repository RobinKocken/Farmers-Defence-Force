using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class InventoryButton : BaseButton
{
    public Image[] changeColorImages;
    public TextMeshProUGUI[] changeColorTexts;

    public float colorFadeDuration;
    float fadeTimer;

    public Color CurrentColor
    {
        get
        {
            return changeColorImages[0].color;
        }
        private set
        {
            for (int i = 0; i < changeColorImages.Length; i++)
            {
                changeColorImages[i].color = value;
            }
            for (int i = 0; i < changeColorTexts.Length; i++)
            {
                changeColorTexts[i].color = value;
            }
        }
    }

    public Button.ButtonClickedEvent OnClick;

    Color _targetColor;

    Color targetColor
    {
        get
        {
            return _targetColor;
        }
        set
        {
            if (!_targetColor.Equals(value))
            {
                _targetColor = value;
                ResetColorTimer();
            }
        }
    }

    public Color normalColor,highlightedColor,pressedColor,selectedColor;

    public static InventoryButton currentButtonSelected;

    protected override void Update()
    {
        if (Released) // button is clicked and will execute
        {
            OnClick.Invoke();
            currentButtonSelected = this;
            print("Clicked");
        }
        if(currentButtonSelected == this)
        {
            targetColor = selectedColor;
        }
        else if (Clicked)
        {
            targetColor = pressedColor;
        }
        else if (Hovered)
        {
            targetColor = highlightedColor;
        }
        else
        {
            targetColor = normalColor;
        }

        UpdateColors();

        base.Update();
    }

    void UpdateColors()
    {
        CurrentColor = Color.Lerp(CurrentColor,targetColor,Mathf.InverseLerp(0,colorFadeDuration,fadeTimer));

        fadeTimer += Time.deltaTime;
    }

    void ResetColorTimer()
    {
        print("Color has been changed!");
        fadeTimer = 0;
    }
}
