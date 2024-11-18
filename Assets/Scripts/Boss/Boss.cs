using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject healthBarSlider;
    private Slider healthBarSliderComponent;

    public HealthBarController healthBarController;
    void Start()
    {
        healthBarSliderComponent = healthBarSlider.GetComponent<Slider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarSliderComponent.value == 1.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
    void Deal()
    { 
        spriteRenderer.color = new Color(1, 0, 0, 1);
        Invoke("restoration", 0.3f);
    }
    void restoration()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    { // 노트랑 부딪히면 딜 함수 호출
        Note noteScript = collision.GetComponent<Note>();
        if (noteScript != null && collision.gameObject.tag == "Note")
        {
            if (noteScript.isHit)
                Deal();
        }
    }
}
