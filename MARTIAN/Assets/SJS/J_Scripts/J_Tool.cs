using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Tool : MonoBehaviour
{
    //이 스크립트는 툴 스크립트로 플레이어가 특정 키를 눌러 
    //자신이 가지고 있는 아이템으로 만들수 있는 아이템이 무엇인지 보여주고 재작합니다 

    //현재 플레이어와 접촉중인지 확인해주는 bool문입니다
    bool onClickPlayer;

    //툴 자신만의 ui상점을 받아옵니다
    public GameObject shop;
    Test_PlayerMovement playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.Find("Player_Body").GetComponent<Test_PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ToolShop();
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            onClickPlayer = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            onClickPlayer = false;
        }
    }

    void ToolShop()
    {
        //플레이어와 충돌 체크를합니다
        if(onClickPlayer)
        {
            //충돌 중에 E키를 입력시 샵UI를 활성화합니다
            if(Input.GetKeyDown(KeyCode.E))
            {
                shop.SetActive(!shop.activeSelf);
                playerMove.myMoveban = !playerMove.myMoveban;
            }
        }
    }
}
