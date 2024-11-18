using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float platformTime = 5f;
    public float disableTime = 0.2f;
    public bool isAttacking = false;
    public GameObject smokeJumpUpPrefab;
    public GameObject smokeJumpDownPrefab;

    Collider2D plat1;
    Collider2D plat2;

    [SerializeField] float fallMultiplier;

    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private Vector2 vecGravity;

    public int currentFloor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<Collider2D>();

        plat1 = GameObject.Find("2nd floor").GetComponent<Collider2D>();
        plat2 = GameObject.Find("3rd floor").GetComponent<Collider2D>();
        currentFloor = 1;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && platformTime > 0 && currentFloor < 3)
        {
            Instantiate(smokeJumpUpPrefab, playerTransform.position, Quaternion.identity);
            currentFloor++;
            playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + 3f);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }

        if (currentFloor > 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Instantiate(smokeJumpDownPrefab, playerTransform.position, Quaternion.identity);

                currentFloor--;
                playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y - 3f);
            }

            platformTime -= Time.deltaTime;
        }

        if (platformTime <= 0)
        {
            currentFloor = 1;
            plat1.enabled = false;
            plat2.enabled = false;
        }
        else
        {
            plat1.enabled = true;
            plat2.enabled = true;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            platformTime = 5f;
        }
    }


}
