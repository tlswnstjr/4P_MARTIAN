using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_Locker : MonoBehaviour
{
    //이스크립트는 보관함 스크립트입니다 

    
    //이 아래 bool문은 플레이어가 충돌중에 있는지 확인하고 그 다음 키 입력에 대한
    //상태 전이를 확인해주기 위해 존재합니다
    bool offLocker;

    //표시 해줄 ui오브젝트를 답고 있는 게임 오브젝트 형식의 오브젝트변수입니다
    public GameObject inventroyAndLocker;

    Test_PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        //inventroyAndLocker = GameObject.FindGameObjectWithTag("LcckerAndInv");
        inventroyAndLocker = GameObject.Find("INVENTORY").transform.GetChild(3).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //플레이어가 충돌중인게 확인되면
        if(offLocker)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                inventroyAndLocker.SetActive(!inventroyAndLocker.activeSelf);
                player.myMoveban = !player.myMoveban;
            }
            
        }
    }


    private void OnTriggerEnter(Collider coll)
    {
        player = coll.GetComponent<Test_PlayerMovement>();
        if (coll.gameObject.tag == "Player")
        {
            player.lockerClick = true;
           
            offLocker = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        player = coll.GetComponent<Test_PlayerMovement>();

        if (coll.gameObject.tag == "Player")
        {
            player.lockerClick = false;
            offLocker = false;
        }
    }
}
