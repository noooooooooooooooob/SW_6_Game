using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyColor : MonoBehaviour
{
     Sprite[] sprites;
     public float minX = -13f;
    public float maxX = 13f;
    public float minY = -5f;
    public float maxY = 5f;
    int playerColor;
    public void sameColor(){
        ObjectManager OM = FindObjectOfType<ObjectManager>();
       
        if ( OM!= null)
        {
            Debug.Log("OM실행");
            OM.unifyNote=true;
        }
        else
        {
            Debug.LogWarning("OM스크립트가 할당되지 않았습니다.");
        }

        PlayerElement PE = FindObjectOfType<PlayerElement>();
       
        if ( PE!= null)
        {
            playerColor=PE.color;
        }
        else
        {
            Debug.LogWarning("PE스크립트가 할당되지 않았습니다.");
        }
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
                    sprites=noteComponent.sprites;
                    
                }
                else
                {
                    Debug.LogWarning("Note 컴포넌트가 없는 오브젝트 ");
                }
                
                
            }
             
            
               
        }
        
    }
}
