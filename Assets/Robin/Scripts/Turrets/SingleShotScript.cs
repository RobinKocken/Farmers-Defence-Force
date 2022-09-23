using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingleShotScript : MonoBehaviour
{
    public AlienManager manager;

    public GameObject target;
    public GameObject bullet;

    [Header("Components")]
    public Transform frame;
    public Transform cannon;
    public Transform shootPoint;
    public Transform gearFrame;
    public Transform gearMotor;

    [Header("Rotation Speed")]
    public float rotSpeed;
    public float rotGearFrame;
    public float rotGearMotor;

    float startTime;
    public float waitForSeconds;

    bool aggro;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<AlienManager>();
    }

    void Update()
    {
        Aiming();

        if(Time.time - startTime > waitForSeconds)
        {
            GameObject hello = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
            startTime = Time.time;
        }

    }

    void Aiming()
    {
        if(aggro)
        {
            //Frame Rot
            Quaternion frameQuat = Quaternion.Slerp(frame.transform.localRotation, Quaternion.LookRotation(target.transform.position - frame.transform.position), rotSpeed * Time.deltaTime);
            frame.transform.localEulerAngles = new Vector3(0, frameQuat.eulerAngles.y, 0);

            //Cannon Rot
            Quaternion cannonQuat = Quaternion.Slerp(cannon.transform.localRotation, Quaternion.LookRotation(target.transform.position - cannon.transform.position), rotSpeed * Time.deltaTime);
            cannon.transform.localEulerAngles = new Vector3(cannonQuat.eulerAngles.x, -90, 0);

            //Gear Frame
            Vector3 oldGearCannonAngles = cannon.transform.localEulerAngles;

            if(oldGearCannonAngles != cannon.transform.localEulerAngles)
            {
                if(oldGearCannonAngles.z > cannon.transform.localEulerAngles.z)
                {

                }
                else if(oldGearCannonAngles.z < cannon.transform.localEulerAngles.z)
                {

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!aggro && other.CompareTag("Alien"))
        {
            aggro = true;
            target = other.gameObject;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if(aggro && other.CompareTag("Alien"))
    //    {
    //        aggro = false;
    //        target = null;
    //    }
    //}
}
