
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class inventory : MonoBehaviour
{
    public AudioClip bombSound;       // 클릭 효과음 파일
    private AudioSource AudioSource;   // AudioSource 컴포넌트
    public AudioClip powerUpSound;       // 클릭 효과음 파일


    [SerializeField]
    private GameObject[] itemsMini;
    private List<string> inventorys = new List<string>();
    private int inventorySize = 3;
    private float[] arrPosX = { -4.1f, -3f, -1.9f };
    private List<GameObject> spawnedItems = new List<GameObject>();

    [SerializeField]
    private GameObject[] inventoryMini;

    public StopSlow ST;
    public DamageDown DD;
    public DifColor DC;
    //private PlayerDisappear playerDisappear;
    public GameObject[] singleAttack;

    public GameObject cloneObject1;
    public GameObject cloneObject2;
    public GameObject cloneObject3;

    public GameObject smokeJumpUpPrefab;
    public GameObject smokeJumpDownPrefab;


    public PlayerAnimationEnum state;
    public bool currentClone;
    //private Clone clone;
    private int playerFloor;
    private bool wait;
    List<bool> bools = new List<bool>() { false, false, false,false,false,false };
    //public Action<int> onInvoke;

    void Awake()
    {

    }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        // 초기화: 인벤토리 크기만큼 null 값을 추가
        for (int i = 0; i < inventorySize; i++)
        {
            inventorys.Add(null);
            spawnedItems.Add(null); // Initialize spawnedItems with null for each slot
        }
        // clone=cloneObject.GetComponent<Clone>();
        //Clone CL = FindObjectOfType<Clone>();
        //CL.isDeleted=true;



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
                // Debug.Log($"Slot {i + 1}에 {itemtype}이(가) 추가되었습니다.");
                int number;
                int.TryParse(itemtype, out number);
                spawnitem(number - 1, arrPosX[i], i);
                
                return;
            }
        }
        // Debug.Log("인벤토리가 가득 찼습니다.");
    }

    void spawnitem(int index, float posX, int slotIndex)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z - 0.1f);

        GameObject newItem = Instantiate(itemsMini[index], spawnPos, Quaternion.identity);
        spawnedItems[slotIndex] = newItem;
    }
    // 지정된 슬롯의 아이템 사용
    private void UseItem(int slotIndex)
    {
        wait = false;
        int number=1;
        if(inventorys[slotIndex]!=null){
            
            int.TryParse(inventorys[slotIndex], out number);
        }
        
        if (slotIndex < 0 || slotIndex >= inventorySize || bools[number-1]==true)
        {
            Debug.Log("잘못된 슬롯입니다.");
            return;
        }

        if (inventorys[slotIndex] != null)
        {
            

            // itemtype이 "1"일 때 Note를 삭제하는 이벤트 호출
            if (inventorys[slotIndex] == "1")
            {
                Bomb bomb = FindObjectOfType<Bomb>();
                Flash flash = FindObjectOfType<Flash>();
                if (bomb != null)
                {
                    bomb.InvokeDeleteNotes(); // Bomb 인스턴스가 있을 때만 호출
                    PlayBombSound();
                    flash.FlashScreen();
                    
                }
                else
                {
                    Debug.LogWarning("Bomb 객체를 찾을 수 없습니다.");
                }
            }
            if (inventorys[slotIndex] == "2")
            {
                Slow slow = FindObjectOfType<Slow>();
                if (slow != null)
                {
                    slow.SlowNotes();
                    wait = true;
                    bools[1]=true;

                    Invoke("stopslow", 5f);
                }
                else
                {
                    Debug.LogWarning("Slow 객체를 찾을 수 없습니다.");
                }
            }
            if (inventorys[slotIndex] == "3")
            {
                AnyColor AC = FindAnyObjectByType<AnyColor>();
                if (AC != null)
                {
                    AC.sameColor();
                    wait = true;
                    bools[2]=true;

                    Invoke("difColor", 5f);
                }
                else
                {
                    Debug.LogWarning("AC 객체를 찾을 수 없습니다.");
                }
            }
            if (inventorys[slotIndex] == "4")
            {
                DamageUp DU = FindObjectOfType<DamageUp>();
                if (DU != null)
                {
                    DU.damageUp();
                    PlayPowerupSound();
                    wait = true;
                    bools[3]=true;

                    Invoke("damagedown", 5f);
                }
                else
                {
                    Debug.LogWarning("DU 객체를 찾을 수 없습니다.");
                }
            }
            //5는 체력반전 즉시실행

            if (inventorys[slotIndex] == "6")
            {
                currentClone = true;
                //clone=cloneObject.GetComponent<Clone>();
                 //DamageUpEffect DUE=FindObjectOfType<DamageUpEffect>();
                //DUE.isClone=true;
                //DUE.Effect();

                ReduceGaugebar RG = FindObjectOfType<ReduceGaugebar>();
                RG.isClone = true;
                //AttackNodeInRange ANIR = FindObjectOfType<AttackNodeInRange>();
                //ANIR.isClone = true;
                CloneAttack CA = FindObjectOfType<CloneAttack>();
                CA.isClone = true;

                PlayerMovement PM = FindObjectOfType<PlayerMovement>();
                PM.isClone = true;
                playerFloor = PM.currentFloor;
                /*
                playerDisappear = playerDisappearObject.GetComponent<PlayerDisappear>();
                PlayerDisappear PL = FindObjectOfType<PlayerDisappear>();
                if (PL != null)
                {

                    PL.isDeleted = true;
                }
                else
                {
                    Debug.LogWarning("Slow 객체를 찾을 수 없습니다.");
                }
                */


                //clone.isDeleted = false;

                //Clone clone=FindObjectOfType<Clone>();
                //clone.isDeleted=false;
                //playerDisappearObject.SetActive(false);


                for (int i = 0; i < 3; i++)
                {
                    AttackNodeInRange ANIR = singleAttack[i].GetComponent<AttackNodeInRange>();
                    ANIR.enabled = false;
                }


                if (playerFloor == 1)
                {
                    cloneObject2.SetActive(true);
                    cloneObject3.SetActive(true);
                    CloneAnime2 CA2 = FindObjectOfType<CloneAnime2>();
                    CloneAnime3 CA3 = FindObjectOfType<CloneAnime3>();
                    CA2.ChangeOriginColor(state);
                    CA3.ChangeOriginColor(state);
                    Vector3 smokePosition = cloneObject2.transform.position;
                    Instantiate(smokeJumpUpPrefab, smokePosition, Quaternion.identity);
                    smokePosition = cloneObject3.transform.position;
                    Instantiate(smokeJumpUpPrefab, smokePosition, Quaternion.identity);
                }
                else if (playerFloor == 2)
                {
                    cloneObject1.SetActive(true);
                    cloneObject3.SetActive(true);
                    CloneAnime1 CA1 = FindObjectOfType<CloneAnime1>();
                    CloneAnime3 CA3 = FindObjectOfType<CloneAnime3>();
                    CA1.ChangeOriginColor(state);
                    CA3.ChangeOriginColor(state);
                    Vector3 smokePosition = cloneObject1.transform.position;
                    Instantiate(smokeJumpUpPrefab, smokePosition, Quaternion.identity);
                    smokePosition = cloneObject3.transform.position;
                    Instantiate(smokeJumpUpPrefab, smokePosition, Quaternion.identity);
                }
                else if (playerFloor == 3)
                {
                    cloneObject1.SetActive(true);
                    cloneObject2.SetActive(true);
                    CloneAnime1 CA1 = FindObjectOfType<CloneAnime1>();
                    CloneAnime2 CA2 = FindObjectOfType<CloneAnime2>();
                    CA1.ChangeOriginColor(state);
                    CA2.ChangeOriginColor(state);
                    Vector3 smokePosition = cloneObject1.transform.position;
                    Instantiate(smokeJumpUpPrefab, smokePosition, Quaternion.identity);
                    smokePosition = cloneObject2.transform.position;
                    Instantiate(smokeJumpUpPrefab, smokePosition, Quaternion.identity);
                }


                wait = true;
                bools[5]=true;
                //PlayerElement PE= FindObjectOfType<PlayerElement>();
                //PE.isClone=true;
                Invoke("clonefin", 5f);
            }




            // Debug.Log($"Slot {slotIndex + 1}의 {inventorys[slotIndex]}을(를) 사용합니다.");
            if (spawnedItems[slotIndex] != null)
            {
                if (wait)
                {
                    ConnectInv CI = inventoryMini[slotIndex].GetComponent<ConnectInv>();
                    CI.findItem();
                    // ItemDisappear ID =FindObjectOfType<ItemDisappear>();
                    //ID.isDis=true;
                    StartCoroutine(InvokeWithParameter(slotIndex, 5f));

                }
                else
                {
                    Destroy(spawnedItems[slotIndex]);
                    spawnedItems[slotIndex] = null; // Clear the reference
                    inventorys[slotIndex] = null;
                }

            }


        }
        else
        {
            Debug.Log("해당 슬롯에 아이템이 없습니다.");
        }
    }

    //item 2 정상화
    void stopslow()
    {
        ST.Normalization();
        bools[1]=false;
    }

    //item 3 정상화
    void difColor()
    {
        DC.Normalization();
        bools[2]=false;
    }

    //item 4 정상화
    void damagedown()
    {
        DD.Normalization();
        bools[3]=false;
    }


    //item 6 정상화
    public void clonefin()
    {

        // if (playerDisappear != null)
        //{
        //    playerDisappear.isDeleted = !playerDisappear.isDeleted;
        //}
        if (currentClone)
        {
            currentClone = false;
            
            //DamageUpEffect DUE=FindObjectOfType<DamageUpEffect>();
            //DUE.isClone=false;
            //DUE.Effect();

            ReduceGaugebar RG = FindObjectOfType<ReduceGaugebar>();
            RG.isClone = false;
            //AttackNodeInRange ANIR = FindObjectOfType<AttackNodeInRange>();
            //ANIR.isClone = false;
            CloneAttack CA = FindObjectOfType<CloneAttack>();
            CA.isClone = false;
            PlayerMovement PM = FindObjectOfType<PlayerMovement>();
            PM.isClone = false;
            playerFloor = PM.currentFloor;
            
            //PlayerElement PE= FindObjectOfType<PlayerElement>();
            //PE.fuck();

            //Clone CL = FindObjectOfType<Clone>();
            //CL.isDeleted = true;
            /*
            clone.isDeleted=true;
            playerDisappear.isDeleted = false;
            playerDisappearObject.SetActive(true);
            */
            for (int i = 0; i < 3; i++)
            {
                AttackNodeInRange ANIR = singleAttack[i].GetComponent<AttackNodeInRange>();
                ANIR.enabled = true;
            }
            if (playerFloor == 1)
            {
           
                Vector3 smokePosition = cloneObject2.transform.position;
                Instantiate(smokeJumpDownPrefab, smokePosition, Quaternion.identity);
                smokePosition = cloneObject3.transform.position;
                Instantiate(smokeJumpDownPrefab, smokePosition, Quaternion.identity);
                if(bools[3]==true){
                   
                    CloneDamageUpEffect destroyPower2 =cloneObject2.GetComponent<CloneDamageUpEffect>();
                    destroyPower2.destroy();
                    CloneDamageUpEffect destroyPower3 =cloneObject3.GetComponent<CloneDamageUpEffect>();
                    destroyPower3.destroy();
                }
                
            }
            else if (playerFloor == 2)
            {
                Vector3 smokePosition = cloneObject1.transform.position;
                Instantiate(smokeJumpDownPrefab, smokePosition, Quaternion.identity);
                smokePosition = cloneObject3.transform.position;
                Instantiate(smokeJumpDownPrefab, smokePosition, Quaternion.identity);

                if(bools[3]==true){
                    CloneDamageUpEffect destroyPower1 =cloneObject1.GetComponent<CloneDamageUpEffect>();
                    destroyPower1.destroy();
                    CloneDamageUpEffect destroyPower3 =cloneObject3.GetComponent<CloneDamageUpEffect>();
                    destroyPower3.destroy();
                }
            }
            else if (playerFloor == 3)
            {
                Vector3 smokePosition = cloneObject1.transform.position;
                Instantiate(smokeJumpDownPrefab, smokePosition, Quaternion.identity);
                smokePosition = cloneObject2.transform.position;
                Instantiate(smokeJumpDownPrefab, smokePosition, Quaternion.identity);

                if(bools[3]==true){
                    CloneDamageUpEffect destroyPower1 =cloneObject1.GetComponent<CloneDamageUpEffect>();
                    destroyPower1.destroy();
                    CloneDamageUpEffect destroyPower2 =cloneObject2.GetComponent<CloneDamageUpEffect>();
                    destroyPower2.destroy();
                }
            }
            cloneObject1.SetActive(false);
            cloneObject2.SetActive(false);
            cloneObject3.SetActive(false);

            
            //cloneObject.SetActive(false);
            //playerDisappearObject.SetActive(true);

            //PlayerMovement PM= FindObjectOfType<PlayerMovement>();
            //PM.doEntrance=false;
            //PM.AllowMovement();

            //PlayerAnimations PANI = FindObjectOfType<PlayerAnimations>();
            //PANI.ChangeOriginColor(state);
            bools[5]=false;
        }




        //PlayerElement PE= FindObjectOfType<PlayerElement>();
        //PE.isClone=false;



    }

    System.Collections.IEnumerator InvokeWithParameter(int number, float delay)
    {
        yield return new WaitForSeconds(delay);
        fade(number);
    }

    void fade(int slotIndex)
    {
        // ItemDisappear ID =FindObjectOfType<ItemDisappear>();
        //ID.isDis=false;
        Destroy(spawnedItems[slotIndex]);
        spawnedItems[slotIndex] = null; // Clear the reference
        inventorys[slotIndex] = null;
        wait = false;
        bools[slotIndex] = false;

    }

    public void PlayBombSound()
    {
        AudioSource.PlayOneShot(bombSound); // 클릭 효과음 재생
    }
    public void PlayPowerupSound()
    {
        AudioSource.PlayOneShot(powerUpSound); // 클릭 효과음 재생
    }

}