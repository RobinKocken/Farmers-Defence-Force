using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVis : MonoBehaviour
{
    public bool visible;
    public float sensDefaultX;
    public float sensDefaultY;

    void Start()
    {
        sensDefaultX = Options.xSens;
        sensDefaultY = Options.ySens;
        visible = false;
        MouseMode(visible);
    }

    void Update()
    {
        MouseMode(visible);
    }

    public void MouseMode(bool mouseSwitch)
    {
        if(!mouseSwitch)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Options.xSens = sensDefaultX;
            Options.ySens = sensDefaultY;
        }
        else if(mouseSwitch)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            Options.xSens = 0;
            Options.ySens = 0;
        }
    }
}
