using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarFill;
    
    private float maxHealth = 100f;
    private float currentHealth;
    private bool isDamaging = false;
    private bool isHealing = false;
    private float damageHealRate = 20f;

    void Start()
    {
        currentHealth = maxHealth / 2f;
        UpdateHealthBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartDamage();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            StopDamage();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            StartHeal();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            StopHeal();
        }

        if (isDamaging)
        {
            TakeDamage(damageHealRate * Time.deltaTime);
        }
        else if (isHealing)
        {
            Heal(damageHealRate * Time.deltaTime);
        }
    }

    private void StartDamage()
    {
        isDamaging = true;
        isHealing = false;
    }

    private void StopDamage()
    {
        isDamaging = false;
    }

    private void StartHeal()
    {
        isHealing = true;
        isDamaging = false;
    }

    private void StopHeal()
    {
        isHealing = false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}