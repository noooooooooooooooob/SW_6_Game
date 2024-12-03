using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite[] numbers; //숫자 스프라이트
    public GameObject[] Timer;

    private float startTime;  // 게임 시작 시간
    public float timeLimit;  // 제한시간
    void Start()
    {
        startTime = Time.time;  // 게임 시작 시간을 현재 시간으로 설정

        int n = (int)timeLimit;
        for (int i = 2; i >= 0; i--)
        {
            int input = n % 10;
            n /= 10;
            if (Timer[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = numbers[input];
            }
        }
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        float timeElapsed = Time.time - startTime;  // 경과 시간 계산
        float timeRemaining = timeLimit - timeElapsed;  // 남은 시간 계산
        int n = (int)timeRemaining;
        for (int i = 2; i >= 0; i--)
        {
            int input = n % 10;
            n /= 10;
            if (Timer[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = numbers[input];
            }
        }
        //if(timeRemaining <= 0) 일시 게임오버
    }

}
