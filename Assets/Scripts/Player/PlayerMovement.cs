using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    public float jumpHeight = 10;
    public float jump_time_to_peak = 1;
    public float jump_time_to_descent = 0.5f;
    public float crouchSize = 0.5f;

    public Transform body;


    private float jump_velocity;
    private float jump_gravity;
    private float fall_gravity;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool hasDoubleJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        body = transform.Find("BodyPivot");
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

        // Debug.Log(Input.GetAxis("Horizontal"));
        // if (moveX > 0)
        // {
        //     transform.rotation = Quaternion.Euler(0, 0, 0);
        // }
        // if (moveX < 0)
        // {
        //     transform.rotation = Quaternion.Euler(0, 180, 0);
        // }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (body != null)
            {
                body.localScale = new Vector2(body.localScale.x, crouchSize * body.localScale.y);
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            if (body != null)
            {
                body.localScale = new Vector2(body.localScale.x, body.localScale.y / crouchSize);
            }
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
