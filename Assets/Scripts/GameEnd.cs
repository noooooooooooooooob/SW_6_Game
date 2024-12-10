using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public GameObject panel;
    public ScoreManager scoreManager;
    public SceneManager sceneManager;
    public TMP_Text scoreText;
    public TMP_Text Hit;
    public TMP_Text Miss;
    bool isClear;
    bool isOver;
    bool hasTriggered;
    
    void Start()
    {
        hasTriggered=false;
        isClear=false;
        isOver=false;
        scoreManager=GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        sceneManager=GameObject.Find("GameManager").GetComponent<SceneManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (isClear || isOver))
        {
            panel.SetActive(false);
            sceneManager.LoadScene(0);
        }
    }

    public void gameOver()
    {
        if(hasTriggered)
            return;
        // 게임 오버 스코어 출력
        string String = string.Join(" ", scoreManager.currentScore);
        scoreText.text = String;
        String = string.Join(" ", scoreManager.currentHit);
        Hit.text = String;
        String = string.Join(" ", scoreManager.currentMiss);
        Miss.text = String;

        // 게임 오버 시 초기화
        scoreManager.sortScores();
        scoreManager.currentScore=0;
        scoreManager.currentHit=0;
        scoreManager.currentMiss=0;

        panel.SetActive(true);
        isOver=true;

        hasTriggered=true;
    }

    public void gameClear()
    {
        panel.SetActive(true);
    }
}
