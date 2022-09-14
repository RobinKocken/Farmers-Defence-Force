using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVis : MonoBehaviour
{
    public KeyCode mouseKey;
    public bool visible;
    public TempMouse camera;
    public float sensDefault;

    // Start is called before the first frame update
    void Start()
    {
        sensDefault = camera.mouseSens;
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

            camera.mouseSens = sensDefault;
        }
        else if(mouseSwitch)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            camera.mouseSens = 0;
        }
    }
}
