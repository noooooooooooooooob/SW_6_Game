using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    public float jumpHeight;
    public float jump_time_to_peak;
    public float jump_time_to_descent;
    public float crouchSize;



    private float jump_velocity;
    private float jump_gravity;
    private float fall_gravity;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool hasDoubleJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump_velocity = 2 * jumpHeight / jump_time_to_peak;
        jump_gravity = 2 * jumpHeight / (jump_time_to_peak * jump_time_to_peak);
        fall_gravity = 2 * jumpHeight / (jump_time_to_descent * jump_time_to_descent);
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal and vertical inputs
        float moveX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        rb.velocity += Vector2.down * getGravity() * Time.deltaTime; //Velocity decreases overtime with set gravity

        if (Input.GetButtonDown("Jump") && (isGrounded || hasDoubleJump))
        {
            //Initial velocity starts at jump_velocity
            rb.velocity = new Vector2(rb.velocity.x, jump_velocity);
            if (!isGrounded)
            {
                hasDoubleJump = false;
            }
            isGrounded = false;
        }
        Debug.Log("Gravitiy " + getGravity());

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.localScale = new Vector2(transform.localScale.x, crouchSize * transform.localScale.y);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {

            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y / crouchSize);
        }
    }

    float getGravity()
    {
        if (rb.velocity.y < 0.0f)
        {
            return jump_gravity;
        }
        else return fall_gravity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            hasDoubleJump = true;
        }
    }
}
