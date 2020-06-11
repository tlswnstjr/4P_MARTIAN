using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class J_PlayerMoveInfoGet : MonoBehaviourPun, IPunObservable
{
    //이 스크립트는 네트워크에 자기 자신의 정보를 보내줄 스크립트입니다 
    public Test_PlayerMovement test_PlayerMovement;

    float h, v;

    public float runSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
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
            test_PlayerMovement.Move(h, v, runSpeed);
            test_PlayerMovement.Turnning();
        }
    }

    //서버에 값을 올려줍니다
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //자기 자신이 값을 올려줄 때 사용합니다
        if(stream.IsWriting)
        {
            stream.SendNext(h);
            stream.SendNext(v);
        }
        else
        {
            h = (float)stream.ReceiveNext();
            v = (float)stream.ReceiveNext();
        }
    }
}
