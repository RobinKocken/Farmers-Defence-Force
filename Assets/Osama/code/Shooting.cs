using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public Transform cam;
    public Transform gun;
    public Transform player;
    public RaycastHit hit;
    public PickUp pickup;

    public int ufo;
    public float cooldown = 2f;
    public float curretnAmmo = 24;
    public int maxAmmo = 24;
    public float recoilGun;

    public bool kanSchieten;
    public bool cooldownActive;
    public bool recoil;

    public TMP_Text ammoText;

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

        if (curretnAmmo < 0 || curretnAmmo == 0)
        {
            curretnAmmo = 0f;
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
                            curretnAmmo--;
                            cooldownActive = true;
                            recoil = true;
                            
                            if (pickup.damageBoostIsActief == true && curretnAmmo > 0)
                            {

                                hit.transform.GetComponent<AlienAi>().TakeDamage(20);

                            }
                            else if (curretnAmmo > 0)
                            {
                                hit.transform.GetComponent<AlienAi>().TakeDamage(10);
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
                        curretnAmmo--;
                        cooldownActive = true;
                        recoil = true;
                    }
                }
            }
        }

        ammoText.text = curretnAmmo + "/" + maxAmmo;
    }

    public void Reload()
    {
        kanSchieten = true;
        curretnAmmo = 5f;
    }
}
