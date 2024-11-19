using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject boss;
    public GameObject notePrefab;
    public float noteSpawnTime;
    public float noteSpawnTimeMin;
    public float noteSpawnTimeMax;
    GameObject[] note;
    int cnt;
    public bool isSlow;

    //boss logic
    public bool changingNoteLocation;
    public bool oppositeNoteArrow;
    public bool notActedNote;
    public bool fadeNote;
    public bool unifyNote;

    //boss appear
    public bool isEnd;
    void Awake()
    {
        cnt=0;
        note = new GameObject[1000];
        isSlow=false;
        isEnd=false;
        noteSpawnTime=1.0f;

        boss=GameObject.Find("Boss");

        Generate();
        makeObj();
    }

    void Generate()
    {
        for(int i=0;i<note.Length;i++)
        {
            note[i] = Instantiate(notePrefab);
            note[i].SetActive(false);
        }
    }

    void bossEnd()
    {
        for(int i=0;i<note.Length;i++)
        {
            note[i].SetActive(false);
        }
    }
    
    void makeObj()
    {
        if(isEnd)
        {
            Debug.Log("생성 금지");
            return;
        }

        if(isSlow)
            note[cnt].GetComponent<Note>().speed*=0.5f;
        if(oppositeNoteArrow)
            note[cnt].GetComponent<Note>().isOpposite=true;
        if(notActedNote)
            note[cnt].GetComponent<Note>().isNotacted=true;
        if(fadeNote)
            note[cnt].GetComponent<Note>().isFaded=true;
        if(unifyNote)
            note[cnt].GetComponent<Note>().isSame=true;

        note[cnt++].SetActive(true);
           
        if(cnt>=1000)
            cnt=0;
        noteSpawnTime=Random.Range(noteSpawnTimeMin,noteSpawnTimeMax);
        Invoke("makeObj",noteSpawnTime);
    }

    void Update()
    {
        if(cnt>=1000)
        {
            cnt=0;
        }
        if(boss.activeSelf==false)
        {
            isEnd=true;
            bossEnd();
        }
    }
}
