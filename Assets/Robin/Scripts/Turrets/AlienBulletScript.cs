using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBulletScript : MonoBehaviour
{
    public GameObject target;
    public int damage;
    public float speed;
    Vector3 prevPos;
    public float timeToLive;

    void Update()
    {
        prevPos = transform.position;

        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        RaycastHit[] hits = Physics.RaycastAll(new Ray(prevPos, (transform.position - prevPos).normalized), (transform.position - prevPos).magnitude);

        for(int i = 0; i < hits.Length; i++)
        {
            if(hits[i].transform.CompareTag("Turret"))
            {
                hits[i].transform.GetComponent<SingleShotScript>().TakeDamage(damage);

                Destroy(gameObject);
            }
            else if(hits[i].transform.CompareTag("House"))
            {
                hits[i].transform.GetComponent<HouseScript>().TakeDamage(damage);

                Destroy(gameObject);
            }


        }
    }
}
