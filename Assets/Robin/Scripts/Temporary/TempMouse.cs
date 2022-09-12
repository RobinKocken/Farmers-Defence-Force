using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TempMouse : MonoBehaviour
{
    public Transform camPos;
    public Transform orientation;
    public float mouseSens;

    float mouseX;
    float mouseY;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Camera();
        transform.position = camPos.position;
    }

    void Camera()
    {

        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation += -mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.localRotation = Quaternion.Euler(0, yRotation, 0);
        
    }
}
