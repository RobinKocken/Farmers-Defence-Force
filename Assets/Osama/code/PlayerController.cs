using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public Vector3 moveDirection;
    public Vector3 jump;

    public float jumpPower;
    public float horizontal;
    public float vertical;
    public float moveVertical;
    public float walkSpeed, runSpeed, boostSpeed, speed;
    public float stamina;

    public float weaponBobSpeed;
    public float weaponBobAmount;

    public bool isGrounded;
    public bool running;

    public PickUp pickup;
    public CameraBob bob;
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
        walkSpeed = 3;
        speed = walkSpeed;
        runSpeed = 6;
        boostSpeed = 12;
        jumpPower = 7f;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.Paused) return;//Speler kan niks doen waneer de game gepauzeerd is
        Vector3 previous = transform.position;
        Vector3 velocity = (transform.position - previous) / Time.deltaTime;

        //gunbob
        if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
        {
            gun.transform.localPosition += new Vector3(Mathf.Cos(Time.time * weaponBobSpeed) * weaponBobAmount, Mathf.Cos(Time.time * weaponBobSpeed) * weaponBobAmount, 0) * Time.deltaTime;
        }

        //movement
        if (isGrounded == true)
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
                            speed = runSpeed+ GetComponent<Rigidbody>().velocity.z;


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
                        stamina -= 5 * Time.deltaTime;
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

           //jump
            if (Input.GetButtonDown("Jump"))
            {
                rb.drag = 0;
                if (isGrounded == true)
                {
                    isGrounded = false;
                    GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x, rb.velocity.y + jumpPower, rb.velocity.z);
                    rb.AddForce(jump * jumpPower, ForceMode.Impulse);
                }
            }
            if (pickup.jumpBoostIsActief == true)
            {
                jumpPower = 6f;
            }
            else if (pickup.jumpBoostIsActief == false)
            {
                jumpPower = 3f;
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            bob.enabled = false;
            cam.transform.position += new Vector3(0, -1f, 0);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            bob.enabled = true;
            cam.transform.position += new Vector3(0, 1f, 0);
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

    public void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDirection.x = horizontal;
        moveDirection.z = vertical;
        transform.Translate(moveDirection * Time.deltaTime * speed);
        rb.drag = 1;
    }
}
