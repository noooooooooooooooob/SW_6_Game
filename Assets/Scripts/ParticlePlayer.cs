using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem particleSystem1; // 연결할 파티클 시스템
    public ParticleSystem particleSystem2; // 연결할 파티클 시스템
    public ParticleSystem particleSystem3; // 연결할 파티클 시스템
    public PlayerMovement playerMovement;
    public bool isPlay=false;
    public bool hasTriggered=false;

    void Start()
    {
        playerMovement=GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if(isPlay)
            switch(playerMovement.currentFloor)
            {
                case 1:
                    PlayParticle1();
                    break;
                case 2:
                    PlayParticle2();
                    break;
                case 3:
                    PlayParticle3();
                    break;
            }
    }

    // 파티클을 재생하는 함수
    public void PlayParticle1()
    {
        if (particleSystem1 != null)
        {
            particleSystem1.Stop(); // 파티클 중복 실행 방지
            particleSystem1.Play();
        }
        isPlay=false;
        Debug.Log("Play Particles ");
    }
    public void PlayParticle2()
    {
        if (particleSystem2 != null)
        {
            particleSystem2.Stop(); // 파티클 중복 실행 방지
            particleSystem2.Play();
        }
        isPlay=false;
        Debug.Log("Play Particles ");
    }
    public void PlayParticle3()
    {
        if (particleSystem3 != null)
        {
            particleSystem3.Stop(); // 파티클 중복 실행 방지
            particleSystem3.Play();
        }
        isPlay=false;
        Debug.Log("Play Particles ");
    }
}
