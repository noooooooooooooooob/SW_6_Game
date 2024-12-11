using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSlow : MonoBehaviour
{
    /*
    public float minX = -13f;
    public float maxX = 13f;
    public float minY = -5f;
    public float maxY = 5f;
    public void Normalization()
    {
        Debug.LogWarning("5초 후에 실행되었습니다!");
        ObjectManager OM = FindObjectOfType<ObjectManager>();

        if (OM != null)
        {
            OM.isSlow = false;
        }
        else
        {
            Debug.LogWarning("스크립트가 할당되지 않았습니다.");
        }
        //if ( NOTE== null)
        //{
        //NOTE.speed = originNoteSpeed;  // speed 값을 반으로 줄임
        //}
        //else
        //{
        //Debug.LogWarning("스크립트가 할당되지 않았습니다.");
        //}

        // x와 y 범위 안에 있는 오브젝트만 느리게 만들기
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
        foreach (GameObject note in notes)
        {
            Vector3 pos = note.transform.position;

            // x와 y 범위 안에 있는 오브젝트만 느리게 만들기
            if (pos.x >= minX && pos.x <= maxX && pos.y >= minY && pos.y <= maxY)
            {
                // Note 컴포넌트를 가져옴
                Note noteComponent = note.GetComponent<Note>();

                if (noteComponent != null)
                {
                    noteComponent.speed *= 2;  // speed 값을 반으로 줄임
                }
                else
                {
                    Debug.LogWarning("Note 컴포넌트가 없는 오브젝트: " + note.name);
                }


            }
        }


    }
    */
    public void Normalization(){
        float speed=1f;
        Time.timeScale = speed; // 게임 전체 시간의 속도를 변경
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // 물리 업데이트 속도도 조정
        GameObject backgroundMusicOB = GameObject.Find("BackGroundMusic");
        
         AudioSource backgroundMusic=backgroundMusicOB.GetComponent<AudioSource>();
        backgroundMusic.pitch = speed;
    }
}
