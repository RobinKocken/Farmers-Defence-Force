using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotScript : MonoBehaviour
{
    public AlienManager manager;

    public GameObject target;
    public GameObject bullet;

    [Header("Components")]
    public Transform frame;
    public Transform cannon;
    public Transform gearFrame;
    public Transform gearMotor;

    float startTime;
    public float waitForSeconds;

    bool aggro;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<AlienManager>();
    }

    void Update()
    {
        if(aggro)
        {
            Quaternion frameQuat = Quaternion.Slerp(frame.transform.rotation, Quaternion.LookRotation(target.transform.position - frame.transform.position), 5 * Time.deltaTime);
            Quaternion newRotFrame = new Quaternion(0, frameQuat.y, 0, frameQuat.w);
            frame.transform.localRotation = newRotFrame;

            Quaternion cannonQuat = Quaternion.Slerp(cannon.transform.rotation, Quaternion.LookRotation(target.transform.position - cannon.transform.position), 5 * Time.deltaTime);
            Quaternion newRotCannon = new Quaternion(cannonQuat.x, 0, 0, cannonQuat.w);
            cannon.transform.localRotation = newRotCannon;
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

    private void OnTriggerExit(Collider other)
    {
        if(aggro && other.CompareTag("Alien"))
        {
            aggro = false;
            target = null;
        }
    }
}
