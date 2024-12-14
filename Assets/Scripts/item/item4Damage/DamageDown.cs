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
        //DamageUpEffect DUE=FindObjectOfType<DamageUpEffect>();
        //DUE.isPower=false;
        PlayerMovement PM = FindObjectOfType<PlayerMovement>();
       PM.isPow=false;
       ReduceGaugebar RG=FindObjectOfType<ReduceGaugebar>();
       RG.isPow=false;
        PlayerDamageUpEffect PD =FindObjectOfType<PlayerDamageUpEffect>();
        PD.destroy();
       PD.isPower=false;
       

    }

}
