using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{

    public bool red;
    public bool blue;
    public bool green;

    void Start()
    {
        red = true;
        blue = false;
        green = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            red = true;
            blue = false;
            green = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            red = false;
            blue = true;
            green = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            red = false;
            blue = false;
            green = true;
        }


    }

}
