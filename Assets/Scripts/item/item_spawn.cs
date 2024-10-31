using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_spawn : MonoBehaviour
{
    
    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private float spawnTime;
    private float[] arrPosY = {-4f,-1f,2.5f};
    void Start()
    {
        StartItemRotine();
    }
    void StartItemRotine(){
        StartCoroutine("itemRoutine");
    }
    IEnumerator itemRoutine(){
        yield return new WaitForSeconds(0.3f);
        while(true){
            int i=Random.Range(0,3);
            int index = Random.Range(0,items.Length);
            spawnitem(index,arrPosY[i]);
            yield return new WaitForSeconds(spawnTime);
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
