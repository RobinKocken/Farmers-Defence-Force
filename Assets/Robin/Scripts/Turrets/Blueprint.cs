using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Blueprint : MonoBehaviour
{
    public GameObject cam;
    public KeyCode buildKey;
    public LayerMask placeble;

    public GameObject prefab;

    public GameObject move;

    public float distance;
    RaycastHit hit;

    public float mouseWheel;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(buildKey))
        {
            move = Instantiate(prefab);

            move.transform.SetParent(this.gameObject.transform);
        }

        Raycast();
    }

    void Raycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if(Physics.Raycast(ray, out hit, distance, placeble))
        {
            if(move != null)
            {
                move.transform.position = hit.point;
                move.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                mouseWheel += Input.mouseScrollDelta.y;
                move.transform.Rotate(Vector3.up, mouseWheel * 10f);

                if(Input.GetButtonDown("Fire1"))
                {
                    move.transform.position = hit.point;

                    move.transform.parent = null;
                    mouseWheel = 0;
                    move = null;
                }
            }
        }
    }
}
