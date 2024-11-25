using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifColor : MonoBehaviour
{
    public void Normalization(){
       ObjectManager OM = FindObjectOfType<ObjectManager>();
   
        Debug.Log("OM실행");
        OM.unifyNote = false;
       
    }
}
