using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class VuurpijlenScript : Turret
{
    public int health;

    public AlienManager manager;
    public List<ParticleSystem> shotParticles;
    public GameObject target;
    public GameObject bullet;
    public Item iDBullet;

    public float radius;

    [Header("Components")]
    public Transform frame;
    public Transform cannon;
    public Transform shootHolder;
    public Transform aimingPoint;
    public Transform gearFrame;
    public Transform gearMotor;

    public List<Transform> shootPoints;
    public int number;

    [Header("Rotation Speed")]
    public float rotSpeed;
    public float rotGearFrame;
    public float rotGearMotor;

    [Header("Bullet")]
    public int bulletDamage;
    public float bulletSpeed;
    float startTime;
    public float waitForSeconds;

    bool aggro;

    [Header("Ammo")]
    public int currentAmmo;
    public int maxAmmo;
    public float currentGas;
    public float maxGas;
    public float gasPerShot;

    [Header("Prediction")]
    public Vector3 currentPosition;
    public Vector3 currentVelocity;
    public Vector3 prev;
    public float targetDistance;
    public float travelTime;

    [Header("UI")]
    public GameObject ammoUI;
    public GameObject gasUI;
    public TMP_Text ammoText;
    public TMP_Text gasText;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<AlienManager>();

        Initialize();
        
        for(int i = 0; i < shotParticles.Count; i++)
        {
            shotParticles[i].Stop();
        }
    }

    void Update()
    {
        Target();
        OverlapSphere();

        if(aggro)
        {
            LookAtTarget();
            PredictMovement();
            Shooting();
        }

        UI();
        Death();
    }

    void Initialize()
    {
        for(int i = 0; i < shootHolder.childCount; i++)
        {
            shootPoints.Add(shootHolder.GetChild(i));
        }

        for(int i = 0; i < shootPoints.Count; i++)
        {
            shotParticles.Add(shootPoints[i].GetComponentInChildren<ParticleSystem>());
        }
    }

    void Target()
    {
        if(target == null)
        {
            aggro = false;
        }
        else if(target != null)
        {
            aggro = true;
        }
    }

    void PredictMovement()
    {
        if(target != null)
        {
            currentVelocity = (target.transform.position - prev) / Time.deltaTime;
            prev = target.transform.position;

            currentPosition = target.transform.position;

            targetDistance = Vector3.Distance(shootHolder.transform.position, target.transform.position);
            travelTime = targetDistance / bulletSpeed;

            Vector3 predictedPosition = currentPosition + currentVelocity * travelTime;

            aimingPoint.position = predictedPosition;
        }
    }

    void LookAtTarget()
    {
        //Frame Rot
        Quaternion frameQuat = Quaternion.Slerp(frame.transform.localRotation, Quaternion.LookRotation(transform.InverseTransformPoint(aimingPoint.position) - frame.transform.localPosition), rotSpeed * Time.deltaTime);
        frame.transform.localEulerAngles = new Vector3(0, frameQuat.eulerAngles.y, 0);

        //Cannon Rot
        Quaternion cannonQuat = Quaternion.Slerp(cannon.transform.localRotation, Quaternion.LookRotation(transform.InverseTransformPoint(aimingPoint.position) - cannon.transform.localPosition), rotSpeed * Time.deltaTime);
        cannon.transform.localEulerAngles = new Vector3(cannonQuat.eulerAngles.x, -90, 0);

        //Gear Frame
        //Work in Progress
        //Vector3 oldGearCannonAngles = cannon.transform.localEulerAngles;

        //if(oldGearCannonAngles != cannon.transform.localEulerAngles)
        //{
        //    if(oldGearCannonAngles.z > cannon.transform.localEulerAngles.z)
        //    {

        //    }
        //    else if(oldGearCannonAngles.z < cannon.transform.localEulerAngles.z)
        //    {

        //    }
        //}
    }

    void Shooting()
    {
        if(Time.time - startTime > waitForSeconds && currentAmmo > 0 && currentGas > 0)
        {
            shotParticles[number].Play();

            GameObject currentBullet = Instantiate(bullet, shootPoints[number].transform.position, Quaternion.identity);

            currentAmmo--;
            currentGas -= gasPerShot;

            currentBullet.transform.SetParent(shootPoints[number].transform);
            currentBullet.transform.eulerAngles = shootPoints[number].transform.eulerAngles;
            currentBullet.transform.parent = null;

            currentBullet.GetComponent<BulletScript>().parent = gameObject;
            currentBullet.GetComponent<BulletScript>().speed = bulletSpeed;
            currentBullet.GetComponent<BulletScript>().damage = bulletDamage;

            number++;
            if(number == shootPoints.Count)
            {
                number = 0;
            }
            startTime = Time.time;
        }
    }

    public int ReloadAmmo(int ammoAmount)
    {
        if(currentAmmo < maxAmmo && ammoAmount > 0)
        {
            int needAmmo = 0;
            if(currentAmmo == 0)
            {
                needAmmo = maxAmmo;
            }
            else if(currentAmmo > 0)
            {
                needAmmo = maxAmmo - currentAmmo;
            }

            if(ammoAmount - needAmmo > 0)
            {
                currentAmmo += needAmmo;

                return needAmmo;
            }
            else if(ammoAmount - needAmmo <= 0)
            {
                currentAmmo += ammoAmount;

                return ammoAmount;
            }
        }

        ammoAmount = 0;
        return ammoAmount;
    }

    public float ReloadGas(float gasAmount)
    {
        if(currentGas < maxGas && gasAmount > 0)
        {
            float needGas;
            if(currentGas == 0)
            {
                needGas = maxGas;
            }
            else
            {
                needGas = maxGas - currentGas;
            }

            if(gasAmount - needGas > 0)
            {
                currentGas += needGas;

                return needGas;
            }
            else if(gasAmount - needGas <= 0)
            {
                currentGas += gasAmount;

                return gasAmount;
            }

        }

        gasAmount = 0;
        return gasAmount;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Death()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UI()
    {
        if(currentAmmo <= 0)
        {
            ammoUI.SetActive(true);
        }
        else
        {
            ammoUI.SetActive(false);
        }

        if(currentGas <= 0)
        {
            gasUI.SetActive(true);
        }
        else
        {
            gasUI.SetActive(false);
        }

        ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
        gasText.text = currentGas.ToString() + "%";
    }

    void OverlapSphere()
    {
        if(!aggro)
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, radius);

            foreach(var hitCollider in collider)
            {
                if(hitCollider.transform.CompareTag("Alien"))
                {
                    target = hitCollider.gameObject;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
