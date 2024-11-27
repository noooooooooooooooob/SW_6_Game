using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_spawn : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private float spawnTime;
    private float[] arrPosY = {-4f,-1f,2f};
    public bool test;

    void Start()
    {
        StartItemRotine();
    }
    void StartItemRotine(){
        StartCoroutine("itemRoutine");
    }
    IEnumerator itemRoutine(){
        yield return new WaitForSeconds(3f);
        int k=0;
        while(true){
            if(test){
                
                spawnitem(k,arrPosY[0]);
                k++;
                if(k==6)
                    k=0;
                yield return new WaitForSeconds(spawnTime);
                continue;
            }
            int i=Random.Range(0,3);
            int j=Random.Range(0,2);
            int index = Random.Range(0,items.Length);
           
            if(j==0){
                yield return new WaitForSeconds(spawnTime);
            }
            else{
                 spawnitem(index,arrPosY[i]);
                yield return new WaitForSeconds(spawnTime);
            }
            
        }
        
        
    }

    void spawnitem(int index,float posY){
        Vector3 spawnPos = new Vector3(transform.position.x,posY,transform.position.z+1);
        Instantiate(items[index],spawnPos,Quaternion.identity);
    }
    void Update()
    {
        
    }
}
