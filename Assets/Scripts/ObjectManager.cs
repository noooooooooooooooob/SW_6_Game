using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject notePrefab;
    public float noteSpawnTime;
    GameObject[] note;
    static int cnt;

    void Awake()
    {
        cnt=0;
        note = new GameObject[1000];

        Generate();
        makeObj();
    }

    void Generate()
    {
        for(int i=0;i<note.Length;i++)
        {
            note[i] = Instantiate(notePrefab);
            note[i].SetActive(false);
            note[i].GetComponent<Note>().prefabIdx=i;
        }
    }
    
    void makeObj()
    {
        note[cnt++].SetActive(true);
        Invoke("makeObj",noteSpawnTime);
    }

    void Update()
    {
        
        if(cnt>=1000)
        {
            cnt=0;
            Generate();
        }
    }
}
