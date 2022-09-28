using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingleShotScript : MonoBehaviour
{
    public AlienManager manager;

    public GameObject target;
    public GameObject bullet;

    Rigidbody rbTarget;

    [Header("Components")]
    public Transform frame;
    public Transform cannon;
    public Transform shootPoint;
    public Transform aimingPoint;
    public Transform gearFrame;
    public Transform gearMotor;

    [Header("Rotation Speed")]
    public float rotSpeed;
    public float rotGearFrame;
    public float rotGearMotor;

    [Header("Other")]
    float startTime;
    public float waitForSeconds;

    bool aggro;


    [Header("Prediction")]
    public Vector3 currentPosition;
    public Vector3 currentVelocity;
    public Vector3 prev;
    public float targetDistance;
    public float travelTime;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<AlienManager>();
    }

    void Update()
    {
        PredictMovement();
        LookAtTarget();
        Shooting();
    }

    void PredictMovement()
    {
        currentVelocity = (target.transform.position - prev) / Time.deltaTime;
        //currentVelocity = new Vector3(0, 0, 1);
        prev = target.transform.position;

        currentPosition = target.transform.position;

        targetDistance = Vector3.Distance(shootPoint.transform.position, target.transform.position);
        travelTime = targetDistance / bullet.GetComponent<BulletScript>().speed;

        Vector3 predictedPosition = currentPosition + currentVelocity * travelTime;

        aimingPoint.position = predictedPosition;
    }    

    void LookAtTarget()
    {
        if(aggro)
        {
            //Frame Rot
            Quaternion frameQuat = Quaternion.Slerp(frame.transform.localRotation, Quaternion.LookRotation(aimingPoint.position - frame.transform.position), rotSpeed * Time.deltaTime);
            frame.transform.localEulerAngles = new Vector3(0, frameQuat.eulerAngles.y, 0);

            //Cannon Rot
            Quaternion cannonQuat = Quaternion.Slerp(cannon.transform.localRotation, Quaternion.LookRotation(aimingPoint.position - cannon.transform.position), rotSpeed * Time.deltaTime);
            cannon.transform.localEulerAngles = new Vector3(cannonQuat.eulerAngles.x, -90, 0);

            //Gear Frame
            //Work in Progress
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

    void Shooting()
    {
        if(Time.time - startTime > waitForSeconds)
        {
            GameObject hello = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);

            hello.transform.SetParent(shootPoint.transform);
            hello.transform.localRotation = shootPoint.transform.localRotation;
            hello.transform.parent = null;

            startTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!aggro && other.CompareTag("Alien"))
        {
            aggro = true;
            target = other.gameObject;

            rbTarget = target.GetComponent<Rigidbody>();
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
