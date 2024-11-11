using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public static HealthBarController Instance {get; private set;}
    [SerializeField] private Image healthBarFill;
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float changeRate = 20f;    // 고정 회복량 및 데미지량

    
    private float currentHealth;

    // 체력 비율에 따른 보너스 기준 (10%와 5% 비율로 설정)
    private const float HIGH_THRESHOLD = 0.25f;  // 25%
    private const float LOW_THRESHOLD = 0.1f;  // 10%
private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Multiple instances of HealthBarController detected. Destroying duplicate");
        }
    }
    private void Start() => SetHealth(maxHealth * 0.5f); // 체력을 절반으로 시작

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            TakeDamage(changeRate);
        else if (Input.GetKeyDown(KeyCode.W))
            Heal(changeRate);
    }

    public void Heal(float amount)
    {
        float healthPercentage = GetHealthPercentage();

        // 체력이 낮을 때 회복량 보너스
        if (healthPercentage <= HIGH_THRESHOLD && healthPercentage > LOW_THRESHOLD)
        {
            amount += 20f; // 10%~5% 구간에서 추가 회복량
        }
        else if (healthPercentage <= LOW_THRESHOLD)
        {
            amount += 50f; // 5% 이하에서 최대 추가 회복량
        }

        // 체력을 증가시키고 최대 체력으로 제한
        SetHealth(Mathf.Clamp(currentHealth + amount, 0f, maxHealth));
    }

    public void TakeDamage(float amount)
    {
        float healthPercentage = GetHealthPercentage();

        // 체력이 낮을 때 데미지 감소
        if (healthPercentage <= HIGH_THRESHOLD && healthPercentage > LOW_THRESHOLD)
        {
            amount -= 10f; // 10%~5% 구간에서 데미지 감소
        }
        else if (healthPercentage <= LOW_THRESHOLD)
        {
            amount -= 20f; // 5% 이하에서 최대 데미지 감소
        }

        // 최소 1의 데미지는 들어가도록 보장
        amount = Mathf.Max(amount, 1f);

        // 체력을 감소시키고 0으로 제한
        SetHealth(Mathf.Clamp(currentHealth - amount, 0f, maxHealth));
    }

    private void SetHealth(float value)
    {
        currentHealth = value;
        if(healthBarFill!=null)
        {
        healthBarFill.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogError("healthBarFill is not assigned in the Inspector.");
        }
    }

    // 현재 체력 비율을 반환하는 메서드
    public float GetHealthPercentage() => currentHealth / maxHealth;
}
