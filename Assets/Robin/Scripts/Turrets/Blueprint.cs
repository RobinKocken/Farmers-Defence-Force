using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public Camera cam;
    public KeyCode buildKey;

    public GameObject prefab;

    public float distance;
    RaycastHit hit;

    void Start()
    {

    }

    void Update()
    {
        Raycast();
    }

    void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, distance))
        {
            transform.position = hit.point;

            //if(Input.GetKeyDown(buildKey))
            //{
            //    Instantiate(prefab, transform.position, transform.rotation);
            //}
        }
    }
}
