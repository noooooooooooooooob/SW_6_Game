using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class Note : MonoBehaviour
{
    Rigidbody2D rigid;
    public float[] spawnPoints;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    int spawnPointYidx;
    float spawnPointY;
    public float spawnPointX;
    public float speed;
    bool isBoardered;
    public bool isHit;
    int arrowidx;
    int coloridx;

    public bool isLeft;
    public bool isRight;
    public bool isUp;
    public bool isDown;

    //기믹들 추가
    public bool isChange;
    public float changeSpeed;
    public bool isOpposite;
    public bool isNotacted;
    public bool isFaded;
    public float op;
    public bool isSame;
    public int playerColor;
    float s;
    float plus;


    // 체력 변화 처리 클래스 연결
    public HealthBarController healthBarController;

    void Awake()
    {
        op=1.0f;
        rigid=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        spawnPointYidx=Random.Range(0,3);
        spawnPointY=spawnPoints[spawnPointYidx]; // 스폰포인트 랜덤
        transform.position=new Vector2(spawnPointX,spawnPointY);

        isChange=false;
        isOpposite=false;
        isNotacted=false;
        isFaded=false;
        isSame=false;
        
        coloridx=Random.Range(0,3);
        arrowidx=Random.Range(0,4);

        isLeft=false;
        isRight=false;
        isUp=false;
        isDown=false;
        playerColor=1;
        s=0;
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
        if(isChange)
        {
            s=Mathf.Asin((transform.position.y)/4);
            Invoke("locateChange",0.8f);
            isChange=false;
        }
        if(isFaded && transform.position.x<=0)
        {
            isFaded=false;
            fadeNodes();
        }
        if(isOpposite)
        {
            switch(arrowidx)
            {
                case 0:
                    isLeft=false;
                    isRight=true;
                    break;
                case 1:
                    isRight=false;
                    isLeft=true;
                    break;
                case 2:
                    isUp=false;
                    isDown=true;
                    break;
                case 3:
                    isDown=false;
                    isUp=true;
                    break;
            }
            isOpposite=false;
        }
        if(isSame)
        {
            //isSame=false;
            sameColor();
        }
        transform.Translate(-1.0f * speed*Time.deltaTime,0,0); // 등속으로 왼쪽으로 이동

    }

    void SetActiveFalseNote()
    {
        gameObject.SetActive(false);
    }

    void locateChange()
    {
        
        transform.position = new Vector2(transform.position.x, 3 * Mathf.Sin(s));
        s+=Mathf.PI/64;
        Invoke("locateChange",0.015625f);
    }

    void fadeNodes()
    {
        spriteRenderer.color=new Color(1,1,1,1.0f*op);
        if(op<=0.0f)
            return;
        op-=0.1f;
        Invoke("fadeNodes",0.1f);
    }

    void sameColor()
    {
        PlayerElement PE = FindObjectOfType<PlayerElement>();
       
        if ( PE!= null)
        {
            playerColor=PE.color;
        }
        else
        {
            Debug.LogWarning("PE스크립트가 할당되지 않았습니다.");
        }
        coloridx=playerColor;
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
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag=="Ground")
        {
            HealthBarController healthBarController = FindObjectOfType<HealthBarController>();
            if(healthBarController!=null && !isNotacted)
            {
                HealthBarController.Instance.TakeDamage();
            }
            else if(healthBarController!=null && isNotacted)
            {
                HealthBarController.Instance.Heal();
            }
            else
            {
                Debug.LogError("HealthBarController not found on Player object.");
            }
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag=="Player")
        {
            HealthBarController healthBarController = FindObjectOfType<HealthBarController>();
            if(healthBarController!=null && !isNotacted)
            {
                HealthBarController.Instance.TakeDamage();
            }
            else if(healthBarController!=null && isNotacted)
            {
                HealthBarController.Instance.Heal();
            }
            else
            {
                Debug.LogError("HealthBarController not found on Player object.");
            }
            gameObject.SetActive(false);
        }
    }
}
