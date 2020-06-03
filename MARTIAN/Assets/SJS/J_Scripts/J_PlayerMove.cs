using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_PlayerMove : MonoBehaviour
{

    //방향키 입력을 받아 줄 변수가 필요합니다
    //상하 좌우의 입력을 받아 줍니다
    //기억하세요
    float h;
    float v;

    //외부에서 플레이어가 i키를 입력했다는걸 알려주는 bool변수입니다
    public bool invClics;

    //인벤토리 오브젝트를 할당해줍니다
    public GameObject inv;

    //플레이어의 좌표를 할당 받을 변수입니다
    Vector3 myDir = Vector3.zero;
    //이동을 위한 스피드 값입니다
    public float speed = 7f;
    //플레이어는 캐릭터 컨트롤러로 움직일 겁니다
    CharacterController cc;

    public bool onAction;

    bool onInv;

    //아이템을 클릭하여 나오는 선택창입니다
    public GameObject iamge;

    //현제 보관함이랑 충돌을 하고 있는지 알기 위한 bool 변수입니다
    public bool lockerClick;
    //중력 값입니다
    public float gravity = 9.8f;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
        //인벤토리가 켜지면 바로 탈출하여 아래 코드를 막는다
        //또 한 툴을 열었을때도 아래 코드를 실행하는 것을 막아 줘야한다
        if (inv.activeSelf || myMoveban)
        {
            return;
        }

        if(iamge.activeSelf == true)
        {
            //아이템설정 창이 열려있으면 닫아준다 
            iamge.SetActive(false);
        }


        if (cc.isGrounded)
        {
            myDir = new Vector3(h, 0, v);
            myDir = transform.TransformDirection(myDir);
            myDir *= speed;
        }
        //GetAxisRaw 사용한 이유는 키보드로 작동하는 게임이기 때문에 0 1 -1 이외의 값은
        //불필요합니다
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");



        myDir.y -= gravity * Time.deltaTime;
        cc.Move(myDir * Time.deltaTime);
        MouseXRot();
    }

    //마우스 x 좌표 값을 받을 변수입니다
    float mouseX;
    //회전 속도 입니다
    public float rotSpeed = 200f;
    void MouseXRot()
    {
        mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(mouseX * rotSpeed * transform.up);
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


    //-------------------------------------------------------------------------------------------
    //이 아래 함수들은 플레이어의 이동 반경에 영향을 주는 함수들입니다

    //이 변수는 기지에서 나가는 순간 true되어
    bool startSubAir;


    //플레이어들은 기지 밖이면 산소가 계속해서 줄어들어 마지막에는 질식사를 하게 됩니다
    void Air()
    {

    }
}
