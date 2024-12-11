using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public GameObject panel;
    private ScoreManager scoreManager;
    private SceneManager sceneManager;
    public TMP_Text scoreText;
    public TMP_Text Hit;
    public TMP_Text Miss;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        sceneManager = GameObject.Find("GameManager").GetComponent<SceneManager>();

        GameOver();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sceneManager.LoadScene(0);
            panel.SetActive(false);
        }
    }

    public void GameOver()
    {
        // 게임 오버 스코어 출력
        string String = string.Join(" ", scoreManager.currentScore);
        scoreText.text = String;
        String = string.Join(" ", scoreManager.currentHit);
        Hit.text = String;
        String = string.Join(" ", scoreManager.currentMiss);
        Miss.text = String;

        // 게임 오버 시 초기화
        scoreManager.sortScores();
        scoreManager.currentScore = 0;
        scoreManager.currentHit = 0;
        scoreManager.currentMiss = 0;

        panel.SetActive(true);
    }

    public void GameClear()
    {
        panel.SetActive(true);
    }
}
