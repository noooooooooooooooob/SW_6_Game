using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyColor : MonoBehaviour
{
    
    //public float minX = -13f;
    //public float maxX = 13f;
   // public float minY = -5f;
    //public float maxY = 5f;
    ColorEnum playerColor;
    
    
   
    public void sameColor()
    {
        ObjectManager OM = FindObjectOfType<ObjectManager>();
        
        if (OM != null)
        {
            Debug.Log("OM실행");
            OM.unifyNote = true;
        }
        else
        {
            Debug.LogWarning("OM스크립트가 할당되지 않았습니다.");
        }
       
       
        

    }
}
