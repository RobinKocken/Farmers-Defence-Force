using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GasManager : MonoBehaviour
{
    public static float gasPercentage;

    public static int GasSliderValue
    {
        get 
        {
            return Mathf.RoundToInt(gasPercentage);
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
        gasPercentage = testGas;

        text.text = GasSliderValue.ToString() + "%";
        slider.value = GasSliderValue;
    }
}
