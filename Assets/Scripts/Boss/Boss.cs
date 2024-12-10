using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public bool isHit;
    public bool isDead;
    public bool doEntrance;

    public AnimationCurve EntranceCurve;
    private MoveToLocation moveToLocation;
    private Transform bossStartLocation;
    void Start()
    {
        isHit = false;
        isDead = false;

        bossStartLocation = GameObject.Find("BossStartLocation").GetComponent<Transform>();
        moveToLocation = GetComponent<MoveToLocation>();

        if (doEntrance)
        {
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

    public void BossDeath()
    {
        isDead = true;
        Invoke("bossActiveFalse", 1f);

    }
    void bossActiveFalse()
    {
        gameObject.SetActive(false);
    }


}
