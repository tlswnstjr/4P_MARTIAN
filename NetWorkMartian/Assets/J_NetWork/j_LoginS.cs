using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class j_LoginS : MonoBehaviourPunCallbacks
{
    public string sevrerVer = "0.0.1";

    //아이디 입력 필드
    public InputField id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //접속하기 버튼을 클릭하면 실행되는 함수 
    public void OnClickCreateButton()
    {
        //서버 버번을 설정한다
        PhotonNetwork.GameVersion = sevrerVer;

        // 아이디를 서버에서 사용할 닉네임을 설정한다
        PhotonNetwork.NickName = id.text;

        // 씬 데이터를 자동으로 동기화하도록 설정한다
        PhotonNetwork.AutomaticallySyncScene = true;

        //오프라인 모드를 끈다
        PhotonNetwork.OfflineMode = false;

        //그 밖의 설정은 환경 설정 파일대로 서버에 접속을 시작한다
        //서버 접속 함수입니다
        PhotonNetwork.ConnectUsingSettings();
    }

    //네임 서버 접속 성공 콜백
    public override void OnConnected()
    {
        print("지나가겠습니다");
    }

    //마스터 서버 접속 성공  콜백
    public override void OnConnectedToMaster()
    {
        print("마스터 서버 접속 성공");

        //로비에 접속한다
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        //아래는 내가 따로 서버를 만들어서 할당해주는곳 위에는 기본적인 곳
        //PhotonNetwork.JoinLobby(new TypedLobby("Ma", LobbyType.Default));
    }

    //로비에 접속 성공 콜백
    public override void OnJoinedLobby()
    {
        print("로비 접속 성공");

        //로비 씬으로 전환합니다
        PhotonNetwork.LoadLevel("LobbyS");
    }
}
