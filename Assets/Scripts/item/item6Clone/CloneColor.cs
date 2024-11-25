using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloneColor : MonoBehaviour
{
    
    public bool isAttacking = false;


   

    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private Transform playerTransform;
    
    
    //private ReduceGaugebar staminaBar;
    //private Vector2 vecGravity;

   
   public SpriteRenderer spriteRenderer;
    private Color originalColor; // 플레이어 색을 다시 되돌리는 용도
    private ColorEnum playerColor;
    public GameObject healthBarSlider;
    private Slider healthBarSliderComponent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<Collider2D>();
        
        healthBarSliderComponent = healthBarSlider.GetComponent<Slider>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // 원래 색상 저장
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
