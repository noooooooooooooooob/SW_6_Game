
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectInv : MonoBehaviour
{
    public GameObject object1; // 기준이 되는 물체1
    public float radius; // 거리 반경
    public void findItem(){
         // 기준 물체의 위치 가져오기
         
        Vector3 center = object1.transform.position;
       
        // "ItemMini" 태그를 가진 모든 객체 검색
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("ItemMini");

        foreach (GameObject obj in taggedObjects)
        {
            
            // 물체1 중심과의 거리 계산
            float distance = Vector3.Distance(center, obj.transform.position);
            
            if (distance <= radius)
            {
                // 해당 물체에 붙어있는 스크립트 가져오기
               ItemDisappear ID = obj.GetComponent<ItemDisappear>(); 
               if(ID!=null){
                    ID.changeBool();
               }
                
                
                
            }
        }
    }
  
}