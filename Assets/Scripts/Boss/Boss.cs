using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    BossAnimations bossAnimations;
    SpriteRenderer spriteRenderer;
    public GameObject healthBarSlider;
    private Slider healthBarSliderComponent;
    public ObjectManager objectManager;
    public HealthBarController healthBarController;
    GameManager gameManager;
    public bool isHit;


    public AnimationCurve EntranceCurve;
    private MoveToLocation moveToLocation;
    private Transform bossStartLocation;
    void Start()
    {

        healthBarSliderComponent = healthBarSlider.GetComponent<Slider>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectManager = GetComponent<ObjectManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isHit = false;

        bossAnimations = GetComponent<BossAnimations>();

        bossStartLocation = GameObject.Find("BossStartLocation").GetComponent<Transform>();
        moveToLocation = GetComponent<MoveToLocation>();
        StartCoroutine(moveToLocation.StartMoving(bossStartLocation.position, 1f, EntranceCurve));
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
        }
    }

    public void BossDeath()
    {

        Invoke("bossActiveFalse", 0.2f);

    }
    void bossActiveFalse()
    {
        gameObject.SetActive(false);
    }


}
