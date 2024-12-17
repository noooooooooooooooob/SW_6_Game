using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public bool isHit;
    public bool isDead;
    public bool doWalk;
    public bool doEntrance;
    public bool doExit;

    public AnimationCurve EntranceCurve;
    private MoveToLocation moveToLocation;
    private Transform bossStartLocation;
    private Transform bossEndLocation;
    void Start()
    {
        isHit = false;
        isDead = false;

        bossStartLocation = GameObject.Find("BossStartLocation").GetComponent<Transform>();
        moveToLocation = GetComponent<MoveToLocation>();

        if (doEntrance)
        {
            Invoke("StopMoving", 1f);
            StartCoroutine(moveToLocation.StartMoving(bossStartLocation.position, 1f, EntranceCurve));
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            isHit = false;
        }
    }

    void StopMoving()
    {
        doWalk = false;
    }

    public void BossDeath()
    {
        if (doExit)
        {
            bossEndLocation = GameObject.Find("BossEndLocation").GetComponent<Transform>();
            StartCoroutine(moveToLocation.StartMoving(bossEndLocation.position, 1f, EntranceCurve));
        }
        else
        {
            isDead = true;
            Invoke("bossActiveFalse", 1f);
        }

    }
    void bossActiveFalse()
    {
        gameObject.SetActive(false);
    }


}
