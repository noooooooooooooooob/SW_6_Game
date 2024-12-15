using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeFalse : MonoBehaviour
{
    public bool isActive;
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick()
    {
        if (isActive)
            isActive = false;
        else
            isActive = true;

        gameObject.SetActive(isActive);
    }
}
