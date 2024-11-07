using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bomb : MonoBehaviour
{
     public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    // 삭제된 Note 오브젝트 개수를 저장할 변수
    private int deleteCount = 0;

    // 이 메서드는 외부에서 호출되어 Note 오브젝트를 삭제합니다
    public void InvokeDeleteNotes()
    {
        deleteCount = 0;  // 삭제된 Note 개수 초기화

         // 'Note' 태그가 붙은 모든 오브젝트 찾기
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");

        foreach (GameObject note in notes)
        {
            Vector3 pos = note.transform.position;

            // x와 y 범위 안에 있는 오브젝트만 삭제
            if (pos.x >= minX && pos.x <= maxX && pos.y >= minY && pos.y <= maxY)
            {
                Destroy(note);
                deleteCount++;
            }
        }

        // 삭제된 Note 개수 출력
        Debug.Log("Deleted Notes Count: " + deleteCount);
    }

}
