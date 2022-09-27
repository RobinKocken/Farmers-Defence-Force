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
    public float cooldown = 2f;
    public float ammo = 5;
    public bool kanSchieten;
    public bool cooldownActive;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (pickup.gunIsActief == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && kanSchieten == true)
            {
               
            }
            else if (cooldownActive == true)
            {
                
            }
        }
        if (cooldownActive == true)
        {
            cooldown -= 1f * Time.deltaTime;
            kanSchieten = false;
            if (cooldown < 0 || cooldown == 0)
            {
                kanSchieten = true;
                cooldownActive = false;
                cooldown = 2f;
            }
        }
        

        if (ammo < 0 || ammo == 0)
        {
            ammo = 0f;
            kanSchieten = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Physics.Raycast(cam.position, cam.forward, out hit, 20))
        {
           
            if (hit.transform.gameObject.tag == "Ufo")
            {
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                   
                    if (pickup.gunIsActief == true)
                    {
                        
                        if (kanSchieten == true)
                        {
                            ammo--;
                            cooldownActive = true;
                            
                            if (pickup.damageBoostIsActief == true && ammo > 0)
                            {
                                ufo -= 20;
                                if (ufo == 0)
                                {
                                    ufObject.SetActive(false);
                                    metalScrap.SetActive(true);
                                }

                            }
                            else if (ammo > 0)
                            {
                                ufo -= 10;
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

    public void Reload()
    {
        kanSchieten = true;
        ammo = 5f;
    }
    public void Cooldown()
    {
        cooldownActive = true;
        cooldown -= 1f * Time.deltaTime;
        if(cooldown<0 || cooldown == 0)
        {
            kanSchieten = true;
            cooldownActive = false;
            cooldown = 2f;
        }
    }
}
