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
    public Transform gear;
    public Transform cannon;

    bool aggro;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<AlienManager>();
    }

    void Update()
    {
        if(aggro)
        {
            frame.transform.rotation = Quaternion.Slerp(frame.transform.rotation, Quaternion.LookRotation(target.transform.position - frame.transform.position), 5 * Time.deltaTime);
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
}
