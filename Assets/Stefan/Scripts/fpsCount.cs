using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fpsCount : MonoBehaviour
{
    TextMeshProUGUI text; 
    public float deltaTime;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        deltaTime += (Options.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        text.text = Mathf.Ceil(fps).ToString();
    }
}
