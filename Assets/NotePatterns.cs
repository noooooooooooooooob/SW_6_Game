using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePatterns : MonoBehaviour
{
    private ObjectManager objectManager;
    /*
        0~2는 3~1층
        3은 모든 칸
        4는 위, 중간
        5는 중간, 아래
    */
    int[][] bossPattern = new int[][]
    {
        new int[]{2, 2, 2, 2, 2}, // 0번 패턴
        new int[]{0, 2, 0, 0, 2}, // 1번 패턴
        new int[]{3, 3, 3, 3, 3}, // 2번 패턴
        new int[]{4, 5, 4, 5, 4}, // 3번 패턴
        new int[]{0,0,0,0,0,1,1,1,1,1,2,2,2,2,2}, // 4번 패턴
        new int[]{0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2}, // 5번 패턴
        new int[]{0,0, 1,1,2,2,1,1,2,2,0,0}

    };

    public int patternNoteColoridx;
    public int patternNoteArrowidx;

    public int patnum;
    public int patIdx;
    private int pos;
    public int[] colorOrder;
    public int colorOrderIdx;
    private int prevPos;
    void Awake()
    {
        objectManager = GetComponent<ObjectManager>();
        patnum = -1;
        patIdx = 0;
        prevPos = -1;
        colorOrderIdx = -1;
    }
    public void changePos()
    {
        Debug.Log("pos = " + bossPattern[patnum][patIdx]);

        objectManager.SetNoteColor(true, 0);
        objectManager.SetNoteDirection(true, 0);

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
                SpawnNotes(pos);
                break;
        }
    }

    public void activatePattern()
    {
        pos = bossPattern[patnum][patIdx];
        switch (patnum)
        {
            case 4:
                RandomizeColorOrder();
                ColorRows(pos);
                break;
            default:
                changePos();
                break;

        }

        if (patIdx + 1 >= bossPattern[patnum].Length)
        {
            Debug.Log("Boss Pattern Ended");
            patnum = -1;
            patIdx = 0;
            DeleteColorOrder();
        }
        else
        {
            patIdx++;
        }

    }

    public void SpawnNotes(int noteLine)
    {
        objectManager.SetNoteColor(false, patternNoteColoridx);
        objectManager.SetNoteDirection(false, patternNoteArrowidx);
        objectManager.SetNoteLine(false, noteLine);
        objectManager.setNoteSpeed(false, 0);
        objectManager.setNoteAttribute();
        objectManager.SetNotetoActive();
    }

    private void spawnNotesInAllRows()
    {
        Debug.Log("SpawnNotesInAllRows");
        for (int j = 0; j < 3; j++)
        {
            objectManager.SetNoteColor(false, patternNoteColoridx);
            objectManager.SetNoteDirection(false, patternNoteArrowidx);
            objectManager.SetNoteLine(false, j);
            objectManager.setNoteSpeed(false, 0);
            objectManager.setNoteAttribute();
            objectManager.SetNotetoActive();
        }
    }

    private void spawnTwoUp()
    {
        for (int j = 0; j < 2; j++)
        {
            objectManager.SetNoteColor(false, patternNoteColoridx);
            objectManager.SetNoteDirection(false, patternNoteArrowidx);
            objectManager.SetNoteLine(false, j);
            objectManager.setNoteSpeed(false, 0);
            objectManager.setNoteAttribute();
            objectManager.SetNotetoActive();
        }
    }
    private void spawnTwoDown()
    {
        for (int j = 2; j > 0; j--)
        {
            objectManager.SetNoteColor(false, patternNoteColoridx);
            objectManager.SetNoteDirection(false, patternNoteArrowidx);
            objectManager.SetNoteLine(false, j);
            objectManager.setNoteSpeed(false, 0);
            objectManager.setNoteAttribute();
            objectManager.SetNotetoActive();
        }
    }

    private void ColorRows(int pos)
    {
        if (prevPos != pos) //if it is the same line, Keep the same color
        {
            colorOrderIdx++;
        }

        objectManager.SetNoteColor(false, colorOrder[colorOrderIdx]);
        objectManager.SetNoteDirection(true, 0);
        objectManager.SetNoteLine(false, pos);
        objectManager.setNoteSpeed(false, 0);
        objectManager.setNoteAttribute();
        objectManager.SetNotetoActive();
        prevPos = pos;
    }

    private void RandomizeColorOrder()
    {
        if (colorOrder.Length == 0)
        {
            colorOrder = new int[3];
            for (int i = 0; i < 3; i++)
            {
                colorOrder[i] = i;
            }
            for (int i = 0; i < 3; i++)
            {
                int temp = colorOrder[i];
                int randomIndex = Random.Range(i, 3);
                colorOrder[i] = colorOrder[randomIndex];
                colorOrder[randomIndex] = temp;
            }
        }
    }

    private void DeleteColorOrder()
    {
        colorOrder = null;
        colorOrderIdx = -1;
    }
}

