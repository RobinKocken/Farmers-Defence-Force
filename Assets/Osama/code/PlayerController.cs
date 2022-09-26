using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 v;
    public Vector3 jumpPower;

    public float horizontal;
    public float vertical;
    public float walkSpeed, runSpeed, boostSpeed, speed;
    public float stamina;

    public bool isGrounded;
    public bool running;

    public PickUp pickup;

    public Transform playerBody;
    public Camera playerCamera;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stamina = 100;
        speed = walkSpeed;
        walkSpeed = 4;
        runSpeed = 8;
        boostSpeed = 12;
        jumpPower.y = 9f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.Paused) return;//Speler kan niks doen waneer de game gepauzeerd is

        if (isGrounded == true)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            v.x = horizontal;
            v.z = vertical;
            transform.Translate(v * speed * Time.deltaTime);

            if (isGrounded == false)
            {
                if (pickup.speedBoostIsActief == true)
                {
                    speed = boostSpeed;
                    stamina = 100;
                }
                else if (pickup.speedBoostIsActief == false)
                {
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
                    else if (stamina < 100)
                    {
                        Invoke("Stamina", 3);
                    }
                }
            }

            //print(stamina);
            if (stamina == 100 || stamina > 100)
            {
                stamina = 100;
            }
            if (stamina == 0 || stamina < 0)
            {
                stamina = 0;
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded == true)
                {
                    isGrounded = false;
                    //playerBody.transform.position = jumpPower * Time.deltaTime;
                    GetComponent<Rigidbody>().velocity += jumpPower;

                }
            }
            if (pickup.jumpBoostIsActief == true)
            {
                jumpPower.y = 20f;
            }
            else if (pickup.jumpBoostIsActief == false)
            {
                jumpPower.y = 9f;
            }
            
        }

        float mouseX = Input.GetAxis("Mouse X") * Options.xSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Options.ySens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            isGrounded = true;
        }
    }
    public void Stamina()
    {
        stamina += 5 * Time.deltaTime;
    }
}
