using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public void DisablePlatform()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void EnablePlatform()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
