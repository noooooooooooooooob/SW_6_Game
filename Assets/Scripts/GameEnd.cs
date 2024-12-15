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
    private GameManager gameManager;
    public TMP_Text title;
    public TMP_Text scoreText;
    public TMP_Text Hit;
    public TMP_Text Miss;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (gameManager.gameWon)
        {
            title.text = "Game Clear";
        }
        else
        {
            title.text = "Game Over";
        }
        scoreText.text += scoreManager.currentScore;
        Hit.text += scoreManager.currentHit;
        Miss.text += scoreManager.currentMiss;

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
