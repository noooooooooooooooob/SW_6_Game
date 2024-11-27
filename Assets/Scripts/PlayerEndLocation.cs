using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndLocation : MonoBehaviour
{
    // Start is called before the first frame update
    GameTransition gameTransition;
    void Start()
    {
        gameTransition = GameObject.Find("GameManager").GetComponent<GameTransition>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameTransition.PrepNextScene();
        }
    }

}
