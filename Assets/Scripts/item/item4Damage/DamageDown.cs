using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDown : MonoBehaviour
{
    HealthBarController healthBarController;
    void Start()
    {
        healthBarController = GameObject.Find("HealthBar").GetComponent<HealthBarController>();
    }
    public void Normalization()
    {
        Debug.Log("4초 후 실행되었습니다!");
        healthBarController.isDamageUp = false;
    }

}
