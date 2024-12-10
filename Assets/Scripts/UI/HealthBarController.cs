using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // 체력 바 활성화
    public bool HealthBarActive = true;
    // 맞았을 때 효과음
    public AudioClip byHitSound;
    private AudioSource audioSource;
    private ObjectManager objectManager;
    private GameManager gameManager;
    private GameEnd gameEnd;
    private ScoreManager scoreManager;
    public static HealthBarController Instance { get; private set; }
    [SerializeField] private Image healthBarFill;     // 체력바의 Fill 이미지
    Slider healthBarSlider;  // Slider로 변경
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float changeRate = 30f;  // 고정 회복량 및 데미지량
    [SerializeField] private float bonusRate = 40f;
    [SerializeField] private float mininumDamage = 5f;

    //부드러운 체력 변화를 위해
    public float currentHealth;
    private float targetHealth;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Color healColor = new Color(0.5f, 1f, 0.5f); // 밝은 초록색
    [SerializeField] private Color damageColor = new Color(1f, 0f, 0f, 0.5f); // 반투명 빨간색

    private Color defaultColor;
    //
    Player player;
    Boss boss;
    GameTransition gameTransition;

    // 체력 비율에 따른 보너스 기준
    private const float THRESHOLD = 0.3f;  // 30%

    //공격력 상승
    public bool isDamageUp;

    private void Start()
    {
        scoreManager=GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        gameEnd=GameObject.Find("GameManager").GetComponent<GameEnd>();
        gameManager = GameObject.Find("ScoreComboTime").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        healthBarSlider = GetComponent<Slider>();
        objectManager = GameObject.Find("Object manager").GetComponent<ObjectManager>();
        healthBarFill = healthBarSlider.fillRect.GetComponent<Image>();
        defaultColor = healthBarFill.color;
        bonusRate = changeRate * 4 / 7; // 보너스 rate (체력이 일정 수준 떨어졌을 시 체력 증감 값에 추가 되는 값) 을 기본 체력 증감의 4/7로 설정

        player = GameObject.Find("Player").GetComponent<Player>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        gameTransition = GameObject.Find("GameManager").GetComponent<GameTransition>();

        isDamageUp = false;

        SetHealth(maxHealth * 0.5f); // 체력을 절반으로 시작
        // Debug.Log("Current health :" + currentHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0 || currentHealth >= maxHealth)
        {
            inventory IVT = FindObjectOfType<inventory>();
            IVT.clonefin();
            if (currentHealth <= 0)
            {
                targetHealth = 0;
                currentHealth = 0;
                CallPlayerDeath();
                HealthBarActive = false; //이거 하면 다음 레벨로 넘어가질 않음. 왜지??
                Debug.Log("Player Health depleted");
            }
            else if (currentHealth >= maxHealth)
            {
                targetHealth = maxHealth;
                currentHealth = maxHealth;
                CallBossDeath();
                HealthBarActive = false;
                Debug.Log("Boss Health depleted");

            }

        }

        if (Mathf.Abs(currentHealth - targetHealth) > 0.01f)
        {
            currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * smoothSpeed);
            UpdateSlider();
        }


        // 수동 체력 조정
        if (Input.GetKeyDown(KeyCode.O))
            TakeDamage();
        else if (Input.GetKeyDown(KeyCode.P))
            Heal();
    }

    private void CallPlayerDeath()
    {
        Debug.Log("Player is dead");
        player.PlayerDeath();
        objectManager.GameEnd();
        gameManager.isGameOver=true;
        gameEnd.gameOver();
    }

    private void CallBossDeath()
    {

        boss.BossDeath();
        objectManager.GameEnd();
        gameTransition.SetBossDefeated();
        gameManager.isGameClear=true;

    }

    public void Heal()
    {
        if (HealthBarActive)
        {
            float amount = changeRate;
            float healthPercentage = GetHealthPercentage();

            // 체력이 낮을 때 회복량 보너스
            if (healthPercentage <= THRESHOLD)
            {
                amount += bonusRate * (1 - healthPercentage);
            }

            if (isDamageUp)
            {
                amount *= 2f;
            }

            // 목표 체력을 증가시키고 최대 체력으로 제한
            SetTargetHealth(Mathf.Clamp(targetHealth + amount, -1f, maxHealth + 1f));
            StartCoroutine(FlashHealthBar(healColor));

            //Hit 증가
            scoreManager.currentHit++;
        }
    }

    public void TakeDamage()
    {
        if (HealthBarActive)
        {
            float amount = changeRate;
            float healthPercentage = GetHealthPercentage();

            // 체력이 낮을 때 데미지 감소
            if (healthPercentage <= THRESHOLD)
            {
                amount -= bonusRate * (1 - healthPercentage);
            }

            // 최소 1의 데미지는 들어가도록 보장
            amount = Mathf.Max(amount, mininumDamage);

            // 목표 체력을 감소시키고 0으로 제한
            SetTargetHealth(Mathf.Clamp(targetHealth - amount, -1f, maxHealth + 1f));
            StartCoroutine(FlashHealthBar(damageColor));

            // 맞았을 때 효과음
            isHitSound();
            player.hitByNote();

            // Miss 증가
            scoreManager.currentMiss++;
        }
    }

    //체력반전
    public void Healthchange()
    {
         if(HealthBarActive){
             float amount = changeRate;
            float healthPercentage = GetHealthPercentage();

            SetHealth(Mathf.Clamp(maxHealth - currentHealth, -1f, maxHealth + 1f));
        }
    }

    private void SetHealth(float value)
    {
        currentHealth = value;
        targetHealth = value;
        UpdateSlider();
    }
    private void SetTargetHealth(float value)
    {
        targetHealth = value;
    }

    private void UpdateSlider()
    {
        healthBarSlider.value = currentHealth / maxHealth;
    }

    // 현재 체력 비율을 반환하는 메서드
    public float GetHealthPercentage() => currentHealth / maxHealth;

    private IEnumerator FlashHealthBar(Color flashColor)
    {
        healthBarFill.color = flashColor; // 변경된 색상 적용
        yield return new WaitForSeconds(0.2f); // 깜빡임 지속 시간
        healthBarFill.color = defaultColor; // 기본 색상으로 복구
    }

    // 맞았을 때 사운드 함수
    public void isHitSound()
    {
        audioSource.PlayOneShot(byHitSound);
        // Debug.Log("HitAudio");
    }
}
