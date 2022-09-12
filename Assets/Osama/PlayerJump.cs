using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded;
    public Vector3 jumpPower;

    void Update()
    {
        if (PauseManager.Paused) return;

        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded==true)
            {
                isGrounded = false;
                GetComponent<Rigidbody>().velocity += jumpPower;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            isGrounded = true;
        }
    }
}
