﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class J_Item : MonoBehaviourPun
{
    //아이템 이름
    public string itemName;
    //아이템 이미지
    public Sprite itemImage;
    //아이템 갯수
    public int auount =1;

    public GameObject my;
    //여기에 아이템 타입을 저장해줄 변수를 만들어야합니다
    public enum ItemType
    {
        WEAPON,  //장비
        CONSUMER, //소비CONSUMER
        STONE,  //원석
    }

    public ItemType type;

   //[HideInInspector]
   public bool click;

    [PunRPC]
    public void aaa()
    {
        if(photon.IsMine)
        {
            print("먹기");
            //아이템 메니저에게 내 자신의 정보를 넣어준다
            J_ItemManager.j_Item.ClicksItem(gameObject);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            print("삭제");
            Destroy(gameObject);
        }

    }

    PhotonView photon;

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {

            photon = coll.GetComponentInParent<PhotonView>();
            if(photon != null)
            {
                if (photon.IsMine)
                    click = true;
            }
            
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            photon = null;
            click = false;
        }
    }
}
