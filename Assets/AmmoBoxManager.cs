using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBoxManager : MonoBehaviour
{
    public int _ammoBoxes = 3;
    public int ammoBoxes
    {
        get
        {
            return _ammoBoxes;
        }
        set
        {
            _ammoBoxes = value;
            UpdateDisplay();
        }
    }

    public TextMeshProUGUI amountText;

    public Image boxImage;

    Color targetColor;

    public float lerpTime;
    float lerpTimer;
    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        boxImage.color = Color.Lerp(boxImage.color,targetColor,Mathf.InverseLerp(lerpTime,0,lerpTimer));
        lerpTimer += Time.deltaTime;
    }

    void UpdateDisplay()
    {
        amountText.text = ammoBoxes.ToString();
        lerpTimer = lerpTime;

        targetColor = ammoBoxes > 0 ? Color.white : new Color(0.8f, 0.8f, 0.8f, 1);
    }
}
