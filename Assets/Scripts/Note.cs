using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float[] spawnPoints;
    float spawnPointY;
    public float spawnPointX;
    public float speed;
    bool isBoardered;

    void Awake()
    {
        spawnPointY=spawnPoints[Random.Range(0,3)]; // 스폰포인트 랜덤
        transform.position=new Vector2(spawnPointX,spawnPointY);
    }

    void Update()
    {
        transform.Translate(-0.01f * speed,0,0); // 등속으로 왼쪽으로 이동
        //if(isBoardered)
        //    Destroy(gameObject); <<<- 이거 외 안됨?
    
        if(transform.position.x<-13.0f)
            Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isBoardered=true;
        }
    }
}
