using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
    public GameObject healthBarSlider;
    private Slider healthBarSliderComponent;

    void Start()
    {
        healthBarSliderComponent = healthBarSlider.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarSliderComponent.value == 0.0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
