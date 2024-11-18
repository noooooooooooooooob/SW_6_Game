using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

            Destroy(gameObject, animationLength);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
