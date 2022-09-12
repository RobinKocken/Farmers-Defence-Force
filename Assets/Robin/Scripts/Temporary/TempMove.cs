using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour
{
    Rigidbody rb;

    public Transform orientation;

    public int speed;
    public float drag;

    float moveZ;
    float moveX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SpeedControl();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        moveZ = Input.GetAxisRaw("Vertical");
        moveX = Input.GetAxisRaw("Horizontal");

        rb.AddForce(orientation.forward.normalized * moveZ * speed * 10, ForceMode.Force);
        rb.AddForce(orientation.right.normalized * moveX * speed * 10, ForceMode.Force);

        rb.drag = drag;

    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
