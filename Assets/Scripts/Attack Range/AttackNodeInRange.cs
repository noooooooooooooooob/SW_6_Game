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
    private ObjectManager objectManager;
    private GameObject player;
    private PlayerElement playerElement;
    private PlayerMovement playerMovement;
    public ArrowDirectionEnum arrowDirection;
    public ParticlePlayer particlePlayer;


    private GameObject clone;
    public List<Note> notesInTrigger = new List<Note>();

    public bool isClone;

    void Start()
    {
        particlePlayer = GameObject.Find("ParticlePlayer").GetComponent<ParticlePlayer>();
        floorChecker = GetComponentInChildren<FloorChecker>();
        player = GameObject.Find("Player");
        playerElement = player.GetComponent<PlayerElement>();
        playerMovement = player.GetComponent<PlayerMovement>();
        objectManager = GameObject.Find("Object manager").GetComponent<ObjectManager>();
        clone = GameObject.Find("clone");


        healthBarController = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
    }

    public void OnChildTrigger()
    {
        if (isClone)
        {
            playerInRange = false;
            Debug.LogWarning("isClone");
        }
        else if (floorChecker != null)
        {
            playerInRange = floorChecker.hasPlayer;
        }
    }

    void Update()
    {
        if (playerInRange && playerMovement.CanMove)
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
                Debug.Log("down");
                attack = true;
                arrowDirection = ArrowDirectionEnum.down;
            }
        }
        else if (!playerInRange)
        {
            attack = false;
        }

        if (objectManager.inputIndex < objectManager.timeStamps.Count)
        {
            double timeStamp = objectManager.timeStamps[objectManager.inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

            if (attackCheck())
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    // objectManager.DestroyNote(objectManager.inputIndex);
                    objectManager.MoveToBoss(objectManager.inputIndex);
                    objectManager.PlayHitSound(objectManager.inputIndex);
                    objectManager.inputIndex++;
                }
                attack = false;
            }
            if (timeStamp + marginOfError <= audioTime) //If not hit in time
            {
                if (!objectManager.isDestroyed(objectManager.inputIndex))
                {
                    ShakeAttackBar();
                }
                // healthBarController.TakeDamage();
                objectManager.inputIndex++;
            }
        }
    }

    void ShakeAttackBar()
    {
        AttackRangeShaker attackRangeShaker = GetComponentInParent<AttackRangeShaker>();
        attackRangeShaker.StartShaking();

    }

    bool attackCheck()
    {
        if (
            objectManager.GetCurrentNoteArrowDirection(objectManager.inputIndex) == arrowDirection && // Arrow direction
            playerElement.playerCurrentElement == objectManager.getCurrentNoteColor(objectManager.inputIndex) && // Color 
            attack && objectManager.getCurrentNoteLine(objectManager.inputIndex) == playerMovement.currentFloor - 1) //Floor
        {
            Debug.Log("TRUE");
            return true;
        }
        else
        {
            Debug.Log("False");
            return false;
        }
    }

}

