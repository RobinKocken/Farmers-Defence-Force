using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform cam;
    public Transform gun;
    public Transform player;
    public RaycastHit hit;
    public GameObject ufObject;
    public GameObject metalScrap;
    public PickUp pickup;
    public AlienAi script;

    public int ufo;
    public float cooldown = 2f;
    public float ammo = 5;
    public float recoilGun;

    public bool kanSchieten;
    public bool cooldownActive;
    public bool recoil;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
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
        if (recoil == true)
        {
            gun.transform.Rotate(1f*recoilGun, 0f, 0f);
            player.transform.position -= player.transform.forward*0.5f;
            recoil = false;
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
           
            if (hit.transform.gameObject.tag == "Alien")
            {
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                   
                    if (pickup.gunIsActief == true)
                    {
                        
                        if (kanSchieten == true)
                        {
                            ammo--;
                            cooldownActive = true;
                            recoil = true;
                            
                            if (pickup.damageBoostIsActief == true && ammo > 0)
                            {
                                script.health -= 20;
                                if (ufo <= 0)
                                {
                                    ufObject.SetActive(false);
                                    metalScrap.SetActive(true);
                                }

                            }
                            else if (ammo > 0)
                            {
                                script.health -= 10;
                                if (ufo <= 0)
                                {
                                    ufObject.SetActive(false);
                                    metalScrap.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (pickup.gunIsActief == true)
                {
                    if (kanSchieten == true)
                    {
                        ammo--;
                        cooldownActive = true;
                        recoil = true;
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
}
