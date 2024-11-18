using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{

    public ColorEnum playerCurrentElement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerCurrentElement = ColorEnum.red;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerCurrentElement = ColorEnum.green;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerCurrentElement = ColorEnum.blue;
        }


    }

}
