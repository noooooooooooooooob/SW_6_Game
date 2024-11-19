using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeColorChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 0, 0, 0.4f);// Default color is red
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            spriteRenderer.color = new Color(0, 1, 0, 0.4f);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            spriteRenderer.color = new Color(0, 0, 1, 0.4f);
        }

    }
}
