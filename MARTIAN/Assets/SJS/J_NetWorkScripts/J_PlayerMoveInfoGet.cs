﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class J_PlayerMoveInfoGet : MonoBehaviourPun, IPunObservable
{
    //이 스크립트는 네트워크에 자기 자신의 정보를 보내줄 스크립트입니다 
    public J_Players J_Players;

    float h, v;

    float animSpeed;

    Vector3 rot;

    public GameObject cam;
    public float runSpeed;
    public Animator animator;
    float lerpSpeed = 50.0f;

    //다른 플레이어의 이동을 동기화 해줄 변수
    Vector3 otherPos;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            J_GameManager.gm.view = photonView;

        }
        J_Players.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        IsMyAndYous();
    }

    //나 자신인지 아니인지 확인하기 위한 함수입니다
    public void IsMyAndYous()
    {
        if(photonView.IsMine)
        {
            J_Players.Move(h, v, runSpeed);
            J_Players.Turnning();
            J_Players.PlayerInputs();
        }
        else
        {
            cam.SetActive(false);
            transform.position = Vector3.Lerp(transform.position, otherPos, Time.deltaTime * lerpSpeed);
            // transform.rotation = Quaternion.Euler(rot);
            J_Players.anim.SetFloat("Speed", animSpeed);
        }
    }

    //서버에 값을 올려줍니다
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //자기 자신이 값을 올려줄 때 사용합니다
        if(stream.IsWriting)
        {
            stream.SendNext(J_Players.myTr.position);
            //stream.SendNext(test_PlayerMovement.myTr.rotation);
            stream.SendNext(J_Players.anim.GetFloat("Speed"));
        }
        else
        {
            otherPos = (Vector3)stream.ReceiveNext();
            //rot = (Vector3)stream.ReceiveNext();
            animSpeed = (float)stream.ReceiveNext();
        }
    }
}
