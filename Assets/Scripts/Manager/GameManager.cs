using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false; // 게임이 일시정지 상태인지 여부를 저장
    private GameObject panel;       // UI 패널
    private AudioSource bgmAudio;   // 배경음악 AudioSource
    public GameObject GamePauseUI;
    public GameObject GameOverUI;

    public bool gameWon = false;

    void Start()
    {
        bgmAudio = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
    }
    void Update()
    {
        // Esc 키 입력 확인
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // 게임 재개
            }
            else
            {
                PauseGame(); // 게임 일시정지
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // 게임 시간을 멈춤
        isPaused = true;
        Instantiate(GamePauseUI);
        if (bgmAudio != null && bgmAudio.isPlaying)
        {
            bgmAudio.Pause();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 시간을 다시 정상 속도로
        panel = GameObject.FindGameObjectWithTag("pauseUI");
        isPaused = false;
        if (bgmAudio != null && !bgmAudio.isPlaying)
        {
            bgmAudio.UnPause();
        }
        {
            bgmAudio.UnPause();
        }
        Destroy(panel);
    }

    public void ShowGameOverUI()
    {
        gameWon = false;
        Instantiate(GameOverUI);
    }
    public void ShowGameClearUI()
    {
        gameWon = true;
        Instantiate(GameOverUI);
    }

}
