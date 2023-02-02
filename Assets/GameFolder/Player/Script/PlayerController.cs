// **** LIBRARIES **** //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Required class MonoBehavior
public class PlayerController : MonoBehaviour
{
    // **** VARIABLES **** //
    Rigidbody2D rb;

    [SerializeField] Vector2 vel;
    [SerializeField] Vector2 force;

    public  Transform floorCollider;
    public  Transform skin;

    public  int comboNumber;
    public  float comboTime;


    // FunctionStart - start *"Whenever there are physics values, create variables in Start"
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        force = new Vector2(0, 140);
    }

    // FunctionLoop - always running and updating
    void Update()
    {
        // **** ATTACK **** //
        comboTime = comboTime + Time.deltaTime; // comboTime=0 + goes up from 0 to.....
        if (Input.GetButtonDown("Fire1") && comboTime > 0.5f)
        {
            comboNumber++;
            if(comboNumber > 2)
            {
                comboNumber = 1;
            }

            comboTime = 0;   
            skin.GetComponent<Animator>().Play("PlayerAttack" + comboNumber, -1); // Play PlayerAttack1 - Searches among all existing layers, starts at 0 so -1 will never exist
        }

        if (comboTime >= 1) // comboTime break
        {
            comboNumber = 0;
        }


        // **** MOVEMENT **** //
        // Jump
        if (Input.GetButtonDown("Jump") && floorCollider.GetComponent<FloorCollider>().canJump == true)
        {
            skin.GetComponent<Animator>().Play("PlayerJump", -1); // Play PlayerJump - Searches among all existing layers, starts at 0 so -1 will never exist
            rb.velocity = Vector2.zero;
            floorCollider.GetComponent<FloorCollider>().canJump = false;
            rb.AddForce(force);
        }
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);   // Horizontal = x1, y-1

        // Run skin change
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1); // Skin mirror
            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }

    }

    // FunctionFixedUpdate - Levels FPS for everyone always updates every 0.2s
    private void FixedUpdate()
    {
        // Leveled player speeed
        rb.velocity = vel;
    }
}
