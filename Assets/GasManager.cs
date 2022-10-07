using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GasManager : MonoBehaviour
{
    public static float GasPercentage;

    public static int GasSliderValue
    {
        get 
        {
            return Mathf.RoundToInt(GasPercentage);
        }
    }

    public TextMeshProUGUI text;
    public Slider slider;

    [Range(0,100)]public float testGas = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GasPercentage = testGas;

        text.text = GasSliderValue.ToString() + "%";
        slider.value = GasSliderValue;
    }
}
