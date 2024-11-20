using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject healthBarSlider;
    private Slider healthBarSliderComponent;
    public ObjectManager objectManager;
    public HealthBarController healthBarController;
    GameObject gameManager;
    public bool isHit;
    void Start()
    {
        healthBarSliderComponent = healthBarSlider.GetComponent<Slider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectManager = GetComponent<ObjectManager>();
        gameManager = GameObject.Find("Game manager");
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
            gameManager.GetComponent<GameManager>().increaseScore();
            Deal();
        }
    }

    public void BossDeath()
    {
        gameObject.SetActive(false);
    }
    public void Deal()
    {
        spriteRenderer.color = new Color(1, 0, 0, 1);
        Invoke("restoration", 0.3f);
    }
    void restoration()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

}
