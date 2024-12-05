using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    public float attackDelay = 0.3f;

    PlayerMovement playerMovement;
    PlayerElement playerElement;

    Animator animator;
    PlayerAnimationEnum currentState;

    public bool isAttacking = false;
    public bool pressedAttack = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerElement = GetComponent<PlayerElement>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    public void Update()
    {
        if (playerMovement.CanMove) //Disable attack animation when player going through entrance or exit transition
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                pressedAttack = true;
                ChangeAnimationState(GetAttackAnimation("up"));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                pressedAttack = true;
                ChangeAnimationState(GetAttackAnimation("down"));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                pressedAttack = true;
                ChangeAnimationState(GetAttackAnimation("side"));
            }
        }

        if (playerMovement.currentFloor > 1 && !pressedAttack)//|| !playerMovement.doRunning) //Change to idle when above floor 1
        {
            ChangeAnimationState(GetIdleAnimation());
        }
        else if (pressedAttack)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Invoke("AttackComplete", attackDelay);
            }
        }
        else
        {
            if (playerMovement.doRunning)
            {
                ChangeAnimationState(GetRunAnimation());
            }
            else
            {
                ChangeAnimationState(GetIdleAnimation());
            }

        }
    }

    PlayerAnimationEnum GetAttackAnimation(string direction)
    {
        switch (playerElement.playerCurrentElement)
        {
            case ColorEnum.blue:
                if (direction == "up") return PlayerAnimationEnum.blue_attack_up;
                if (direction == "down") return PlayerAnimationEnum.blue_attack_down;
                return PlayerAnimationEnum.blue_attack_side;
            case ColorEnum.green:
                if (direction == "up") return PlayerAnimationEnum.green_attack_up;
                if (direction == "down") return PlayerAnimationEnum.green_attack_down;
                return PlayerAnimationEnum.green_attack_side;
            default:
                if (direction == "up") return PlayerAnimationEnum.red_attack_up;
                if (direction == "down") return PlayerAnimationEnum.red_attack_down;
                return PlayerAnimationEnum.red_attack_side;
        }
    }

/*
    PlayerAnimationEnum GetIdleAnimation()
    {
        switch (playerElement.playerCurrentElement)
        {
            case ColorEnum.blue:
                return PlayerAnimationEnum.blue_idle;
            case ColorEnum.green:
                return PlayerAnimationEnum.green_idle;
            default:
                return PlayerAnimationEnum.red_idle;
        }
    }
    PlayerAnimationEnum GetRunAnimation()
    {
        switch (playerElement.playerCurrentElement)
        {
            case ColorEnum.blue:
                return PlayerAnimationEnum.blue_run;
            case ColorEnum.green:
                return PlayerAnimationEnum.green_run;
            default:
                return PlayerAnimationEnum.red_run;
        }
    }
*/

     PlayerAnimationEnum GetIdleAnimation()
    {
        inventory IVT=FindObjectOfType<inventory>();
        
        switch (playerElement.playerCurrentElement)
        {
            case ColorEnum.blue:
                IVT.state=PlayerAnimationEnum.blue_idle;
                return PlayerAnimationEnum.blue_idle;
            case ColorEnum.green:
                IVT.state=PlayerAnimationEnum.green_idle;
                return PlayerAnimationEnum.green_idle;
            default:
                IVT.state=PlayerAnimationEnum.red_idle;
                return PlayerAnimationEnum.red_idle;
        }
    }
    PlayerAnimationEnum GetRunAnimation()
    {
        inventory IVT=FindObjectOfType<inventory>();
        switch (playerElement.playerCurrentElement)
        {
            case ColorEnum.blue:
                IVT.state=PlayerAnimationEnum.blue_run;
                return PlayerAnimationEnum.blue_run;
            case ColorEnum.green:
                IVT.state=PlayerAnimationEnum.green_run;
                return PlayerAnimationEnum.green_run;
            default:
                IVT.state=PlayerAnimationEnum.red_run;
                return PlayerAnimationEnum.red_run;
        }
    }
    void AttackComplete()
    {
        isAttacking = false;
        pressedAttack = false;
    }

    void ChangeAnimationState(PlayerAnimationEnum newState)
    {
        if (currentState == newState) return;

        // Debug.Log("Changing state to: " + newState.ToString());
        animator.Play(newState.ToString());

        currentState = newState;

    }
    public void ChangeOriginColor(PlayerAnimationEnum state){
        playerMovement = GetComponent<PlayerMovement>();

         if(state==PlayerAnimationEnum.blue_run||state==PlayerAnimationEnum.blue_idle){
            
            if(playerMovement.currentFloor>1) {
                state=PlayerAnimationEnum.blue_idle;
            }
            else{
                state=PlayerAnimationEnum.blue_run;
            }
        }
        if(state==PlayerAnimationEnum.green_run||state==PlayerAnimationEnum.green_idle){
            
            if(playerMovement.currentFloor>1) {
                state=PlayerAnimationEnum.green_idle;
            }
            else{
                state=PlayerAnimationEnum.green_run;
            }
        }
        if(state==PlayerAnimationEnum.red_run||state==PlayerAnimationEnum.red_idle){
            
            if(playerMovement.currentFloor>1) {
                state=PlayerAnimationEnum.red_idle;
            }
            else{
                state=PlayerAnimationEnum.red_run;
            }
        }
        animator = GetComponent<Animator>();
        animator.Play(state.ToString());
    }

}