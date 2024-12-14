using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDamageUpEffect : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
     GameObject instance;
    
    
    bool change1;
    bool change2;
    void Start()
    {
       
        change1=true;
        change2=false;
        
    }

    
  
    void Update()
    {
        PlayerDamageUpEffect PD =FindObjectOfType<PlayerDamageUpEffect>();
        
        if(PD.isPower && !change2){
            object2.transform.position=new Vector2(object1.transform.position.x, object1.transform.position.y );
            instance = Instantiate(object2);
            change1=false;
            change2=true;
        }
        else if(!PD.isPower&& !change1){
             Destroy(instance);
        }
    }
    public void destroy(){
        //PlayerDamageUpEffect PD =FindObjectOfType<PlayerDamageUpEffect>();
       
        Destroy(instance);
        
    }
}
