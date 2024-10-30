using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReduceGaugebar : MonoBehaviour
{
    public GameObject Player;
    private PlayerMovement playerMovement;

    Slider slider;

    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerMovement.platformTime / 5;
    }
}
