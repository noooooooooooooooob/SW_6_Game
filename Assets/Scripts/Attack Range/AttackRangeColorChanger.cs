using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeColorChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            spriteRenderer.color = new Color(0, 1, 0, 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.color = new Color(0, 0, 1, 0.4f);
        }

    }
}
