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
    private float cooldown = 2f;
    public float ammo = 5;
    public bool kanSchieten;
    private bool cooldownShooting;
    // Start is called before the first frame update
    void Start()
    {
        cooldownShooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickup.gunIsActief == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && kanSchieten == true)
            {
                Shoot();
            }
        }
       
        if (ammo < 0 || ammo == 0)
        {
            ammo = 0f;
            kanSchieten = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && ammo == 0)
        {
            Reload();
        }
    }

    void Shoot()
    {
        kanSchieten = false;

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
                            if (pickup.damageBoostIsActief == true && ammo > 0)
                            {
                                ufo -= 10;
                                if (ufo == 0)
                                {
                                    ufObject.SetActive(false);
                                    metalScrap.SetActive(true);
                                }

                            }
                            else if (ammo > 0)
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
        Invoke("Cooldown", cooldown);
        ammo--;
    }
    public void Reload()
    {
        kanSchieten = true;
        ammo = 5f;
    }
    public void Cooldown()
    {
        kanSchieten = true;
    }
}
