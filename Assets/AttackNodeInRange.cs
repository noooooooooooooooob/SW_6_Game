using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackNodeInRange : MonoBehaviour
{
    public bool hasNote = false;
    public bool attack = false;
    private FloorChecker floorChecker;
    public bool playerInRange = false;

    private HealthBarController healthBarController;
    private GameObject player;
    private PlayerElement playerElement;
    public ArrowDirectionEnum arrowDirection;

    public bool isClone;

    void Start()
    {
        floorChecker = GetComponentInChildren<FloorChecker>();
        player = GameObject.Find("Player");
        playerElement = player.GetComponent<PlayerElement>();
        healthBarController = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
    }

    public void OnChildTrigger()
    {

        if(isClone){
            playerInRange=true;
        }
        else if (floorChecker != null)
        {
            playerInRange = floorChecker.hasPlayer;
        }
    }

    void Update()
    {

        if (hasNote && playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.right;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.down;
            }
        }
        else if (!playerInRange)
        {
            attack = false;
        }
    }

    void ShakeAttackBar()
    {
        AttackRangeShaker attackRangeShaker = GetComponentInParent<AttackRangeShaker>();
        attackRangeShaker.StartShaking();

    }

    void OnTriggerEnter2D(Collider2D other) // 공격범위에 들어왔을때
    {
        if (other.gameObject.tag == "Note")
        {
            hasNote = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            Note note = other.gameObject.GetComponent<Note>();
            if (attack && playerInRange)
            {
                if (note.noteArrowDirection == arrowDirection && playerElement.playerCurrentElement == note.noteColor)
                {
                    note.StartMovingToBoss();
                    attack = false;
                    hasNote = false;

                    arrowDirection = ArrowDirectionEnum.none;
                }
                else
                {
                    ShakeAttackBar();
                    other.gameObject.SetActive(false);
                    attack = false;
                    hasNote = false;
                    arrowDirection = ArrowDirectionEnum.none;

                    if (healthBarController != null)
                    {
                        healthBarController.TakeDamage();
                    }
                    else
                    {
                        Debug.LogError("HealthBarController not found on Player object.");
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            hasNote = false;
            Note note = other.gameObject.GetComponent<Note>();
            if (note != null)
            {
                note.StartMovingToPlayer();
            }
        }
    }
}

