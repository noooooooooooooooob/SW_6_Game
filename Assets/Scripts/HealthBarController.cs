using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public static HealthBarController Instance { get; private set; }
    Slider healthBarSlider;  // Slider로 변경
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float changeRate = 70f;  // 고정 회복량 및 데미지량
    [SerializeField] private float mininumDamage = 5f;

    Player player;
    Boss boss;
    GameTransition gameTransition;
    public float currentHealth;

    // 체력 비율에 따른 보너스 기준
    private const float HIGH_THRESHOLD = 0.3f;  // 30%
    private const float LOW_THRESHOLD = 0.1f;   // 10%

    //공격력 상승
    public bool isDamageUp;
    
    

    private void Start()
    {
        healthBarSlider = GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<Player>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        gameTransition = GameObject.Find("GameManager").GetComponent<GameTransition>();
        isDamageUp = false;
        
        SetHealth(maxHealth * 0.5f); // 체력을 절반으로 시작
        Debug.Log("Current health :" + currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            TakeDamage();
        else if (Input.GetKeyDown(KeyCode.P))
            Heal();

        if (currentHealth <= 0)
        {
            CallPlayerDeath();
        }
        else if (currentHealth >= maxHealth)
        {
            CallBossDeath();
        }
    }

    private void CallPlayerDeath()
    {
        Debug.Log("Player is dead");
        player.PlayerDeath();
    }

    private void CallBossDeath()
    {
        boss.BossDeath();
        gameTransition.SetBossDefeated();

    }

    public void Heal()
    {
        float amount = changeRate;
        float healthPercentage = GetHealthPercentage();

        // 체력이 낮을 때 회복량 보너스
        if (healthPercentage <= HIGH_THRESHOLD && healthPercentage > LOW_THRESHOLD)
        {
            amount += 50f * (1 - healthPercentage);
        }
        else if (healthPercentage <= LOW_THRESHOLD)
        {
            amount += 40f * (1 - healthPercentage);
        }

        if (isDamageUp)
        {
            amount *= 5f;
        }

        // 체력을 증가시키고 최대 체력으로 제한
        SetHealth(Mathf.Clamp(currentHealth + amount, 0f, maxHealth));
    }

    public void TakeDamage()
    {
        float amount = changeRate;
        float healthPercentage = GetHealthPercentage();

        // 체력이 낮을 때 데미지 감소
        if (healthPercentage <= HIGH_THRESHOLD && healthPercentage > LOW_THRESHOLD)
        {
            amount -= 25f * (1 - healthPercentage);
        }
        else if (healthPercentage <= LOW_THRESHOLD)
        {
            amount -= 40f * (1 - healthPercentage);
        }

        // 최소 1의 데미지는 들어가도록 보장
        amount = Mathf.Max(amount, mininumDamage);

        // 체력을 감소시키고 0으로 제한
        SetHealth(Mathf.Clamp(currentHealth - amount, 0f, maxHealth));
    }

    //체력반전
    public void Healthchange(){
        float amount = changeRate;
        float healthPercentage = GetHealthPercentage();

        SetHealth(Mathf.Clamp(maxHealth-currentHealth, 0f, maxHealth));
    }

    private void SetHealth(float value)
    {
        currentHealth = value;
        healthBarSlider.value = currentHealth / maxHealth;
    }

    // 현재 체력 비율을 반환하는 메서드
    public float GetHealthPercentage() => currentHealth / maxHealth;
}
