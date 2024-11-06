using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNodeInRange : MonoBehaviour
{
    bool hasNote = false;
    bool deleteNode = false;

    private FloorChecker floorChecker;
    public bool playerInRange = false;

    void Start()
    {
        floorChecker = GetComponentInChildren<FloorChecker>();
    }

    public void OnChildTrigger(){
        if (floorChecker!= null){
            playerInRange = floorChecker.hasPlayer;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (hasNote)
            {
                deleteNode = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            hasNote = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            if (deleteNode && playerInRange)
            {
                Destroy(other.gameObject);
                deleteNode = false;
                hasNote = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            hasNote = false;
        }
    }
}

