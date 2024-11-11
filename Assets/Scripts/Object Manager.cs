using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject notePrefab;
    public float noteSpawnTime;
    GameObject[] note;
    int cnt;
    public bool isSlow;
    void Awake()
    {
        cnt=0;
        note = new GameObject[1000];
        isSlow=false;

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
    
    void makeObj()
    {
        if(isSlow){
                note[cnt].GetComponent<Note>().speed*=0.5f;
            }
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
