using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloneColor : MonoBehaviour
{
    public float platformTime = 5f;
    //public float disableTime = 0.2f;
    public bool isAttacking = false;
    public GameObject smokeJumpUpPrefab;
    public GameObject smokeJumpDownPrefab;

    [SerializeField] float fallMultiplier;

    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private Transform playerTransform;
    //private ReduceGaugebar staminaBar;
    //private Vector2 vecGravity;

    public int currentFloor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<Collider2D>();
        //staminaBar = GameObject.Find("StaminaBar").GetComponent<ReduceGaugebar>();
        //fallMultiplier = 1.2f;
        //currentFloor = 1;
    }

    void Update()
    {
        /*
        if (Input.GetButtonDown("Jump") && staminaBar.platformTime > 0 && currentFloor < 3)
        {
            Instantiate(smokeJumpUpPrefab, playerTransform.position, Quaternion.identity);
            currentFloor++;
            playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + 3f);
        }
        

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }

        if (currentFloor > 1)
        {
            staminaBar.StartStaminaDecrease();
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Instantiate(smokeJumpDownPrefab, playerTransform.position, Quaternion.identity);

                currentFloor--;
                playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y - 3f);
            }

        }
        */
    }
    /*
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            currentFloor = 1;
            staminaBar.StopStaminaDecrease();


        }
    }
    */
}
