using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemsMini;
    private List<string> inventorys = new List<string>();
    private int inventorySize = 3;
    private float[] arrPosX = { -4.1f, -3f, -1.9f };
    private List<GameObject> spawnedItems = new List<GameObject>();

    void Start()
    {
        // 초기화: 인벤토리 크기만큼 null 값을 추가
        for (int i = 0; i < inventorySize; i++)
        {
            inventorys.Add(null);
            spawnedItems.Add(null); // Initialize spawnedItems with null for each slot
        }
    }

    void Update()
    {


        // 각 숫자 키로 아이템 사용
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
    }

    // 첫 번째 빈 슬롯에 아이템 추가
    public void AddItem(string itemtype)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventorys[i] == null)
            {
                inventorys[i] = itemtype;
                Debug.Log($"Slot {i + 1}에 {itemtype}이(가) 추가되었습니다.");
                int number;
                int.TryParse(itemtype, out number);
                spawnitem(number - 1, arrPosX[i], i);
                return;
            }
        }
        Debug.Log("인벤토리가 가득 찼습니다.");
    }
    void spawnitem(int index, float posX, int slotIndex)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z - 1f);

        GameObject newItem = Instantiate(itemsMini[index], spawnPos, Quaternion.identity);
        spawnedItems[slotIndex] = newItem;
    }
    // 지정된 슬롯의 아이템 사용
    private void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventorySize)
        {
            Debug.Log("잘못된 슬롯입니다.");
            return;
        }

        if (inventorys[slotIndex] != null)
        {
            Debug.Log($"Slot {slotIndex + 1}의 {inventorys[slotIndex]}을(를) 사용합니다.");
            if (spawnedItems[slotIndex] != null)
            {
                Destroy(spawnedItems[slotIndex]);
                spawnedItems[slotIndex] = null; // Clear the reference
            }

            inventorys[slotIndex] = null;
        }
        else
        {
            Debug.Log("해당 슬롯에 아이템이 없습니다.");
        }
    }
}
/*
public class inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemsMini;
    private List<string> inventorys = new List<string>();
    private List<GameObject> spawnedItems = new List<GameObject>(); // List to track spawned items
    private int inventorySize = 3;
    private float[] arrPosX = {-3.8f,-3f,-2.2f};

    void Start()
    {
        // 초기화: 인벤토리 크기만큼 null 값을 추가
        for (int i = 0; i < inventorySize; i++)
        {
            inventorys.Add(null);
            spawnedItems.Add(null); // Initialize the spawned items list with null
        }
    }

    void Update()
    {
        // 각 숫자 키로 아이템 사용
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
    }

    // 첫 번째 빈 슬롯에 아이템 추가
    public void AddItem(string itemtype)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventorys[i] == null)
            {
                inventorys[i] = itemtype;
                Debug.Log($"Slot {i + 1}에 {itemtype}이(가) 추가되었습니다.");
                int number;
                int.TryParse(itemtype, out number);
                spawnitem(number-1, arrPosX[i], i); // Pass the slot index to track the spawned item
                return;
            }
        }
        Debug.Log("인벤토리가 가득 찼습니다.");
    }

    void spawnitem(int index, float posX, int slotIndex)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        GameObject newItem = Instantiate(itemsMini[index], spawnPos, Quaternion.identity);
        spawnedItems[slotIndex] = newItem; // Store the instantiated item at the corresponding slot index
    }

    // 지정된 슬롯의 아이템 사용
    private void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventorySize)
        {
            Debug.Log("잘못된 슬롯입니다.");
            return;
        }

        if (inventorys[slotIndex] != null)
        {
            Debug.Log($"Slot {slotIndex + 1}의 {inventorys[slotIndex]}을(를) 사용합니다.");

            // Remove the item from the inventory
            inventorys[slotIndex] = null;

            // Destroy the spawned item associated with this slot, if it exists
            if (spawnedItems[slotIndex] != null)
            {
                Destroy(spawnedItems[slotIndex]);
                spawnedItems[slotIndex] = null; // Clear the reference
            }
        }
        else
        {
            Debug.Log("해당 슬롯에 아이템이 없습니다.");
        }
    }
}*/