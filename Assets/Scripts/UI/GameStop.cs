using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStop : MonoBehaviour
{
    private bool isPaused = false; // 게임이 일시정지 상태인지 여부를 저장
    public GameObject panel;

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
        // 추가적으로 UI 표시 등 작업을 여기에 작성
        panel.SetActive(true);
        Debug.Log("Game Paused");
    }

    // 게임 재개
    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 시간을 다시 정상 속도로
        isPaused = false;
        panel.SetActive(false);
        // 추가적으로 UI 숨김 등 작업을 여기에 작성
        Debug.Log("Game Resumed");
    }
}
