using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAttack : MonoBehaviour
{
    public bool hasNote = false;
    public bool attack = false;
    
    public bool playerInRange = false;

    private HealthBarController healthBarController;
    private GameObject player;
    private PlayerElement playerElement;
    public ArrowDirectionEnum arrowDirection;

    private GameObject clone;
    

    public bool isClone;

    void Start()
    {
        
        //player = GameObject.Find("Player");
        //playerElement = player.GetComponent<PlayerElement>();
        
        //clone = GameObject.Find("clone");
        

        healthBarController = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
    }

    //public void OnChildTrigger()
   // {

        
    //}

    void Update()
    {
        
        if(isClone){
            playerInRange=true;
        }
        else
        {
            playerInRange = false;
        }
        if (hasNote && playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.right;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                attack = true;
                arrowDirection = ArrowDirectionEnum.down;
            }
        }
        else if (!playerInRange)
        {
            attack = false;
        }
    }

    void ShakeAttackBar()
    {
        AttackRangeShaker attackRangeShaker = GetComponentInParent<AttackRangeShaker>();
        attackRangeShaker.StartShaking();

    }

    void OnTriggerEnter2D(Collider2D other) // 공격범위에 들어왔을때
    {
        if (other.gameObject.tag == "Note")
        {
            hasNote = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        /*
        if(isClone){
            playerElement=clone.GetComponent<PlayerElement>();
        }
        else{
            playerElement = player.GetComponent<PlayerElement>();
        }
        */
        if (other.gameObject.tag == "Note")
        {
            Note note = other.gameObject.GetComponent<Note>();
            CurrentColor CC=FindObjectOfType<CurrentColor>();
            
            if (attack && playerInRange)
            {
               
                    if(note.noteArrowDirection == arrowDirection && CC.color == note.noteColor)
                    {
                        note.StartMovingToBoss();
                        attack = false;
                        hasNote = false;

                        arrowDirection = ArrowDirectionEnum.none;
                    }
                    else
                    {
                        ShakeAttackBar();
                        other.gameObject.SetActive(false);
                        attack = false;
                        hasNote = false;
                        arrowDirection = ArrowDirectionEnum.none;

                        if (healthBarController != null)
                        {
                            healthBarController.TakeDamage();
                        }
                        else
                        {
                            Debug.LogError("HealthBarController not found on Player object.");
                        }
                    }
               
                
                
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            hasNote = false;
            Note note = other.gameObject.GetComponent<Note>();
            if (note != null)
            {
                note.StartMovingToPlayer();
            }
        }
    }
}
