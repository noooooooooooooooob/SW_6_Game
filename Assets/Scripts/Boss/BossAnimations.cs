using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimations : MonoBehaviour
{
    public enum animations
    {
        Idle,
        IsHit,
        Death,
        Walk,
    }
    Animator animator;
    animations currentState;
    Boss boss;

    private bool inAnimation = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        boss = GetComponent<Boss>();

        currentState = animations.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.isDead)
        {
            ChangeAnimationState(animations.Death);
        }
        else if (boss.isHit)
        {
            inAnimation = true;
            ChangeAnimationState(animations.IsHit);
            Invoke("AnimationDelay", 0.2f);
        }
        else if (boss.doWalk)
        {
            ChangeAnimationState(animations.Walk);
        }
        else if (!inAnimation)
        {
            ChangeAnimationState(animations.Idle);
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
