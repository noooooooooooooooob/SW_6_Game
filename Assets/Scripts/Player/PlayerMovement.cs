using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject smokeJumpUpPrefab;
    public GameObject smokeJumpDownPrefab;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private ReduceGaugebar staminaBar;
    private Transform playerStartLocation;
    private Transform playerEndLocation;
    public AnimationCurve EntranceCurve;
    public AnimationCurve ExitCurve;
    public float entraceTime;
    public float sceneExitTime;
    private MoveToLocation moveToLocation;

    public float platformTime = 5f;
    public float disableTime = 0.2f;
    public bool isAttacking = false;
    public bool inVicotryRun;
    public bool doEntrance;
    public bool doRunning;
    public bool CanMove = false;
    public int currentFloor;

    public bool isClone;
    public bool isPow;

    [SerializeField] float fallMultiplier;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<ReduceGaugebar>();
        moveToLocation = GetComponent<MoveToLocation>();

        playerStartLocation = GameObject.Find("PlayerStartLocation").GetComponent<Transform>();
        playerEndLocation = GameObject.Find("PlayerEndLocation").GetComponent<Transform>();


        if (doEntrance)
        {
            StartCoroutine(moveToLocation.StartMoving(playerStartLocation.position, entraceTime, EntranceCurve));
            Invoke("AllowMovement", entraceTime);
        }
        else
        {
            CanMove = true;
        }
        isClone=false;
    }

    void Update()
    {
        if (CanMove&& !isClone)
        {
            if (Input.GetButtonDown("Jump") && staminaBar.platformTime > 0 && currentFloor < 3)
            {
                Instantiate(smokeJumpUpPrefab, playerTransform.position, Quaternion.identity);
                currentFloor++;
                playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + 3f);
                if(isPow){
                     PlayerDamageUpEffect PD =FindObjectOfType<PlayerDamageUpEffect>();
                    PD.move();
                }
               
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            }

            if (currentFloor > 1 && staminaBar.platformTime > 0)
            {
                staminaBar.StartStaminaDecrease();
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Instantiate(smokeJumpDownPrefab, playerTransform.position, Quaternion.identity);

                    currentFloor--;
                    playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y - 3f);
                    if(isPow){
                        PlayerDamageUpEffect PD =FindObjectOfType<PlayerDamageUpEffect>();
                        PD.move();
                    }
                }

            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            currentFloor = 1;
            staminaBar.StopStaminaDecrease();
        }
    }

    public void AllowMovement()
    {
        CanMove = true;
    }
    public void DisableMovement()
    {
        CanMove = false;
    }


    public void VictoryRun()
    {
        DisableMovement();
        doRunning = true;
        if (currentFloor > 1)
        {
            staminaBar.DisablePlatforms();
        }
        else
        {
            if (inVicotryRun == false)
            {
                inVicotryRun = true;
                StartCoroutine(moveToLocation.StartMoving(playerEndLocation.position, sceneExitTime, ExitCurve));
            }
        }
    }
}
