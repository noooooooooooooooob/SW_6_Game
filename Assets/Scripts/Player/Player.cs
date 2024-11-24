using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color originalColor; // 플레이어 색을 다시 되돌리는 용도
    public GameObject healthBarSlider;
    private Slider healthBarSliderComponent;

    void Start()
    {
        healthBarSliderComponent = healthBarSlider.GetComponent<Slider>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 원래 색상 저장
    }

    // Update is called once per frame
    void Update()
    {
        // if (healthBarSliderComponent.value == 0.0f)
        // {
        //     this.gameObject.SetActive(false);
        // }
    }

    public void PlayerDeath()
    {
        gameObject.SetActive(false);
    }

    //맞았을 때 깜빡임
    public void hitByNote()
    {
        spriteRenderer.color = new Color(1, 1 ,1 ,0.5f);
        Invoke("restroration",0.18f);
    }

      // 원래 색상으로 복원
    void restroration()
    {
        spriteRenderer.color = originalColor;
    }
}
