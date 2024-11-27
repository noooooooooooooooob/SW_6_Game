using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    public GameObject healthBarSlider;
    private Slider healthBarSliderComponent;
    public ObjectManager objectManager;
    public HealthBarController healthBarController;
    GameManager gameManager;
    public bool isHit;

    void Start()
    {
        animator = GetComponent<Animator>();
        healthBarSliderComponent = healthBarSlider.GetComponent<Slider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectManager = GetComponent<ObjectManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isHit = false;
    }


    // Update is called once per frame
    void Update()
    {
        // if (healthBarSliderComponent.value == 1.0f)
        // {
        //     this.gameObject.SetActive(false);
        // }
        if (isHit)
        {
            isHit = false;
            gameManager.increaseScore();
            isHitAnimation();
        }
    }

    public void BossDeath()
    {
        if (animator != null)
        {
            animator.Play("SlimeDie");
            Invoke("bossActiveFalse", 0.2f);
        }
    }
    void bossActiveFalse()
    {
        gameObject.SetActive(false);
    }
    void isHitAnimation()
    {
        if (animator != null)
        {
            animator.Play("SlimeHit");
            Invoke("GoBack", 0.2f);
        }
        else
        {
            Debug.LogWarning("애니메이션 상태 'SlimeHit'이 존재하지 않습니다.");
        }
    }

    void GoBack()
    {
        animator.Play("SlimeAnimation");
    }
}
