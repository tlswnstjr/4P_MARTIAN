using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

using System.IO;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager gm;

    public Image[] playerPos;

    public Sprite[] charImages;


    int myNub;

    private void Awake()
    {
        // 싱글턴
        if (gm == null)
        {
            gm = this;
        }

        // 해상도를 윈도우 모드로 960 x 640 크기로 설정한다.
        Screen.SetResolution(1920, 1080
            , FullScreenMode.Windowed);
    }

    void Start()
    {
        // 1. RPC 전송 빈도를 설정하기
        PhotonNetwork.SendRate = 30;

        // 2. SerializeView 함수 호출 빈도를 설정하기
        PhotonNetwork.SerializationRate = 30;

        // 3. 플레이어 캐릭터 생성하기
        SpawnPlayer();

        // 4. 반응형 오브젝트 생성하기
        SpawnObject();
    }

    void SpawnPlayer()
    {
        // 팀을 설정하기
        myNub = PhotonNetwork.PlayerList.Length;
        playerPos[myNub - 1].enabled = true;
        //방에 들어오면 각자의 번호를 부여해줍니다 그 번호에 따라서 자신의 위치가 정해집니다 
        playerPos[myNub - 1].sprite = charImages[Random.Range(0, 4)];

    }

    void SpawnObject()
    {
        // 만일, 내가 방장이라면...
        if (PhotonNetwork.IsMasterClient)
        {
            // 반응형 오브젝트 프리팹을 생성한다.
            PhotonNetwork.Instantiate("InteractiveObject", new Vector3(0, 3, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        // 1. 방에서 나가기
        if (Input.GetKeyDown(KeyCode.L))
        {
            // 만일, 내가 방장이면서 방에 나말고 다른 사람이 있다면...
            if (PhotonNetwork.IsMasterClient && PhotonNetwork.PlayerList.Length > 1)
            {
                // 방장 권한을 자기 다음으로 들어온 사람에게 자동으로 방장 권한을 넘긴다.
                Player pl = PhotonNetwork.PlayerList[1];
                PhotonNetwork.SetMasterClient(pl);

                print("나 방장이었음");
                print("방장 권한을 " + pl.NickName + "에게 넘겼습니다.");
            }
            // 그 후에, 방에서 떠난다(서버) => 마스터 서버로 접속.
            PhotonNetwork.LeaveRoom();
            print("방을 떠났음");


        }

        // 2. 로비(마스터 서버)를 떠나기(=접속을 종료)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.Disconnect();
        }
    }

    public override void OnLeftRoom()
    {
        print("방 나갑니다~");
    }

    public override void OnConnected()
    {
        print("네임 서버에 다시 왔어요");
    }

    public override void OnConnectedToMaster()
    {
        print("마스터 서버에 다시 왔어요");

        // 로비로 접속
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        print("로비에 다시 왔어요");

        // 로비 씬으로 씬을 전환한다(클라이언트).
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    // 서버와의 연결이 종료되면 실행되는 콜백 함수
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("연결 종료 - " + cause);
        Application.Quit();
    }
}
