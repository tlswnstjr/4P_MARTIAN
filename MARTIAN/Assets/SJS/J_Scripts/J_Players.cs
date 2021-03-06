﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class J_Players : MonoBehaviourPun
{
    //내 부모의 photonview 가져오기
    public PhotonView view;
    //내 부모
    Transform parnt;
    Check_In_Outside cio;

    public Animator anim;
    public Transform myTr;
    public float moveSpeed = 6f;

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

    public bool onAction;

    bool onInv;

    public AudioClip footStepClip;

    public AudioSource audio;


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

        cio = GetComponent<Check_In_Outside>();

        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {

        inv = J_Inventory.j_Inventory.gameObject;
        iamge = J_Inventory.j_Inventory.iamges;
        inv.SetActive(false);
    }
    private void FixedUpdate()
    {
        PlayerInputs();
        //인벤토리가 켜지면 바로 탈출하여 아래 코드를 막는다
        //또 한 툴을 열었을때도 아래 코드를 실행하는 것을 막아 줘야한다
        if (inv.activeSelf || myMoveban || cio.doingAnim)
        {
            return;
        }


        if (iamge.activeSelf == true)
        {
            //아이템설정 창이 열려있으면 닫아준다
            iamge.SetActive(false);
        }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Horizontal")|| Input.GetButtonDown("Vertical"))
        {
            audio.clip = footStepClip;
            audio.Play();
        }
        else if (Input.GetButton("Horizontal")|| Input.GetButton("Vertical"))
        {
            if (!audio.isPlaying)
            {
                audio.clip = footStepClip;
                audio.Play();
            }
        }
        else if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            audio.Stop();
        }

         Move(h, v, 7);
        Turnning();
        TurnONOFF();
        //inv.activeSelf ||
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
        //Vector3 dir = new Vector3(h, 0, v);
        movement.Set(h, 0f, v);
        //애니메이터 컨트롤러의 파라미터값을 세팅
        anim.SetFloat("Speed", movement.magnitude);

        // movement 를 내가 바라보는 방향에서의 방향으로 변경
        movement = transform.TransformDirection(movement);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }
    public void TurnONOFF()
    {
        //만약 외부씬이 아닐경우에
        if (SceneManager.GetActiveScene().name != "Test_INSIDE")
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
    }

    //자기 자신의 움직임을 막습니다
    public bool myMoveban;
   public void PlayerInputs()
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
}
