using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string itemType; // 아이템의 종류 또는 모양
    [SerializeField]
    public float speed;



    // Update is called once per frame
    void Update()
    {
        transform.Translate(-1f * speed * Time.deltaTime, 0, 0); // 등속 왼쪽 이동
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // 바닥에 닿으면 삭제
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            HealthBarController HC=FindAnyObjectByType<HealthBarController>();
            HC.Healthchange();
            Destroy(gameObject);
            
        }
    }
}
