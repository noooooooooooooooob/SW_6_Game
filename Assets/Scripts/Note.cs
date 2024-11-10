using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class Note : MonoBehaviour
{
    Rigidbody2D rigid;
    public float[] spawnPoints;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    float spawnPointY;
    public float spawnPointX;
    public float speed;
    bool isBoardered;
    bool isHit;
    public float prefabIdx; // Object Manager 안에서 배열 인덱스

    public bool isLeft;
    public bool isRight;
    public bool isUp;
    public bool isDown;

    void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        spawnPointY=spawnPoints[Random.Range(0,3)]; // 스폰포인트 랜덤
        transform.position=new Vector2(spawnPointX,spawnPointY);

        int coloridx=Random.Range(0,3);
        int arrowidx=Random.Range(0,4);

        isLeft=false;
        isRight=false;
        isUp=false;
        isDown=false;
        switch(arrowidx)
        {
            case 0:
                isLeft=true;
                break;
            case 1:
                isRight=true;
                break;
            case 2:
                isUp=true;
                break;
            case 3:
                isDown=true;
                break;
        }
        
        //coloridx [ Red, Green, Blue ]
        //arrowidx [ Left, Right, Up, Down]
        if(coloridx==0&&arrowidx==0)
            spriteRenderer.sprite=sprites[0];
        else if(coloridx==0&&arrowidx==1)
            spriteRenderer.sprite=sprites[1];
        else if(coloridx==0&&arrowidx==2)
            spriteRenderer.sprite=sprites[2];
        else if(coloridx==0&&arrowidx==3)
            spriteRenderer.sprite=sprites[3];
        else if(coloridx==1&&arrowidx==0)
            spriteRenderer.sprite=sprites[4];
        else if(coloridx==1&&arrowidx==1)
            spriteRenderer.sprite=sprites[5];
        else if(coloridx==1&&arrowidx==2)
            spriteRenderer.sprite=sprites[6];
        else if(coloridx==1&&arrowidx==3)
            spriteRenderer.sprite=sprites[7];
        else if(coloridx==2&&arrowidx==0)
            spriteRenderer.sprite=sprites[8];
        else if(coloridx==2&&arrowidx==1)
            spriteRenderer.sprite=sprites[9];
        else if(coloridx==2&&arrowidx==2)
            spriteRenderer.sprite=sprites[10];
        else if(coloridx==2&&arrowidx==3)
            spriteRenderer.sprite=sprites[11];
        
        isHit=false;
    }

    void Update()
    {
        if(!isHit)
            transform.Translate(-1.0f * speed*Time.deltaTime,0,0); // 등속으로 왼쪽으로 이동
        else
        {
            
        }

    }


    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag=="Ground"
           || collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
        /*
        if(collision.gameObject.tag=="Ground")
        {
            isHit=false;
        }
        if(collision.gameObject.tag=="Player")
        {
            isHit=true;
        }
        */
    }
}
