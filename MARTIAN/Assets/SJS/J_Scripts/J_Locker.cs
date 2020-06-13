using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//창고 ui스크립트를 상속해줍니다 
public class J_Locker : J_LockerInvs, IPunObservable
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //내가 값을 서버로 올려 보낼 때
        if(stream.IsWriting)
        {
            for(int i = 0; i < items.Count; i++)
            {
               //그 아이템 위치에 이름값에 ""혹은 nill이 아니면 값이 있다는 뜻으로 그 값을 서버로 올려줘야한다
                if (items[i].GetComponent<J_Slots>().names != "" || items[i].GetComponent<J_Slots>().names != null)
                {
                    stream.SendNext(items[i]);
                }               
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                //아이템 창의 이름 값에 비어 있다면 그곳에 서버에 올라간 아이템 정보를 가져 옵니다
                if(items[i].GetComponent<J_Slots>().names == "" || items[i].GetComponent<J_Slots>().names == null)
                {
                    items[i] = (J_Slots)stream.ReceiveNext();
                }
            }
        }
    }
}
