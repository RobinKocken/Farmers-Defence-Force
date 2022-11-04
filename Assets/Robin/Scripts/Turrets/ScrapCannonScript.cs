using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrapCannonScript : Turret
{
    public int health;

    public AlienManager manager;
    public ParticleSystem shotParticle;
    public GameObject target;
    public GameObject bullet;
    public Item iDBullet;

    public float radius;

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

        shotParticle.Stop();
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

            targetDistance = Vector3.Distance(shootPoint.transform.position, target.transform.position);
            travelTime = targetDistance / bulletSpeed;

            Vector3 predictedPosition = currentPosition + currentVelocity * travelTime;

            aimingPoint.position = predictedPosition;
        }
    }

    void LookAtTarget()
    {
        //Frame Rot
        Vector3 oldGearFrame = frame.transform.localEulerAngles;
        Quaternion frameQuat = Quaternion.Slerp(frame.transform.localRotation, Quaternion.LookRotation(transform.InverseTransformPoint(aimingPoint.position) - frame.transform.localPosition), rotSpeed * Time.deltaTime);
        frame.transform.localEulerAngles = new Vector3(0, frameQuat.eulerAngles.y, 0);

        //Cannon Rot
        Vector3 oldGearMotor = cannon.transform.localEulerAngles;
        Quaternion cannonQuat = Quaternion.Slerp(cannon.transform.localRotation, Quaternion.LookRotation(transform.InverseTransformPoint(aimingPoint.position) - cannon.transform.localPosition), rotSpeed * Time.deltaTime);
        cannon.transform.localEulerAngles = new Vector3(cannonQuat.eulerAngles.x, -90, 0);

        //Gear Frame
        if(oldGearFrame != frame.transform.localEulerAngles)
        {
            if(oldGearFrame.x > frame.transform.localEulerAngles.x)
            {
                gearFrame.Rotate(0, 0, rotSpeed * Time.deltaTime);
            }
            else if(oldGearFrame.x < frame.transform.localEulerAngles.x)
            {
                gearFrame.Rotate(0, 0, -rotSpeed * Time.deltaTime);
            }
        }

        if(oldGearMotor != cannon.transform.localEulerAngles)
        {
            if(oldGearMotor.x > cannon.transform.localEulerAngles.x)
            {
                gearMotor.Rotate(0, 0, rotSpeed * Time.deltaTime);
            }
            else if(oldGearMotor.x < cannon.transform.localEulerAngles.x)
            {
                gearMotor.Rotate(0, 0, -rotSpeed * Time.deltaTime);
            }
        }
    }

    void Shooting()
    {
        if(Time.time - startTime > waitForSeconds && currentAmmo > 0 && currentGas > 0)
        {
            shotParticle.Play();

            GameObject currentBullet = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);

            currentAmmo--;
            currentGas -= gasPerShot;

            currentBullet.transform.SetParent(shootPoint.transform);
            currentBullet.transform.eulerAngles = shootPoint.transform.eulerAngles;
            currentBullet.transform.parent = null;

            currentBullet.GetComponent<BulletScript>().parent = gameObject;
            currentBullet.GetComponent<BulletScript>().speed = bulletSpeed;
            currentBullet.GetComponent<BulletScript>().damage = bulletDamage;

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
