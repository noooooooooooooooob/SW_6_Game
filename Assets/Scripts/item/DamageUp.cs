using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour
{
    //public HealthBarController HBC;
   public void Start()
    {
        if (HealthBarController.Instance != null)
        {
            HealthBarController.Instance.isDamageUp=true; // 원하는 값으로 변경
           
        }
       
    }
}
