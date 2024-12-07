
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisappear : MonoBehaviour
{
    public float fadeDuration = 5f; // 사라지는 데 걸리는 시간
    private Material material;
    private float elapsedTime = 0f;

    public bool isDis;
    private void Awake() {
        
    }
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    public void changeBool(){
        isDis=true;
    }

    void Update()
    {
        if(isDis){
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeDuration;
            progress = Mathf.Clamp01(progress); // Ensure within 0 to 1

            // 시계방향으로 알파를 감소시키는 로직 (UV 좌표를 기반으로 구현 필요)
            // Alpha 값 감소 (단순 예)
            Color color = material.color;
            color.a = 1f - progress;
            material.color = color;

            if (progress >= 1f)
            {
                gameObject.SetActive(false); // 완전히 사라지면 비활성화
            }
        }
        
    }
}