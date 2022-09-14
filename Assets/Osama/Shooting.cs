using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform cam;
    public RaycastHit hit;
    public GameObject ufObject;
    public GameObject metalScrap;
    public PickUp pickup;
    public int ufo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 20))
        {
            if (hit.transform.gameObject.tag == "Ufo")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (pickup.gunIsActief == true)
                    {
                        if (pickup.damageBoostIsActief == true)
                        {
                            ufo -= 10;
                            if (ufo == 0)
                            {
                                ufObject.SetActive(false);
                                metalScrap.SetActive(true);
                            }
                        
                        }
                        else
                        {
                            ufo -= 5;
                            if (ufo == 0)
                            {
                                ufObject.SetActive(false);
                                metalScrap.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }
}
