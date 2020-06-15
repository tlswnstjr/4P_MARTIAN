using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TestNEt : MonoBehaviourPunCallbacks
{
    public string serverVersion = "0.0.1";
    void Start()
    {
        // 서버 버전을 설정한다.
        PhotonNetwork.GameVersion = serverVersion;
     
        // 오프라인 모드를 끈다.
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //마스터 서버에 접속 시도
        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        RoomOptions ro = new RoomOptions { MaxPlayers = byte.Parse("5") };
        PhotonNetwork.JoinOrCreateRoom("TestRoom", ro, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        print("방 생성 성공!");
    }
    // 방 생성에 실패했을 때의 콜백 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("방 생성 실패 - " + message);

        //OnClickedJoinButton();
    }

    // 방에 입장이 실패했을 때의 콜백 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("방 입장 실패 -" + message);
    }
    public override void OnJoinedRoom()
    {
        print("확인");
        PhotonNetwork.LoadLevel("SJSM_Map");
        //PhotonNetwork.LoadLevel("Test_INSIDE");
    }
}
