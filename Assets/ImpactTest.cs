using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactTest : MonoBehaviour
{
    public Transform cam;

    public GameObject projectile;
    public GameObject particle;

    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var projectile = Instantiate(this.projectile, cam.position + cam.forward * 3, Quaternion.identity).GetComponent<TestProjectile>();
            if (projectile)
            {
                projectile.transform.rotation = cam.rotation;
                projectile.Initialize(particle,speed);
            }
        }
    }
}
