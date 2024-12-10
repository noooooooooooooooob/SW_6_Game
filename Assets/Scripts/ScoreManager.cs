using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // 싱글톤 패턴을 위한 인스턴스
    public int[] Scores = new int[9];
    public int currentScore = 0;
    public int currentHit=0;
    public int currentMiss=0;
    void Awake()
    {
         if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 오브젝트가 파괴되지 않도록 설정2
    }
    void Start()
    {
        
    }
    public void addScore(int add)
    {
        currentScore+=add;
    }

    public void sortScores()
    {
        if(currentScore<=Scores[8])
            return;
        
        Scores[8]=currentScore;

        // 오름차순 정렬
        System.Array.Sort(Scores);

        // 내림차순으로 뒤집기
        System.Array.Reverse(Scores);
    }
}
