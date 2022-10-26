using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalScrollBarScript : MonoBehaviour
{
    [Range(0,1)]
    public float multiplier = 0.05f;

    Scrollbar scrollbar;
    public VerticalLayoutGroup layout;
    public float layoutTopMin, layoutTopMax;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        scrollbar.value -= Input.mouseScrollDelta.y * multiplier;
        scrollbar.value = Mathf.Clamp01(scrollbar.value);

        layout.padding.top = (int)Mathf.Lerp(layoutTopMin,layoutTopMax, scrollbar.value);
        layout.SetLayoutVertical();
    }
}
