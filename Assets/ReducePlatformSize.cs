using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducePlatformSize : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMovement;

    // private Transform transform;
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.platformTime > 0)
        {
            float newScaleX = Mathf.Max(0, playerMovement.platformTime / 5);
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
