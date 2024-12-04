using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAnime3 : MonoBehaviour
{
     Animator animator;
    PlayerAnimationEnum tempState;
    // Start is called before the first frame update
    public void ChangeOriginColor(PlayerAnimationEnum state){
        
        if(state==PlayerAnimationEnum.blue_run||state==PlayerAnimationEnum.blue_idle){
            Vector3 currentPosition = transform.position;
            if(currentPosition.y>-3) {
                tempState=PlayerAnimationEnum.blue_idle;
            }
            else{
                tempState=PlayerAnimationEnum.blue_run;
            }
        }
        else if(state==PlayerAnimationEnum.green_run||state==PlayerAnimationEnum.green_idle){
            Vector3 currentPosition = transform.position;
            if(currentPosition.y>-3) {
                tempState=PlayerAnimationEnum.green_idle;
            }
            else{
                tempState=PlayerAnimationEnum.green_run;
            }
        }
        else if(state==PlayerAnimationEnum.red_run||state==PlayerAnimationEnum.red_idle){
            Vector3 currentPosition = transform.position;
            if(currentPosition.y>-3) {
                tempState=PlayerAnimationEnum.red_idle;
            }
            else{
               tempState=PlayerAnimationEnum.red_run;
            }
        }
      
        animator = GetComponent<Animator>();
        animator.Play(tempState.ToString());
    }
}
