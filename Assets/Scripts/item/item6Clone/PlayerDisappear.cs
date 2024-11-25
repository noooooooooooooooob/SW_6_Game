using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisappear : MonoBehaviour
{
    
    public GameObject targetObject; // 관리할 2D 오브젝트
    public bool isDeleted = false;// true: 비활성화, false: 활성화

  

   
    void Update()
    {
        AttackNodeInRange ANIR = FindObjectOfType<AttackNodeInRange>();
        if (isDeleted)
        {
            ANIR.isClone=true;
            if (targetObject != null && targetObject.activeSelf)
            {
                targetObject.SetActive(false); // 비활성화
            }
        }
        else
        {
            ANIR.isClone=false;
            if (targetObject != null && !targetObject.activeSelf)
            {
                targetObject.SetActive(true);// 활성화
            }
        }

        
    }
        
    
}
