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

    public float platformTime = 5f;
    public float disableTime = 0.2f;

    public Transform body;
    Collider2D plat1;
    Collider2D plat2;

    private float jump_velocity;
    private float jump_gravity;
    private float fall_gravity;

    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isOnPlatform = false;
    private bool hasDoubleJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        plat1 = GameObject.Find("2nd floor").GetComponent<Collider2D>();
        plat2 = GameObject.Find("3rd floor").GetComponent<Collider2D>();
        body = transform.Find("BodyPivot");
        jump_velocity = 2 * jumpHeight / jump_time_to_peak;
        jump_gravity = 2 * jumpHeight / (jump_time_to_peak * jump_time_to_peak);
        fall_gravity = 2 * jumpHeight / (jump_time_to_descent * jump_time_to_descent);

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity += Vector2.down * GetGravity() * Time.deltaTime; //Velocity decreases overtime with set gravity

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


        if (isOnPlatform == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("Pressed down");
                StartCoroutine(DisablePlayerCollider(disableTime));
            }

            platformTime -= Time.deltaTime;
        }

        if (platformTime <= 0)
        {
            plat1.enabled = false;
            plat2.enabled = false;
        }
        else
        {
            plat1.enabled = true;
            plat2.enabled = true;
        }

    }

    float GetGravity()
    {
        if (rb.velocity.y < 0.0f)
        {
            return jump_gravity;
        }
        else return fall_gravity;
    }

    private IEnumerator DisablePlayerCollider(float disableTime)
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(disableTime);
        playerCollider.enabled = true;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            hasDoubleJump = true;
            platformTime = 5f;
        }
        else if (other.gameObject.tag == "platform")
        {
            Debug.Log("On Platform");
            isGrounded = true;
            hasDoubleJump = true;
            isOnPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            isOnPlatform = false;
        }
    }
}
