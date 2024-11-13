using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public enum animations{
        blue_run,
        blue_attack_side,
        blue_attack_up,
        blue_attack_down,
        red_run,
        red_attack_side,
        red_attack_up,
        red_attack_down,
        green_run,
        green_attack_side,
        green_attack_up,
        green_attack_down
    }

    public float attackDelay = 0.3f;

    PlayerMovement playerMovement;
    PlayerElement playerElement;

    Animator animator;
    animations currentState;

    public bool isAttacking = false;
    public bool pressedAttack = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();        
        playerElement = GetComponent<PlayerElement>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
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

        if (pressedAttack)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Invoke("AttackComplete", attackDelay);
            }
        }
        else
        {
            ChangeAnimationState(GetRunAnimation());
        }
    }

    animations GetAttackAnimation(string direction)
    {
        if (playerElement.red)
        {
            if (direction == "up") return animations.red_attack_up;
            if (direction == "down") return animations.red_attack_down;
            return animations.red_attack_side;
        }
        else if (playerElement.blue)
        {
            if (direction == "up") return animations.blue_attack_up;
            if (direction == "down") return animations.blue_attack_down;
            return animations.blue_attack_side;
        }
        else if (playerElement.green)
        {
            if (direction == "up") return animations.green_attack_up;
            if (direction == "down") return animations.green_attack_down;
            return animations.green_attack_side;
        }
        return animations.blue_run; // default case
    }

    animations GetRunAnimation()
    {
        if (playerElement.red) return animations.red_run;
        if (playerElement.blue) return animations.blue_run;
        if (playerElement.green) return animations.green_run;
        return animations.blue_run; // default case
    }

    void HandleAnimationState()
    {
    }

    void AttackComplete()
    {
        isAttacking = false;
        pressedAttack = false;
    }

    void ChangeAnimationState(animations newState)
    {
       if(currentState == newState) return;

        Debug.Log("Changing state to: " + newState.ToString());
        animator.Play(newState.ToString());

        currentState = newState;

    }
}
