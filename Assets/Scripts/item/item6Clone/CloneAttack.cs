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

     private HashSet<GameObject> notesInRange = new HashSet<GameObject>();

    void Start()
    {

        healthBarController = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
    }


    void Update()
    {
        
        if(isClone){
            playerInRange=true;
        }
        else
        {
            playerInRange = false;
        }
        if(notesInRange.Count > 0 && playerInRange)//if (hasNote && playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //attack = true;
                //arrowDirection = ArrowDirectionEnum.left;
                ProcessNotes(ArrowDirectionEnum.left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //attack = true;
                //arrowDirection = ArrowDirectionEnum.right;
                 ProcessNotes(ArrowDirectionEnum.right);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //attack = true;
                //arrowDirection = ArrowDirectionEnum.up;
                 ProcessNotes(ArrowDirectionEnum.up);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //attack = true;
                //arrowDirection = ArrowDirectionEnum.down;
                 ProcessNotes(ArrowDirectionEnum.down);
            }
        }
        //else if (!playerInRange)
        //{
        //    attack = false;
        //}
    }
     void ProcessNotes(ArrowDirectionEnum direction)
    {
        attack = true;
        arrowDirection = direction;

        List<GameObject> notesToRemove = new List<GameObject>();
        CurrentColor CC = FindObjectOfType<CurrentColor>();

        foreach (var noteObject in new List<GameObject>(notesInRange))
        {
            Note note = noteObject.GetComponent<Note>();
            if (note != null)
            {
                if (note.noteArrowDirection == arrowDirection && CC.color == note.noteColor)
                {
                    note.StartMovingToBoss();
                }
                else
                {
                    ShakeAttackBar();
                    noteObject.SetActive(false);
                    note.StartMovingToPlayer();

                    if (healthBarController != null)
                    {
                        healthBarController.TakeDamage();
                    }
                    else
                    {
                        Debug.LogError("HealthBarController not found on Player object.");
                    }
                }

                notesToRemove.Add(noteObject);
            }
        }

        // Remove processed notes
        foreach (var noteObject in notesToRemove)
        {
            notesInRange.Remove(noteObject);
        }

        attack = false;
        arrowDirection = ArrowDirectionEnum.none;
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
            notesInRange.Add(other.gameObject);//hasNote = true;
        }
    }
    /*
    void OnTriggerStay2D(Collider2D other)
    {

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
                         notesInRange.Remove(other.gameObject);//hasNote = false;

                        arrowDirection = ArrowDirectionEnum.none;
                    }
                    else
                    {
                        ShakeAttackBar();
                        other.gameObject.SetActive(false);
                        note.StartMovingToPlayer();
                        attack = false;
                        notesInRange.Remove(other.gameObject);//hasNote = false;
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
    */
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
           notesInRange.Remove(other.gameObject);// hasNote = false;
            Note note = other.gameObject.GetComponent<Note>();
            if (note != null)
            {
                note.StartMovingToPlayer();
            }
        }
    }
}
