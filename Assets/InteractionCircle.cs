using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionCircle : MonoBehaviour
{
    public bool active, canPickup;

    public Animator mainCircle, pulsingCircle, background,text,textBackground,key;
    public TextMeshProUGUI keyText, interactionText;
    public Color interactableColor;

    public float fadeSpeed = 0.3f;
    float fadeTimer;
    Color _targetColor;

    Color TargetColor
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
                fadeTimer = 0;
            }
        }
    }

    Image[] images;

    TextMeshProUGUI[] texts;
    private void Start()
    {
        images = GetComponentsInChildren<Image>();

        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.Lerp(images[i].color,TargetColor,Mathf.InverseLerp(0,fadeSpeed,fadeTimer));
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = Color.Lerp(texts[i].color, TargetColor, Mathf.InverseLerp(0, fadeSpeed, fadeTimer));
        }

        fadeTimer += Time.deltaTime;
    }
    public void SetState(bool active, bool canPickup, KeyCode key, string interactionMethod)
    {
        this.active = active;
        this.canPickup = canPickup;

        mainCircle.SetBool("Active", active);
        background.SetBool("Active", active);
        pulsingCircle.SetBool("Active", active && !canPickup);
        text.SetBool("Active", active && canPickup);
        textBackground.SetBool("Active", active && canPickup);
        this.key.SetBool("Active", active && canPickup);

        keyText.text = key.ToString();

        interactionText.text = "To " + interactionMethod;

        TargetColor = canPickup ? interactableColor : Color.white;
    }
}
