using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.AI;

public class AlienAi : MonoBehaviour
{
    public NavMeshAgent alienAgent;
    public GameObject shootPoint;
    public int health;

    public GameObject house;
    public GameObject aimPoint;
    public GameObject target;

    public float distance;

    public bool aggro;
    public bool pathing;

    [Header("Bullet")]
    public GameObject bullet;
    public float bulletSpeed;
    public int damage;
    float startTime;
    public float waitForSeconds;

    void Start()
    {
        alienAgent = GetComponent<NavMeshAgent>();
        alienAgent.enabled = false;

        house = GameObject.FindGameObjectWithTag("House");
        aimPoint = GameObject.FindGameObjectWithTag("House").GetComponent<HouseScript>().hitPoint;
        target = house;
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        Aiming();
        if(alienAgent.enabled == true)
        {
            Path();
            Attack();
        }

        Death();
    }

    void Path()
    {
        if(distance > 30)
        {
            alienAgent.SetDestination(target.transform.position);
        }
        else if(distance < 29)
        {
            alienAgent.SetDestination(transform.position);
        }
    }

    void Attack()
    {
        if(target != null && distance < 40)
        {
            Shooting();
        }
        else if(target == null)
        {
            target = house;
        }
    }

    void Shooting()
    {
        if(Time.time - startTime > waitForSeconds)
        {
            GameObject b = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);

            b.transform.SetParent(shootPoint.transform);
            b.transform.eulerAngles = shootPoint.transform.eulerAngles;
            b.transform.parent = null;

            b.GetComponent<AlienBulletScript>().target = target;
            b.GetComponent<AlienBulletScript>().speed = bulletSpeed;
            b.GetComponent<AlienBulletScript>().damage = damage;

            startTime = Time.time;
        }
    }

    void Aiming()
    {
        Quaternion quatShoot = Quaternion.Slerp(shootPoint.transform.localRotation, Quaternion.LookRotation(aimPoint.transform.position - shootPoint.transform.position), 100);
        shootPoint.transform.eulerAngles = new Vector3(quatShoot.eulerAngles.x, quatShoot.eulerAngles.y, quatShoot.eulerAngles.z);
    }

    void IsHit(GameObject turretHit)
    {
        target = turretHit;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Death()
    {
        if(health <= 0)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
        }
    }

    public IEnumerator Lerping(Vector3 goTo)
    {
        while(true)
        {
            float distance = Vector3.Distance(transform.position, goTo);

            if(distance > 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, goTo, 2 * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goTo - transform.position), 5 * Time.deltaTime);
            }
            else
            {
                alienAgent.enabled = true;
                yield break;
            }

            yield return null;
        }
    }
}
