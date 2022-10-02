using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBulletScript : MonoBehaviour
{
    public GameObject target;
    Rigidbody rb;
    public int damage;
    public float speed;
    Vector3 prevPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        rb.AddForce(transform.forward * speed);

        prevPos = transform.position;

        Quaternion quatBullet = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 100);
        transform.eulerAngles = new Vector3(quatBullet.eulerAngles.x, quatBullet.eulerAngles.y, quatBullet.eulerAngles.z);

        RaycastHit[] hits = Physics.RaycastAll(new Ray(prevPos, (transform.position - prevPos).normalized), (transform.position - prevPos).magnitude);

        for(int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i]);
            if(hits[i].transform.CompareTag("Turret"))
            {
                hits[i].transform.GetComponent<SingleShotScript>().TakeDamage(damage);

                Destroy(gameObject);
                Debug.Log("Hit Turret");
            }
            else if(hits[i].transform.CompareTag("House"))
            {
                hits[i].transform.GetComponent<HouseScript>().TakeDamage(damage);

                Destroy(gameObject);
                Debug.Log("Hit House");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("House"))
        {
            collision.transform.GetComponent<HouseScript>().TakeDamage(damage);

            Destroy(gameObject);
            Debug.Log("Hit House");
        }
        else if(collision.transform.CompareTag("Turret"))
        {
            collision.transform.GetComponent<SingleShotScript>().TakeDamage(damage);

            Destroy(gameObject);
            Debug.Log("Hit Turret");
        }
    }
}
