using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score; //점수
    int maxscore;
    public int Combo; //콤보
    int maxcombo;
    public Sprite[] numbers; //숫자 스프라이트
    public GameObject[] Scores;
    public GameObject[] Combos;
    public GameObject[] Timer;

    private float startTime;  // 게임 시작 시간
    public float timeLimit;  // 제한시간
    void Start()
    {
        score = 0; // 출력되는 점수
        Combo = 0;
        maxscore = 999999;
        maxcombo = 999;

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

    void updateScore()
    {
        int n = score;
        for (int i = 5; i >= 0; i--)
        {
            int input = n % 10;
            n /= 10;
            if (Scores[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = numbers[input];
            }
        }
    }

    void updateCombo()
    {
        int n = Combo;
        for (int i = 2; i >= 0; i--)
        {
            int input = n % 10;
            n /= 10;
            if (Combos[i].TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = numbers[input];
            }
        }
    }
    public void increaseScore()
    {
        score += 10 + Combo;
        Combo += 1;
        if (score >= maxscore)
            score = maxscore;
        if (Combo >= maxcombo)
            Combo = maxcombo;
        updateScore();
        updateCombo();
    }

    public void decreaseCombo()
    {
        Combo = 0;
        updateCombo();
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
