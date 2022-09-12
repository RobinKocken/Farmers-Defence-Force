using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVis : MonoBehaviour
{
    public bool visible;

    // Start is called before the first frame update
    void Start()
    {
        MouseMode(visible);
    }

    // Update is called once per frame
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
        }
        else if(mouseSwitch)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
