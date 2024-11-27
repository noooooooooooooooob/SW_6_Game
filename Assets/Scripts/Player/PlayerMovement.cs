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
    public float runSpeed;
    public bool CanMove = false;
    public AnimationCurve EntranceCurve;
    public AnimationCurve ExitCurve;

    private Transform playerStartLocation;
    private Transform playerEndLocation;
    private MoveToLocation moveToLocation;
    public float entraceTime;
    public float sceneExitTime;

    [SerializeField] float fallMultiplier;

    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private ReduceGaugebar staminaBar;
    private Vector2 vecGravity;
    private bool inVicotryRun;


    public int currentFloor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<Collider2D>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<ReduceGaugebar>();
        fallMultiplier = 1.2f;
        currentFloor = 1;

        CanMove = false;
        inVicotryRun = false;

        playerStartLocation = GameObject.Find("PlayerStartLocation").GetComponent<Transform>();
        playerEndLocation = GameObject.Find("PlayerEndLocation").GetComponent<Transform>();
        moveToLocation = GetComponent<MoveToLocation>();

        StartCoroutine(moveToLocation.StartMoving(playerStartLocation.position, entraceTime, EntranceCurve));
        Invoke("AllowMovement", entraceTime);

    }

    void Update()
    {
        if (CanMove)
        {
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

            if (currentFloor > 1 && staminaBar.platformTime > 0)
            {
                staminaBar.StartStaminaDecrease();
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Instantiate(smokeJumpDownPrefab, playerTransform.position, Quaternion.identity);

                    currentFloor--;
                    playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y - 3f);
                }

            }
        }
    }

    IEnumerator MoveToLocation(Vector3 target, float duration, AnimationCurve curve)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float curveValue = curve.Evaluate(elapsedTime / duration); // Evaluate the curve at the current time
            transform.position = Vector3.Lerp(startPosition, target, curveValue);
            yield return null;
        }

        transform.position = target; // Ensure final position is exact
        CanMove = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            currentFloor = 1;
            staminaBar.StopStaminaDecrease();
        }
    }
    private void AllowMovement()
    {
        CanMove = true;
    }
    public void VictoryRun()
    {
        CanMove = false;

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
