using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Game is exiting..."); // 디버그 메시지 (에디터에서 확인용)
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 용
        //Application.Quit(); // 게임 종료
    }
}
