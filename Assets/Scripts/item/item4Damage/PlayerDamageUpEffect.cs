using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageUpEffect : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    // Start is called before the first frame update
    private void Update() {
         object2.transform.position = object1.transform.position;
    }
    public void Effect(){
        GameObject instance = Instantiate(object2);
        StartCoroutine(DestroyAfterDelay(instance, 5.0f));
    }
     IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        Debug.Log("des");
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
