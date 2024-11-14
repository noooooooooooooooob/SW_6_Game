using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDown : MonoBehaviour
{
    
    public void Normalization(){
        Debug.Log("4초 후 실행되었습니다!");
        HealthBarController.Instance.isDamageUp=false;
    }
    
}
