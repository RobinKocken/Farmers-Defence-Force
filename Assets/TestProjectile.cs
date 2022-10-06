using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    GameObject particle;
    float speed;

    public void Initialize(GameObject particle, float speed)
    {
        this.particle = particle;
        this.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) return;

        var obj = Instantiate(particle, transform.position,Quaternion.identity).transform;

        obj.rotation = Quaternion.LookRotation(collision.GetContact(0).normal);

        obj.SendMessage("Play",SendMessageOptions.DontRequireReceiver);

        Destroy(obj.gameObject,10f);
        Destroy(gameObject);
    }
}
