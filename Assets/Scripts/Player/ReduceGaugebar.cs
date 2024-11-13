using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReduceGaugebar : MonoBehaviour
{
    public GameObject Player;
    private PlayerMovement playerMovement;
    private RectTransform GaugeBar;
    

    public float maxShakeAmount = 0.1f;
    public float shakeIntensityMultiplier = 1.0f;
    Slider slider;

    private Vector2 initialPosition;

    void Start()
    {
        GaugeBar = GetComponent<RectTransform>();
        playerMovement = Player.GetComponent<PlayerMovement>();
        slider = GetComponent<Slider>();

        initialPosition = GaugeBar.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerMovement.platformTime / 5;

        if (slider.value < 0.5f)
        {
            Shake();
        }
        else{
            GaugeBar.anchoredPosition = initialPosition;
        }
    }

    void Shake(){

        float shakeAmount = (1 - slider.value) * shakeIntensityMultiplier * maxShakeAmount;

        // Apply random offset for shake effect
        float offsetX = Random.Range(-shakeAmount, shakeAmount);
        float offsetY = Random.Range(-shakeAmount, shakeAmount);

        GaugeBar.anchoredPosition= initialPosition + new Vector2(offsetX, offsetY);
    }
}
