using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageUpEffect : MonoBehaviour
{
    public bool isPower;
    public GameObject object1;
    public GameObject object2;
     GameObject instance;
    // Start is called before the first frame update

    public void Effect(){
        object2.transform.position=new Vector2(object1.transform.position.x, object1.transform.position.y );
        instance = Instantiate(object2);
        
        //Instantiate(object2,object2.transform.position,Quaternion.identity);
        //StartCoroutine(DestroyAfterDelay(instance, 5.0f));
    }
    public void move(){
        Destroy(instance);
        
        object2.transform.position=new Vector2(object1.transform.position.x, object1.transform.position.y );
        instance = Instantiate(object2);
    }
    public void destroy(){
        if(isPower){
            Destroy(instance);
        }
        
    }
    /*
     IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        Debug.Log("des");
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
    */
}
