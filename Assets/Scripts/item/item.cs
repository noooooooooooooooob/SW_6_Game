using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class item : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private string itemType; // 아이템의 종류 또는 모양
    [SerializeField]
    public float speed;

    [SerializeField]
    private inventory inventorys;

    void Start()
    {
        // inventorys가 설정되지 않았다면 자동으로 할당해줌 (필요 시)
        if (inventorys == null)
        {
            inventorys = GameObject.FindObjectOfType<inventory>();
        }
    }


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

            if (inventorys != null)
            {

                inventorys.AddItem(itemType); // 아이템 종류를 인벤토리에 추가
            }
            Destroy(gameObject); // 인벤토리에 추가 후 삭제
        }
    }

}
