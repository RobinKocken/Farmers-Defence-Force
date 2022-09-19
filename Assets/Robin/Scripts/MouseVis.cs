using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVis : MonoBehaviour
{
    public KeyCode mouseKey;
    public bool visible;
    public TempMouse cam;
    public float sensDefault;

    // Start is called before the first frame update
    void Start()
    {
        sensDefault = cam.mouseSens;
        MouseMode(visible);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(mouseKey))
        {
            MouseMode(visible = !visible);
        }

        //MouseMode(visible);
    }

    public void MouseMode(bool mouseSwitch)
    {
        if(!mouseSwitch)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            cam.mouseSens = sensDefault;
        }
        else if(mouseSwitch)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            cam.mouseSens = 0;
        }
    }
}
