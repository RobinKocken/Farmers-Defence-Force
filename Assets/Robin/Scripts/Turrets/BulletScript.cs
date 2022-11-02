using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject parent;
    public ParticleSystem particle;
    public GameObject explosion;

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
            if(hits[i].transform.CompareTag("Alien"))
            {
                hits[i].transform.GetComponent<AlienAi>().TakeDamage(damage);
                hits[i].transform.GetComponent<AlienAi>().IsHit(parent);


                Destroy(transform.GetChild(0).gameObject, 10f);
                transform.DetachChildren();
                Destroy(gameObject);
            }
        }

        Destroy(gameObject, timeToLive);
    }

    private void OnDestroy()
    {
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 10f);
        transform.DetachChildren();
    }
}
