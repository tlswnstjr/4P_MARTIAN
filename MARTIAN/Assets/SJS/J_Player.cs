﻿

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class J_Player : MonoBehaviourPun, IPunObservable
{
    //내 부모
    Transform parnt;

    Animator anim;
    public Transform myTr;
    public float moveSpeed = 6f;

    public GameObject cam;

    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    //라이트
    public Light[] lights;

    //외부에서 플레이어가 i키를 입력했다는걸 알려주는 bool변수입니다
    public bool invClics;

    //인벤토리 오브젝트를 할당해줍니다
    public GameObject inv;

    //다른 플레이어의 이동을 동기화 해줄 변수
    Vector3 otherPos;
    public bool onAction;

    bool onInv;


    //애니메이션 파리미터 값 받아오기
    float h, v, speed, lerpSpeed = 50.0f;
    //아이템을 클릭하여 나오는 선택창입니다
    public GameObject iamge;

    //현제 보관함이랑 충돌을 하고 있는지 알기 위한 bool 변수입니다
    public bool lockerClick;

    private void Awake()
    {
        //----
        myTr = GetComponent<Transform>();
        parnt = gameObject.transform.parent;
        //DontDestroyOnLoad(parnt);

        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();

    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        print(anim.name);
    }
    private void FixedUpdate()
    {
        PlayerInputs();
        //인벤토리가 켜지면 바로 탈출하여 아래 코드를 막는다
        //또 한 툴을 열었을때도 아래 코드를 실행하는 것을 막아 줘야한다
        if (inv.activeSelf || myMoveban)
        {
            return;
        }

        if (iamge.activeSelf == true)
        {
            //아이템설정 창이 열려있으면 닫아준다 
            iamge.SetActive(false);
        }
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Move(h, v, 7);
        Turnning();
        TurnONOFF();
    }

    public void Turnning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
            Vector3 a = Vector3.forward;
            a = transform.forward;
        }

    }

    public void Move(float h, float v, float speed)
    {
        if(photonView.IsMine)
        {
            
            //Vector3 dir = new Vector3(h, 0, v);
            movement.Set(h, 0f, v);
            //애니메이터 컨트롤러의 파라미터값을 세팅
            anim.SetFloat("Speed", movement.magnitude);

            // movement 를 내가 바라보는 방향에서의 방향으로 변경
            movement = transform.TransformDirection(movement);

            movement = movement.normalized * speed * Time.deltaTime;

            playerRigidbody.MovePosition(transform.position + movement);
        }

        else
        {
            cam.SetActive(false);
           // transform.position = Vector3.Lerp(transform.position, otherPos, Time.deltaTime * lerpSpeed);

        }
    }


    public void TurnONOFF()
    {
        if (DayAndNight.sun.myRotX >= 170)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<Light>().enabled = true;
            }
        }
        else if (DayAndNight.sun.myRotX >= -10)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].GetComponent<Light>().enabled = false;
            }
        }
    }

    //자기 자신의 움직임을 막습니다
    public bool myMoveban;
    void PlayerInputs()
    {
        //인벤토리 열고 닫기입니다
        if (Input.GetKeyDown(KeyCode.I))
        {
            inv.SetActive(!inv.activeSelf);
            invClics = !invClics;
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            onAction = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            onAction = false;
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(anim.GetFloat("Speed"));
        }
        else
        {
            otherPos = (Vector3)stream.ReceiveNext();
            speed = (float)stream.ReceiveNext();
        }
    }
}