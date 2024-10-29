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
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        spawnPointY=spawnPoints[Random.Range(0,3)]; // 스폰포인트 랜덤
        transform.position=new Vector2(spawnPointX,spawnPointY);
        int coloridx=Random.Range(0,3);
        switch(coloridx)
        {
            case 0:
                spriteRenderer.color=new Color(1,0,0,1);
                break;
            case 1:
                spriteRenderer.color=new Color(0,1,0,1);
                break;
            case 2:
                spriteRenderer.color=new Color(0,0,1,1);
                break;
        }
    }

    void Update()
    {
        transform.Translate(-0.01f * speed,0,0); // 등속으로 왼쪽으로 이동
    }


    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag=="Ground"
           || collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
    }
}
