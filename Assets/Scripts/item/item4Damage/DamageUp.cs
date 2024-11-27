using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour
{
    [SerializeField] public HealthBarController healthBarController;
    //public HealthBarController HBC;
   public void damageUp()
    {
        /*
        if (HealthBarController.Instance != null)
        {
            HealthBarController.Instance.isDamageUp=true; // 원하는 값으로 변경
           Debug.Log("damageup");
        }
        else{
            Debug.Log(".....dama");
        }
        */
         GameObject healthBarObject = GameObject.Find("HealthBar"); // HealthBar 오브젝트 이름으로 찾기
        if (healthBarObject != null)
        {
            healthBarController = healthBarObject.GetComponent<HealthBarController>();
        }
        if (healthBarController != null)
        {
            healthBarController.isDamageUp = true;
        }
   
    
        
       
    }
}
