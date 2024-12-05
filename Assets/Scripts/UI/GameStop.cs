using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStop : MonoBehaviour
{
    private bool isPaused = false; // 게임이 일시정지 상태인지 여부를 저장
    public GameObject panel;       // UI 패널
    public AudioSource bgmAudio;   // 배경음악 AudioSource

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

    // 게임 일시정지
    public void PauseGame()
    {
        Time.timeScale = 0f; // 게임 시간을 멈춤
        isPaused = true;

        // UI Panel 활성화
        panel.SetActive(true);

        // 배경음악 일시정지
        if (bgmAudio != null && bgmAudio.isPlaying)
        {
            bgmAudio.Pause();
        }

        Debug.Log("Game Paused");
    }

    // 게임 재개
    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 시간을 다시 정상 속도로
        isPaused = false;

        // UI Panel 비활성화
        panel.SetActive(false);

        // 배경음악 재개
        if (bgmAudio != null && !bgmAudio.isPlaying)
        {
            bgmAudio.UnPause();
        }

        Debug.Log("Game Resumed");
    }
}
