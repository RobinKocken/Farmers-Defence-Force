using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public Vector3 moveDirection;
    public Vector3 jumpPower;

    public float horizontal;
    public float vertical;
    public float walkSpeed, runSpeed, boostSpeed, speed;
    public float stamina;

    public bool isGrounded;
    public bool running;

    public PickUp pickup;
    public WeaponSwayAndBob bob;
    public RaycastHit hit;

    public Transform feet;
    public Transform playerBody;
    public Transform cam;
    public Transform gun;
    public Camera playerCamera;
    public GameObject player;
    

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stamina = 100;
        speed = walkSpeed;
        walkSpeed = 10;
        runSpeed = 20;
        boostSpeed = 30;
        jumpPower.y = 7f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.Paused) return;//Speler kan niks doen waneer de game gepauzeerd is
        Vector3 previous = transform.position;
        Vector3 velocity = (transform.position - previous) / Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            gun.transform.position += new Vector3(0, 0f, 1) * Time.deltaTime;
            gun.transform.position += new Vector3(0, 0f, -1) * Time.deltaTime;
        }

        if (isGrounded == true)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            moveDirection.x = horizontal;
            moveDirection.z = vertical;
            rb.AddForce(transform.forward.normalized * speed *vertical);
            rb.AddForce(transform.right.normalized * speed * horizontal);
            rb.drag = 5;

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
                rb.drag = 0;
                if (isGrounded == true)
                {
                    isGrounded = false;
                    GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    GetComponent<Rigidbody>().velocity += jumpPower;
                }
            }
            if (pickup.jumpBoostIsActief == true)
            {
                jumpPower.y = 14f;
            }
            else if (pickup.jumpBoostIsActief == false)
            {
                jumpPower.y = 7f;
            }
        }

        //mouse options
        float mouseX = Input.GetAxis("Mouse X") * Options.xSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Options.ySens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        //ladder physics
        if (Physics.Raycast(feet.position, feet.forward, out hit, 1))
        {
            print(hit.transform.tag);
            if (hit.transform.gameObject.tag == "Ladder")
            {
                if (Input.GetKey(KeyCode.W))
                {
                    player.transform.position += new Vector3(0, 5f, 0) * Time.deltaTime;
                }
            }
        }
    }

    //jump
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    //stamina
    public void Stamina()
    {
        stamina += 5 * Time.deltaTime;
    }
}
