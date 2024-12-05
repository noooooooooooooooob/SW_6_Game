using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioClip clickSound;       // 클릭 효과음 파일
    private AudioSource audioSource;   // AudioSource 컴포넌트

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound); // 클릭 효과음 재생
    }
}