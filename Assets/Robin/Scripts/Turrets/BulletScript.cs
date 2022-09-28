using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    Vector3 prevPos;

    void Update()
    {
        prevPos = transform.position;

        transform.Translate(speed * Time.deltaTime * Vector3.forward);

        RaycastHit[] hits = Physics.RaycastAll(new Ray(prevPos, (transform.position - prevPos).normalized), (transform.position - prevPos).magnitude);
        
        for(int i = 0; i < hits.Length; i++)
        {
            if(hits[i].transform.CompareTag("Alien"))
            {
                Destroy(gameObject);
                Debug.Log("Hit");
            }
        }
    }
}
