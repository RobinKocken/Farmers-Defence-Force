using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 v;
    public float horizontal;
    public float vertical;
    public float z;
    public float walkSpeed, runSpeed;
    public float speed;
    public float stamina;
    public bool running;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stamina = 100;
        speed = walkSpeed;
        walkSpeed = 4;
        runSpeed = 8;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        v.x = horizontal;
        v.z = vertical;
        transform.Translate(v*speed * Time.deltaTime);
        //print(stamina);

        if (stamina > 0)
        {
           
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                running = true;
                speed = runSpeed;
                
                
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = walkSpeed;
                running = false;
            }
        }
        else
        {
            running = false;
            speed = walkSpeed;
        }
        if (running == true)
        {
            stamina -= 10 * Time.deltaTime;
        }
        else if (stamina < 100 )
        {
            stamina += 5 * Time.deltaTime;
        }
    }
}
