using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform cam;
    public RaycastHit hit;
    public int ufo;
    public GameObject Ufo;
    public PickUp pickup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10))
        {
            if (hit.transform.gameObject.tag == "UFO")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (pickup.gunIsActief == true)
                    {
                        ufo -= 5;
                        if (ufo == 0)
                        {
                            Ufo.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
