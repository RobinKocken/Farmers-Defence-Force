using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayer : MonoBehaviour
{
    [Header("PickUp")]
    public KeyCode interact;
    public float distance;
    RaycastHit hit;

    [Header("Blueprint")]
    public GameObject cam;
    public KeyCode buildKey;
    public LayerMask placeble;

    public Item prefab;

    public GameObject move;

    public float distanceBuild;
    RaycastHit hitBuild;

    public float mouseWheel;

    void Update()
    {
        PickUpRay();
        Blueprint();
    }

    void ReloadTurrets()
    {

    }

    void PickUpRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        if(Physics.Raycast(ray, out hit, distance))
        {
            if(Input.GetKeyDown(interact))
            {
                if(hit.transform.CompareTag("Item") && hit.transform.GetComponent<PickupItem>())
                {
                    hit.transform.GetComponent<PickupItem>().PickUp();
                }
            }
        }
    }

    void Blueprint()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if(Physics.Raycast(ray, out hitBuild, distanceBuild, placeble))
        {
            if(move != null)
            {
                move.transform.position = hitBuild.point;
                move.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitBuild.normal);

                mouseWheel += Input.mouseScrollDelta.y;
                move.transform.Rotate(Vector3.up, mouseWheel * 10f);

                if(Input.GetButtonDown("Fire1"))
                {
                    move.transform.position = hitBuild.point;
                    move.GetComponent<SingleShotScript>().enabled = true;

                    move.transform.parent = null;
                    mouseWheel = 0;
                    move = null;
                }
            }
        }

        if(prefab != null)
        {
            move = Instantiate(prefab.prefab);
            prefab = null;

            move.transform.SetParent(this.gameObject.transform);
        }
    }
}
