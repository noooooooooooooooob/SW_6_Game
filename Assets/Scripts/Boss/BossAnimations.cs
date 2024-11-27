using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimations : MonoBehaviour
{
    public enum animations
    {
        MoveBack,
        IsHit,
        SlimeDie
    }
    Animator animator;
    animations currentState;
    Boss boss;

    private bool inAnimation = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        boss = GetComponent<Boss>();

        currentState = animations.MoveBack;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.isDead)
        {
            ChangeAnimationState(animations.SlimeDie);
        }
        else if (boss.isHit)
        {
            inAnimation = true;
            ChangeAnimationState(animations.IsHit);
            Invoke("AnimationDelay", 0.2f);
        }
        else if (!inAnimation)
        {
            ChangeAnimationState(animations.MoveBack);
        }


    }


    void ChangeAnimationState(animations newState)
    {
        if (currentState == newState) return;

        // Debug.Log("Changing state to: " + newState.ToString());
        animator.Play(newState.ToString());

        currentState = newState;

    }

    void AnimationDelay()
    {
        inAnimation = false;
    }
    public void BossDeath()
    {
        animator.Play("SlimeDie");
    }

}
