
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

using System.IO;

public class J_GameManager : MonoBehaviourPunCallbacks
{
    public static J_GameManager gm;



    public PhotonView view;

    public bool[] readyClick;
    public bool x;
    public int myNub;

    private void Awake()
    {

        // 싱글턴
        if (gm == null)
        {
            gm = this;
        }

        // 해상도를 윈도우 모드로 960 x 640 크기로 설정한다.
        Screen.SetResolution(960, 640
            , FullScreenMode.Windowed);
    }

    void Start()
    {
        x = true;

        // 1. RPC 전송 빈도를 설정하기
        PhotonNetwork.SendRate = 30;

        // 2. SerializeView 함수 호출 빈도를 설정하기
        PhotonNetwork.SerializationRate = 30;

        // 3. 플레이어 캐릭터 생성하기
        SpawnPlayer();

        // 4. 반응형 오브젝트 생성하기
        SpawnObject();
    }

    public void SpawnPlayer()
    {
        //일단 여기가 생성해주는 곳입니다 
        //그러면 빌드한 애동 여기서 생성을 해주는건데 왜 게임 씬 혹은 빌드 파일에서 서로의 플레이어가 보이지
        //않는 것 일까요 흠 

       
            PhotonNetwork.Instantiate(Path.Combine("Test_Player"), new Vector3(-10, 1, -20), Quaternion.identity);
               
        //Test_Player
        // PhotonNetwork.Instantiate(Path.Combine("Camera Rig"), transform.position, Quaternion.identity);

        //GameObject obj = PhotonNetwork.Instantiate(Path.Combine("sss", "myChar"), 
        //     transform.position, Quaternion.identity);
        //  obj.transform.parent = parent.transform;
        //GameObject a = PhotonNetwork.Instantiate(imageF, transform.position, Quaternion.identity);


        //a.transform.parent = target.transform;
        //a.transform.SetParent(target.transform);
        //myNub = PhotonNetwork.PlayerList.Length;
        //myImages = a.GetComponent<Image>();
        //방에 들어오면 각자의 번호를 부여해줍니다 그 번호에 따라서 자신의 위치가 정해집니다 
        //myImages.sprite = charImages[Random.Range(0, 4)];
    }

    void SpawnObject()
    {
        // 만일, 내가 방장이라면...
        if (PhotonNetwork.IsMasterClient)
        {
            //지금 막 방을 만든 사람이 반장이라면 맵 전체의 구성을 만들우 둔다 
            //이제 여기서 추가해줘야하는 것들은 맵 뿐만아니라 각 플레이어끼리 상호작용하는 것들도 해줘야한다 
            GameObject x =  PhotonNetwork.Instantiate(Path.Combine("Ore","Ore"), new Vector3(-10, 1, -40), Quaternion.identity);
            x.SetActive(true);

           
            

            //gameStart.GetComponentInChildren<Text>().text = "게임 스타트";
            // 반응형 오브젝트 프리팹을 생성한다.
            //PhotonNetwork.Instantiate("InteractiveObject", new Vector3(0, 3, 0), Quaternion.identity);

        }
    }

    public void StoneIns()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //photonView.RPC("aaa", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void Masters(int a, string names, float ReincarnationSize, Vector3 tr)
    {
        print("확인차");
        if (PhotonNetwork.IsMasterClient)
        {
            print("아 짜증나");
            //photonView.RPC("ReincarnationSummons", RpcTarget.All);
            for (int i = 0; i < a; i++)
            {
                GameObject sum = PhotonNetwork.Instantiate(Path.Combine("Stone", names), tr, Quaternion.identity);
                //Instantiate(reincarnation);
                //여기가 새로 생성해주는 재료의 크기를 정해주는 변수입니다
                sum.transform.localScale = new Vector3(ReincarnationSize, ReincarnationSize, ReincarnationSize);
            }
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
