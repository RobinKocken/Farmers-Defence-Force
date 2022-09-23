using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CharacterController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float stamina;
    public float walkSpeed, runSpeed, boostSpeed;
    public PickUp pickup;
    public bool running;

    CharacterController characterController;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        stamina = 100;
        walkSpeed = 4;
        runSpeed = 8;
        boostSpeed = 12;
        jumpSpeed = 8;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
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
        //print(stamina);
        if (stamina == 100 || stamina > 100)
        {
            stamina = 100;
        }
        if (stamina == 0 || stamina < 0)
        {
            stamina = 0;
        }
        if (pickup.jumpBoostIsActief == true)
        {
            jumpSpeed = 20;
        }
        else if (pickup.jumpBoostIsActief == false)
        {
            jumpSpeed = 8;
        }
    }
    public void Stamina()
    {
        stamina += 5 * Time.deltaTime;
    }
}
    

