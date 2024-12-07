using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;

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
    public bool spawnSlow;
    float spawnSlowTime;

    //boss logic
    public bool changingNoteLocation;
    public bool oppositeNoteArrow;
    public bool notActedNote;
    public bool fadeNote;
    public bool unifyNote;
    public int[,] bossPattern;
    public int patnum;
    public int patIdx;

    [Range(0, 2)]
    public int floors;
    int onlyOne = 0;

    private int noteLine; // Floor of the note (0~2) 

    //boss appear
    public bool isEnd;
    void Awake()
    {
        cnt = 0;
        note = new GameObject[100];
        isSlow = false;
        isEnd = false;
        noteSpawnTime = 1.0f;

        boss = GameObject.Find("Boss");

        Generate();

        Invoke("makeObj", 1f);
        // makeObj();
        // Boss pattern
        patnum = -1;
        patIdx = 0;
        setBossPattern();
    }

    void Generate()
    {
        for (int i = 0; i < note.Length; i++)
        {
            note[i] = Instantiate(notePrefab);
            note[i].SetActive(false);
        }
    }

    public void GameEnd()
    {
        for (int i = 0; i < note.Length; i++)
        {
            note[i].SetActive(false);
        }
    }

    void makeObj()
    {
        if (isEnd)
        {
            Debug.Log("생성 금지");
            return;
        }

        SetNoteLinetoRandom();

        if (isSlow)
            note[cnt].GetComponent<Note>().speed *= 0.5f;
        if (oppositeNoteArrow)
            note[cnt].GetComponent<Note>().isOpposite = true;
        if (notActedNote)
            note[cnt].GetComponent<Note>().isNotacted = true;
        if (fadeNote)
            note[cnt].GetComponent<Note>().isFaded = true;
        if (unifyNote)
        {
            for (int i = 10; i >= 0; i--)
            {
                if ((cnt - i) > 0)
                {
                    if (note[cnt - i] != null)
                    {
                        note[(cnt - i)].GetComponent<Note>().isSame = true;
                    }
                }

            }
        }
        // 패턴 테스트
        if (cnt > 2 && onlyOne == 0)
        {
            onlyOne = 1;
            patnum = 3;
            patIdx = 0;
        }

        if (patnum >= 0)
        {
            activatePattern();
        }

        note[cnt++].SetActive(true);

        if (cnt >= note.Length)
            cnt = 0;

        noteSpawnTime = Random.Range(noteSpawnTimeMin, noteSpawnTimeMax);
        spawnSlowTime = 1;

        if (spawnSlow)
        {
            spawnSlowTime *= 2;
        }

        noteSpawnTime *= spawnSlowTime;

        Invoke("makeObj", noteSpawnTime);
    }


    void Update()
    {
        if (cnt >= note.Length)
        {
            cnt = 0;
        }
    }

    void SetNoteLinetoRandom()
    {
        noteLine = Random.Range(0, floors);
    }

    void SetNoteLine(int line)
    {
        noteLine = line;
    }

    void changePos()
    {
        Debug.Log("pos = " + bossPattern[patnum, patIdx]);
        int pos = bossPattern[patnum, patIdx++];
        switch (pos)
        {
            case 3:
                spawnNotesInAllRows();
                break;
            case 4:
                spawnTwoUp();
                break;
            case 5:
                spawnTwoDown();
                break;
            default:
                note[cnt].GetComponent<Note>().posChange = pos;
                break;
        }
    }

    void activatePattern()
    {
        changePos();
        if (patIdx >= bossPattern.GetLength(1))
        {
            Debug.Log("Boss Pattern Ended");
            patnum = -1;
            patIdx = 0;
        }
    }


    void setBossPattern()
    {
        bossPattern = new int[,]
        {
        {2, 2, 2, 2, 2}, // 0번 패턴
        {0, 2, 0, 0, 2}, // 1번 패턴
        {3, 3, 3, 3, 3}, // 2번 패턴
        {4, 5, 4, 5, 4}  // 3번 패턴
        };
    }

    void spawnNotesInAllRows()
    {
        for (int j = 0; j < 3; j++)
        {
            // note[cnt].GetComponent<Note>().spawnLine = j;
            if (j < 2)
            {
                note[cnt++].SetActive(true);
            }
        }
    }

    void spawnTwoUp()
    {
        for (int j = 0; j < 2; j++)
        {
            // note[cnt].GetComponent<Note>().spawnLine = j;
            if (j < 1)
            {
                note[cnt++].SetActive(true);
            }
        }
    }
    void spawnTwoDown()
    {
        for (int j = 2; j > 0; j--)
        {
            // note[cnt].GetComponent<Note>().spawnLine = j;
            if (j > 1)
            {
                note[cnt++].SetActive(true);
            }
        }
    }
}

