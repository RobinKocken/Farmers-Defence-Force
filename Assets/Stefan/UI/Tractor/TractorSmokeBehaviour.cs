using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorSmokeBehaviour : MonoBehaviour
{
    public float xSpeed, ySpeed, rotSpeed;

    void Update()
    {
        transform.position += new Vector3(xSpeed, ySpeed) * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotSpeed) * Time.deltaTime);
    }
}
