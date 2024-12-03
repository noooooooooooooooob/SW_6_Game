using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducePlatformSize : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMovement;
    public float sizeMultiplier;

    ReduceGaugebar staminaBar;
    // private Transform transform;
    void Start()
    {
        staminaBar = GameObject.Find("StaminaBar").GetComponent<ReduceGaugebar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaBar.platformTime > 0)
        {
            float newScaleX = Mathf.Max(0, staminaBar.platformTime / 5) * sizeMultiplier;
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
