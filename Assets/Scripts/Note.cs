using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Note : MonoBehaviour
{
    public GameObject player;
    GameObject Boss;
    GameManager gameManager;
    Rigidbody2D rigid;
    public float[] spawnPoints;
    public float[] spawnPointschangelocate;
    public Sprite[] sprites;
    public Sprite[] notActedNodes;
    public Sprite[] oppositeNodes;
    SpriteRenderer spriteRenderer;
    int spawnPointYidx;
    float spawnPointY;
    public float spawnPointX;
    public float speed;
    bool isBoardered;
    public bool isHit;
    int arrowidx;
    int coloridx;



    public ColorEnum noteColor;
    public bool isRed;
    public bool isGreen;
    public bool isBlue;
    public ArrowDirectionEnum noteArrowDirection;
    public bool isLeft;
    public bool isRight;
    public bool isUp;
    public bool isDown;

    //기믹들 추가
    public float changeSpeed;
    public bool isOpposite;
    public bool isNotacted;
    public bool isFaded;
    public float op;
    public bool isSame;
    //public int playerColor;
    ColorEnum playerColor;

    float s;
    float plus;
    // 플레이어 보스 위치 변수
    private Vector2 bossPosition;
    private bool isMovingToBoss = false;
    private Vector2 playerPosition;
    private bool isMovingToPlayer = false;



    // 체력 변화 처리 클래스 연결
    public HealthBarController healthBarController;
    private ReduceGaugebar staminaBar;

    void Awake()
    {
        
        healthBarController = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<ReduceGaugebar>();
        op = 1.0f;
        Boss = GameObject.Find("Boss");
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 스폰 위치 설정
        spawnPointYidx = Random.Range(0, 3);
        spawnPointY = spawnPoints[spawnPointYidx]; // 스폰포인트 랜덤

        // 보스가 활성화되어 있으면 보스 위치에서 시작
        if (Boss != null && Boss.activeInHierarchy)
        {
            transform.position = Boss.transform.position; // 보스 위치에서 시작
        }
        else
        {
            Debug.LogWarning("Boss is not active or missing. Default spawn point will be used.");
            transform.position = new Vector2(-10f, spawnPointY); // 기본 위치 설정
        }

        isOpposite = false;
        isNotacted = false;
        isFaded = false;
        isSame = false;

        coloridx = Random.Range(0, 3);
        arrowidx = Random.Range(0, 4);

        coloridx = Random.Range(0, 3);
        arrowidx = Random.Range(0, 4);

        isRed = false;
        isGreen = false;
        isBlue = false;
        switch (coloridx)
        {
            case 0:
                noteColor = ColorEnum.red;
                isRed = true;
                break;
            case 1:
                noteColor = ColorEnum.green;
                isGreen = true;
                break;
            case 2:
                noteColor = ColorEnum.blue;
                isBlue = true;
                break;
        }

        isLeft = false;
        isRight = false;
        isUp = false;
        isDown = false;
        //playerColor = 1;
        s = 0;
        switch (arrowidx)
        {
            case 0:
                noteArrowDirection = ArrowDirectionEnum.left;
                isLeft = true;
                break;
            case 1:
                noteArrowDirection = ArrowDirectionEnum.right;
                isRight = true;
                break;
            case 2:
                noteArrowDirection = ArrowDirectionEnum.up;
                isUp = true;
                break;
            case 3:
                noteArrowDirection = ArrowDirectionEnum.down;
                isDown = true;
                break;
        }

        //coloridx [ Red, Green, Blue ]
        //arrowidx [ Left, Right, Up, Down]

        if (coloridx == 0 && arrowidx == 0)
            spriteRenderer.sprite = sprites[0];
        else if (coloridx == 0 && arrowidx == 1)
            spriteRenderer.sprite = sprites[1];
        else if (coloridx == 0 && arrowidx == 2)
            spriteRenderer.sprite = sprites[2];
        else if (coloridx == 0 && arrowidx == 3)
            spriteRenderer.sprite = sprites[3];
        else if (coloridx == 1 && arrowidx == 0)
            spriteRenderer.sprite = sprites[4];
        else if (coloridx == 1 && arrowidx == 1)
            spriteRenderer.sprite = sprites[5];
        else if (coloridx == 1 && arrowidx == 2)
            spriteRenderer.sprite = sprites[6];
        else if (coloridx == 1 && arrowidx == 3)
            spriteRenderer.sprite = sprites[7];
        else if (coloridx == 2 && arrowidx == 0)
            spriteRenderer.sprite = sprites[8];
        else if (coloridx == 2 && arrowidx == 1)
            spriteRenderer.sprite = sprites[9];
        else if (coloridx == 2 && arrowidx == 2)
            spriteRenderer.sprite = sprites[10];
        else if (coloridx == 2 && arrowidx == 3)
            spriteRenderer.sprite = sprites[11];

        isHit = false;
    }

    IEnumerator MoveToSpawnPointY()
    {
        float duration = 0.5f; // 이동 시간
        float elapsed = 0.0f;

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector2(startPosition.x, spawnPointY);

        while (elapsed < duration)
        {
            // 부드럽게 Y축 위치로 이동
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(startPosition.y, targetPosition.y, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Y축 최종 위치 보정
        transform.position = new Vector2(transform.position.x, spawnPointY);
    }

    void Start()
    {
        StartCoroutine(MoveToSpawnPointY());

        var bossObj = FindObjectOfType<Boss>();
        var playerObj = FindObjectOfType<Player>();

        if (bossObj != null && bossObj.gameObject.activeInHierarchy)
        {
            bossPosition = bossObj.transform.position;
            transform.position = new Vector2(bossObj.transform.position.x, bossObj.transform.position.y);
        }
        else
        {
            Debug.LogWarning("Boss is not active or missing.");
        }

        if (playerObj != null && playerObj.gameObject.activeInHierarchy)
        {
            playerPosition = playerObj.transform.position;
        }
        else
        {
            Debug.LogWarning("Player is not active or missing.");
        }
    }

    public void updatePlayerPosition()
    {
        var playerObj = FindObjectOfType<Player>();

        if (playerObj != null && playerObj.gameObject.activeInHierarchy)
        {
            playerPosition = playerObj.transform.position;
        }
        else
        {
            //Debug.LogWarning("Player is not active or missing.");
        }
    }



    void Update()
    {
        updatePlayerPosition();
        if (isMovingToBoss) //현재 플레이어와 충돌하면
        {
            MoveToBoss();
        }
        else if (isMovingToPlayer)
        {
            MoveToPlayer();
        }
        else
        {
            if (isFaded && transform.position.x <= 8.0f)
            {
                isFaded = false;
                fadeNodes();
            }
            if (isOpposite)
            {
                spriteRenderer.sprite = oppositeNodes[coloridx * 4 + arrowidx];
                switch (arrowidx)
                {
                    case 0:
                        isLeft = false;
                        isRight = true;
                        break;
                    case 1:
                        isRight = false;
                        isLeft = true;
                        break;
                    case 2:
                        isUp = false;
                        isDown = true;
                        break;
                    case 3:
                        isDown = false;
                        isUp = true;
                        break;
                }
                isOpposite = false;
            }

            if (isSame)
            {
                //isSame = false;
                sameColor();
            }


            if (isNotacted)
            {
                if (arrowidx == 0)
                    spriteRenderer.sprite = notActedNodes[0];
                else if (arrowidx == 1)
                    spriteRenderer.sprite = notActedNodes[1];
                else if (arrowidx == 2)
                    spriteRenderer.sprite = notActedNodes[2];
                else if (arrowidx == 3)
                    spriteRenderer.sprite = notActedNodes[3];
            }
            transform.Translate(-1.0f * speed * Time.deltaTime, 0, 0); // 등속으로 왼쪽으로 이동
        }

    }

    void SetActiveFalseNote()
    {
        gameObject.SetActive(false);
    }

    void fadeNodes()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1.0f * op);
        if (op <= 0.0f)
        {
            inFadeNodes();
            return;
        }
        op -= 0.1f;
        Invoke("fadeNodes", 0.08f);
    }

    void inFadeNodes()
    {
        if (op >= 1.0f)
            return;
        if (transform.position.x <= -0.5f && op <= 1.0f)
        {
            op += 0.1f;
            spriteRenderer.color = new Color(1, 1, 1, 1.0f * op);
        }

        Invoke("inFadeNodes", 0.08f);
    }

    void sameColor()
    {
        PlayerElement PE = FindObjectOfType<PlayerElement>();

        if (PE != null)
        {
            playerColor = PE.playerCurrentElement;

        }
        else
        {
            Debug.LogWarning("PE스크립트가 할당되지 않았습니다.");
        }
        //coloridx = playerColor;

        if(playerColor==ColorEnum.red){
            noteColor=ColorEnum.red;
            if(arrowidx==0)
                spriteRenderer.sprite = sprites[0];
             else if(arrowidx==1)
                spriteRenderer.sprite = sprites[1];
            else if(arrowidx==2)
                spriteRenderer.sprite = sprites[2];
            else if(arrowidx==3)
                spriteRenderer.sprite = sprites[3]; 
        }
        if(playerColor==ColorEnum.green){
            noteColor=ColorEnum.green;
            if(arrowidx==0)
                spriteRenderer.sprite = sprites[4];
             else if(arrowidx==1)
                spriteRenderer.sprite = sprites[5];
            else if(arrowidx==2)
                spriteRenderer.sprite = sprites[6];
            else if(arrowidx==3)
                spriteRenderer.sprite = sprites[7]; 
        }
        if(playerColor==ColorEnum.blue){
            noteColor=ColorEnum.blue;
            if(arrowidx==0)
                spriteRenderer.sprite = sprites[8];
             else if(arrowidx==1)
                spriteRenderer.sprite = sprites[9];
            else if(arrowidx==2)
                spriteRenderer.sprite = sprites[10];
            else if(arrowidx==3)
                spriteRenderer.sprite = sprites[11]; 
        }
    }
    void difcolor()
    {


    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player"||collision.gameObject.tag=="clone")
        {   // 노트가 플레이어와 충돌 시 데미지를 입음
            if (!isNotacted)
            {
                damagePlayer();
            }
            else if (isNotacted) // 때리면 안되는 노트와 닿으면 회복
            {
                healPlayer();
            }
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Boss" && isHit)
        {
            Debug.Log("노트 부딪힘");
        }
    }

    void damagePlayer()
    {
        // HealthBarController healthBarController = FindObjectOfType<HealthBarController>();
        if (healthBarController != null)
        {
            gameManager.decreaseCombo();
            healthBarController.TakeDamage();
        }
        else
        {
            Debug.LogError("HealthBarController not found on Player object.");
        }
    }
    void healPlayer()
    {
        // HealthBarController healthBarController = FindObjectOfType<HealthBarController>();
        if (healthBarController != null)
        {
            healthBarController.Heal();
        }
        else
        {
            Debug.LogError("HealthBarController not found on Player object.");
        }
    }
    public void StartMovingToBoss()
    {
        isMovingToBoss = true; // 보스 방향 이동 시작
    }

    void MoveToBoss()
    {
        var bossObj = FindObjectOfType<Boss>();

        if (bossObj == null || !bossObj.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Boss is not active or missing.");
            isMovingToBoss = false; // 이동 중지
            return;
        }
        bossPosition = bossObj.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, bossPosition, 4 * speed * Time.deltaTime);

        // 보스에 도달한 경우
        if (Vector2.Distance(transform.position, bossPosition) < 0.1f)
        {
            healthBarController.Heal(); // 플레이어 체력 회복
            Boss.GetComponent<Boss>().isHit = true;
            staminaBar.IncreaseGaugeBySmallAmount();
            gameObject.SetActive(false);
        }
    }

    public void StartMovingToPlayer()
    {
        isMovingToPlayer = true;
    }
    void MoveToPlayer()
    {
        var playerObj = FindObjectOfType<Player>();

        if (playerObj == null || !playerObj.gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Player is not active or missing.");
            isMovingToPlayer = false; // 이동 중지
            return;
        }

        playerPosition = playerObj.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, 4 * speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerPosition) < 0.1f)
        {
            gameObject.SetActive(false);
        }
    }

    
}
