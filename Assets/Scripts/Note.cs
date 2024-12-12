using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Note : MonoBehaviour
{
    private AudioSource AudioSource;   // AudioSource 컴포넌트
    public AudioClip noteHitSound;       // 노트 치는 효과음 파일
    public GameObject player;
    GameObject Boss;
    public float[] spawnPoints;
    public float[] spawnPointschangelocate;
    public Sprite[] sprites;
    public Sprite[] oppositeNodes;
    SpriteRenderer spriteRenderer;
    public int spawnLine;
    float spawnPointYInWorld;
    public float speed;
    public bool isHit;
    public int arrowidx;
    public int coloridx;

    public ColorEnum noteColor;
    public ArrowDirectionEnum noteArrowDirection;

    //기믹들 추가
    public bool isOpposite;
    public bool isFaded;
    float noteditance; // fade용 float 변수
    public float opacity;
    public bool isSame;
    //public int playerColor;
    ColorEnum playerColor;

    // 플레이어 보스 위치 변수
    private Player playerObj;
    private Boss bossObj;
    private Vector2 bossPosition;
    private bool isMovingToBoss = false;
    private Vector2 playerPosition;
    private bool isMovingToPlayer = false;

    // 체력 변화 처리 클래스 연결
    public HealthBarController healthBarController;
    private ReduceGaugebar staminaBar;

    float timer = 0.0f;
    void Awake()
    {
        //External components
        healthBarController = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<ReduceGaugebar>();
        Boss = GameObject.Find("Boss");

        //Local components
        AudioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        opacity = 1.0f;

        // 보스가 활성화되어 있으면 보스 위치에서 시작
        if (Boss != null && Boss.activeInHierarchy)
        {
            transform.position = Boss.transform.position; // 보스 위치에서 시작
        }
        else
        {
            Debug.LogWarning("Boss is not active or missing. Default spawn point will be used.");
            transform.position = new Vector2(-10f, spawnPointYInWorld); // 기본 위치 설정
        }

        isOpposite = false;
        isFaded = false;
        isSame = false;

        // coloridx = Random.Range(0, 3);
        // arrowidx = Random.Range(0, 4);


        isHit = false;
    }

    void SetNoteColorDirection()
    {
        switch (coloridx)
        {
            case 0:
                noteColor = ColorEnum.red;
                break;
            case 1:
                noteColor = ColorEnum.green;
                break;
            case 2:
                noteColor = ColorEnum.blue;
                break;
        }

        switch (arrowidx) // 0 : left, 1 : right ,2 : up, 3 : down
        {
            case 0:
                noteArrowDirection = ArrowDirectionEnum.left;
                break;
            case 1:
                noteArrowDirection = ArrowDirectionEnum.right;
                break;
            case 2:
                noteArrowDirection = ArrowDirectionEnum.up;
                break;
            case 3:
                noteArrowDirection = ArrowDirectionEnum.down;
                break;
        }

        spriteRenderer.sprite = sprites[coloridx * 4 + arrowidx];
    }

    IEnumerator MoveToSpawnPointY()
    {
        float duration = 0.5f; // 이동 시간
        float elapsed = 0.0f;

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector2(startPosition.x, spawnPointYInWorld);

        while (elapsed < duration)
        {
            // 부드럽게 Y축 위치로 이동
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(startPosition.y, targetPosition.y, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Y축 최종 위치 보정
        transform.position = new Vector2(transform.position.x, spawnPointYInWorld);
    }

    void Start()
    {
        // 스폰 위치 설정
        spawnPointYInWorld = spawnPoints[spawnLine]; // 스폰포인트 랜덤
        bossObj = FindObjectOfType<Boss>();
        playerObj = FindObjectOfType<Player>();

        transform.position = bossObj.transform.position;

        SetNoteColorDirection();
        StartCoroutine(MoveToSpawnPointY());

    }

    public void updatePlayerPosition()
    {
        // var playerObj = FindObjectOfType<Player>();

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
        timer += Time.deltaTime;
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
            if (isFaded)
            {
                fadeNodes();
            }
            if (isOpposite)
            {
                if (arrowidx % 2 == 0)
                {
                    arrowidx += 1;
                }
                else
                {
                    arrowidx -= 1;
                }
                SetNoteColorDirection();
                isOpposite = false;
            }

            if (isSame)
            {
                sameColor();
            }
            transform.Translate(-1.0f * speed * Time.deltaTime, 0, 0); // 등속으로 왼쪽으로 이동
        }

    }

    void SetActiveFalseNote()
    {
        gameObject.SetActive(false);
    }

    //-5.5 attack range 7 start postition 0.75 middle
    void fadeNodes()
    {
        /*
        if(transform.position.x<=2.0f && transform.position.x >= -0.5f)
            noteditance=0;
        else
        {
            if(transform.position.x>2.0f)
                noteditance=transform.position.x-2.0f;
            else
                noteditance=transform.position.x+0.5f;
        }
        opacity = Mathf.Abs(noteditance / 5.0f);
        spriteRenderer.color = new Color(1, 1, 1, 1.0f * opacity);
        */

        noteditance = Mathf.Clamp(transform.position.x, -0.5f, 2.0f) - transform.position.x;
        opacity = Mathf.Abs(noteditance / 5.0f);
        spriteRenderer.color = new Color(1, 1, 1, opacity);
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

        if (playerColor == ColorEnum.red)
        {
            noteColor = ColorEnum.red;
            coloridx = 0;
            spriteRenderer.sprite = sprites[coloridx * 4 + arrowidx];
        }
        if (playerColor == ColorEnum.green)
        {
            noteColor = ColorEnum.green;
            coloridx = 1;
            spriteRenderer.sprite = sprites[coloridx * 4 + arrowidx];
        }
        if (playerColor == ColorEnum.blue)
        {
            noteColor = ColorEnum.blue;
            coloridx = 2;
            spriteRenderer.sprite = sprites[coloridx * 4 + arrowidx];
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackRange")
        {
            Debug.Log("Time : " + timer);
        }
        if (collision.gameObject.tag == "Player")//|| collision.gameObject.tag == "clone"
        {   
            // 노트가 플레이어와 충돌 시 데미지를 입음
            damagePlayer();
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Boss" && isHit)
        {
            Debug.Log("노트 부딪힘");
        }
    }

    void damagePlayer()
    {
        if (healthBarController != null)
        {
            healthBarController.TakeDamage();
        }
        else
        {
            Debug.LogError("HealthBarController not found on Player object.");
        }
    }
    void healPlayer()
    {
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

    public void playNoteHitSound()
    {
        AudioSource.PlayOneShot(noteHitSound);
        // Debug.Log("노트 맞음");
    }
}
