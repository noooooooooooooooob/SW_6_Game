using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElement : MonoBehaviour
{

    public bool red;
    public bool blue;
    public bool green;
    public int color;

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
            color=0;
            red = true;
            blue = false;
            green = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            color=1;
            red = false;
            blue = true;
            green = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            color=2;
            red = false;
            blue = false;
            green = true;
        }
        


    }

}
