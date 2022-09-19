using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 v;

    public float horizontal;
    public float vertical;
    public float z;
    public float walkSpeed, runSpeed, boostSpeed;
    public float speed;
    public float stamina;

    public bool running;

    public PickUp pickup;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stamina = 100;
        speed = walkSpeed;
        walkSpeed = 4;
        runSpeed = 8;
        boostSpeed = 12;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.Paused) return;//Speler kan niks doen waneer de game gepauzeerd is

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        v.x = horizontal;
        v.z = vertical;
        transform.Translate(v*speed * Time.deltaTime);

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
        print(stamina);
        if(stamina==100 || stamina > 100)
        {
            stamina = 100;
        }
        if (stamina == 0 || stamina < 0)
        {
            stamina = 0;
        }
    }
    public void Stamina()
    {
        stamina += 5 * Time.deltaTime;
    }
}
