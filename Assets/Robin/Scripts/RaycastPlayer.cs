using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayer : MonoBehaviour
{
    public KeyCode interact;
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
}
