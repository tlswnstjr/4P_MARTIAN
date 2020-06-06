using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class J_LobbyManager : MonoBehaviourPunCallbacks
{
    // 버튼
    public Button btn_Create;
    public Button btn_Join;

    //입력 필드
    public InputField field_RoomName;
    public InputField fieid_maxplayer;

    // 대기 인원 텍스트 변수
    public Text txt_waitPlayers;


    public override void OnEnable()
    {
        //콜백 인터페이스 사용하기
        base.OnEnable();

        //
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // 방을 생성하기 버튼
    public void OnClickedCreateButton()
    {
        //룸의 옵션을 설정하자
        RoomOptions myRoom = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = byte.Parse(fieid_maxplayer.text)
        };

        PhotonNetwork.CreateRoom(field_RoomName.text, myRoom, TypedLobby.Default);
    }

    //방을 생성하는데 성공했을 떄 콜백 함수
    public override void OnCreatedRoom()
    {
        print("방 생성 성공");
    }

    //생성된 방에 들어 갔을때 콜백 함수
    public override void OnJoinedLobby()
    {
    }

    //방 생성에 실패 했을 때 콜백 함수 
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("방 생성 실패 했습니다 : " + message);

        
       // OnClickJoinButton();
    }


    // 방에 입장하기 버튼
    public void OnClickJoinButton()
    {

    }
}
