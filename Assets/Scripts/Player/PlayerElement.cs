using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{

    public ColorEnum playerCurrentElement;

    void Start()
    {
        playerCurrentElement = ColorEnum.red;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerCurrentElement = ColorEnum.red;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerCurrentElement = ColorEnum.green;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerCurrentElement = ColorEnum.blue;
        }



    }

}
