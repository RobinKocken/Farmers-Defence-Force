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

    public Item prefab;

    public GameObject move;

    public float distance;
    RaycastHit hit;

    public float mouseWheel;

    int numberClicked;

    void Start()
    {

    }

    void Update()
    {
        Raycast();

        if(prefab != null && Input.GetButtonDown("Fire1"))
        {
            move = Instantiate(prefab.prefab);
            prefab = null;

            move.transform.SetParent(this.gameObject.transform);
        }
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
                    move.GetComponent<SingleShotScript>().enabled = true;

                    move.transform.parent = null;
                    mouseWheel = 0;
                    move = null;
                }
            }
        }
    }
}
