using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_SlotButtons : MonoBehaviour
{
    //이 스크립트는 슬롯을 클릭하면 ui가 나타나는 스크립트입니다
    //나타나는 ui들은 여러가지 선택지를 고를수 있게 합니다 

    //지금 무슨 슬롯이 선택 되었는지 확인하기 위한 게임오브젝트입니다

    //자신이 몇번째 배열의 인덱스를 가지고 있는 지 알려주기위한 변수입니다
    public int myWhyNub;

    public enum State
    {
        MYINVENYROY,
        LOCKER,
        LOCKERINVENTROY
    }

    public State state;

    public GameObject infoItem;

    //지금 무슨 슬롯이 선택되었는지 알려주기 위한 변수입니다
    public GameObject _Slots;


    public GameObject slotButton;
    //실행을 위한 함수
    public Button button;
    //그냥 취소버튼
    public Button button2;
    //사용하기 버튼입니다
    public Button button3;


    Button clickButton;
    public GameObject player;


    //플레이어 아이템 사용시 나타날 곳을 받아 옵니다 
    public GameObject playerUseItem;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //아래 방법으로 사용하면 버튼을 생성하자마자 함수를 등록해줄수 있다
        //button.onClick.AddListener(() => print("버튼 클릭!"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClicks()
    {
        switch (state)
        {
            case State.MYINVENYROY:
                Myinv();
                break;
            case State.LOCKER:
                Locker();
                break;
            case State.LOCKERINVENTROY:
                LockerInv();
                break;
        }
    }

    //단지 자기 자신의 인벤토리를 열었다면 
    void Myinv()
    {
        Contents("버리기", "취소", "사용하기");
    }

    //창고를 열었다면
    void Locker()
    {
        Contents("빼내기", "취소", null);
    }

    //창고 인벤토리를 열었다면
    void LockerInv()
    {
        Contents("집어 넣기", "취소", null);
    }

    void Contents(string text1, string text2, string text3)
    {
        J_Item _Item = J_ItemManager.j_Item.items2[myWhyNub];
        if (slotButton.transform.childCount > 0)
        {
            for (int i = 0; i < slotButton.transform.childCount; i++)
            {
                Destroy(slotButton.transform.GetChild(i).gameObject);
            }
        }


        slotButton.SetActive(true);
        clickButton = Instantiate(button);
        //소환하면서 지금 누구의 아이템 정보를 전달해줍니다
        //시발 이걸 생각 못했네
        //현제 선택한 아이템이 원소태그가 아니면
        if (text3 != null && _Item.type != J_Item.ItemType.STONE)
        {
            Button a = Instantiate(button3);
            a.onClick.AddListener(Actions3);
            a.GetComponent<J_SclectButton>().scls = _Slots;
            a.GetComponentInChildren<Text>().text = text3;
            a.transform.SetParent(slotButton.transform);
        }
        clickButton.onClick.AddListener(Actions2);
        clickButton.GetComponent<J_SclectButton>().scls = _Slots;
        clickButton.GetComponentInChildren<Text>().text = text1;
        clickButton.transform.SetParent(slotButton.transform);

        Button b = Instantiate(button2);
        b.onClick.AddListener(Actions);
        b.GetComponentInChildren<Text>().text = text2;
        b.transform.SetParent(slotButton.transform);
    }


    void Actions()
    {
        slotButton.SetActive(false);
    }


    void Actions2()
    {
        #region 내 자신 인벤토리 
        J_Slots a = _Slots.GetComponent<J_Slots>();
        //자기 인벤토리에서 실행하는 상태입니다
        if (state == State.MYINVENYROY)
        {
            if (clickButton.GetComponent<J_SclectButton>().ss != a.itemMy.GetComponent<J_Item>().auount)
            {

                if (clickButton.GetComponent<J_SclectButton>().ss == 0)
                {
                    J_ItemManager.j_Item.items2[a.myWhyNub].auount -=
           clickButton.GetComponent<J_SclectButton>().ss;
                    GameObject x = Instantiate(a.itemMy);
                    x.SetActive(true);
                    x.transform.position = player.transform.position;
                    x.GetComponent<J_Item>().auount = clickButton.GetComponent<J_SclectButton>().ss;
                }
                else
                {
                    J_ItemManager.j_Item.items2[a.myWhyNub].auount -=
           clickButton.GetComponent<J_SclectButton>().ss;
                    GameObject x = Instantiate(a.itemMy);
                    x.SetActive(true);
                    x.transform.position = player.transform.position;
                    x.GetComponent<J_Item>().auount = clickButton.GetComponent<J_SclectButton>().ss;
                }

            }

            //만약 슬롯의 아이템 갯수가 0이면 그 자리를 비워줘야합니다
            else if (J_ItemManager.j_Item.items2[a.myWhyNub].auount ==
                clickButton.GetComponent<J_SclectButton>().ss)
            {
                //아이템이 없기 때문에 이미지도 비활성화 해줍니다 

                J_Inventory.j_Inventory.items[a.myWhyNub].GetComponent<J_Slots>().mainIamge.SetActive(false);

                //아이템이 없기 때문에 null값을 넣어줍니다
                J_ItemManager.j_Item.items2[a.myWhyNub] = null;
                a.itemMy.transform.position = player.transform.position;
                a.itemMy.GetComponent<J_Item>().auount = clickButton.GetComponent<J_SclectButton>().ss;
                a.itemMy.SetActive(true);

            }


        }
        #endregion
        #region
        /* for (int i = 0; i < 
                 J_ItemManager.j_Item.items2.Length; i++)
             {
                 if(J_ItemManager.j_Item.items2[i].itemName == a.name)
                 {


                     J_ItemManager.j_Item.items2[i].auount -=
                         clickButton.GetComponent<J_SclectButton>().ss;


                     if(clickButton.GetComponent<J_SclectButton>().ss != a.itemMy.GetComponent<J_Item>().auount)
                     {
                         GameObject x = Instantiate(a.itemMy);
                         x.transform.position = player.transform.position;
                         x.GetComponent<J_Item>().auount = clickButton.GetComponent<J_SclectButton>().ss;
                     }



                     //만약 슬롯의 아이템 갯수가 0이면 그 자리를 비워줘야합니다
                     if (J_ItemManager.j_Item.items2[i].auount ==0)
                     {
                         //아이템이 없기 때문에 이미지도 비활성화 해줍니다 

                         J_Inventory.j_Inventory.items[i].GetComponent<J_Slots>().mainIamge.SetActive(false);

                         //아이템이 없기 때문에 null값을 넣어줍니다
                         J_ItemManager.j_Item.items2[i] = null;

                     }
                     break;
                 }
             }
             */

        /* GameObject mm = Instantiate(a.itemMy);
         mm.SetActive(true);
         mm.GetComponent<J_Item>().auount = clickButton.GetComponent<J_SclectButton>().ss;
         mm.transform.position = player.transform.position;
         */

        #endregion



        else if (state == State.LOCKER)
        {
            J_Slots slotss = _Slots.GetComponent<J_Slots>();
            for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
            {

                //만약에 같은 이름이 있다면 거기에 넣어준다 
                if (J_ItemManager.j_Item.items2[i] != null && J_ItemManager.j_Item.items2[i].itemName == _Slots.GetComponent<J_Slots>().names)
                {
                    if(J_ItemManager.j_Item.items2[i].type == J_Item.ItemType.WEAPON)
                    {
                        continue;
                    }
                    J_ItemManager.j_Item.items2[i].auount += clickButton.GetComponent<J_SclectButton>().ss;

                    break;
                }
                //하지만 같은 이름이 존재하지 않고 자리가 남아있다면
                else if (J_ItemManager.j_Item.items2[i] == null)
                {
                    for (int j = 0; j < infoItem.GetComponent<J_ItemInformationManager>().allItems.Length; j++)
                    {
                        if (_Slots.GetComponent<J_Slots>().names ==
                            infoItem.GetComponent<J_ItemInformationManager>().allItems[j]
                            .GetComponent<J_Item>().itemName)
                        {
                            GameObject item = Instantiate(infoItem.GetComponent<J_ItemInformationManager>().allItems[j]);
                            item.transform.position = player.transform.position; 
                            J_Item j_Item = item.GetComponent<J_Item>();
                            J_ItemManager.j_Item.items2[i] = j_Item;
                            J_ItemManager.j_Item.items2[i].auount = clickButton.GetComponent<J_SclectButton>().ss;
                            J_ItemManager.j_Item.items2[i].itemImage = j_Item.itemImage;
                            J_ItemManager.j_Item.items2[i].my = item;
                            item.SetActive(false);
                            break; 
                        }
                    }
                    break;
                }
            }

            //이제 여기서 창고에 있는 아이를 죽인다

            slotss.MySeilf(slotss.name, slotss.Image.sprite, int.Parse(slotss.text.text) -
                clickButton.GetComponent<J_SclectButton>().ss);
            if (slotss.text.text == "")
            {
                slotButton.SetActive(false);
                slotss.MySeilf(null, null, 0);
                slotss.mainIamge.SetActive(false);
            }
        }


        //자신 인벤토리에서 창고로 넣어주는 상태입니다
        #region 자신 인벤에서 창고로

        else if (state == State.LOCKERINVENTROY)
        {
            J_Slots slotss = _Slots.GetComponent<J_Slots>();
            for (int i = 0; i < J_LockerInvs.j_LockerInvs.items.Count; i++)
            {
                J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().mainIamge.SetActive(true);
                if (J_LockerInvs.j_LockerInvs.items[i].names == "" || J_LockerInvs.j_LockerInvs.items[i].names == null)
                {
                    J_LockerInvs.j_LockerInvs.items[i].MySeilf(slotss.names, slotss.Image.sprite
                        , clickButton.GetComponent<J_SclectButton>().ss);
                }
                else if (J_LockerInvs.j_LockerInvs.items[i].names != "" && J_LockerInvs.j_LockerInvs.items[i].names == slotss.names)
                {
                    //아이템 메니저의 정보에 접근하여 지금 인벤토리에 있는 아이템의 타입이 장비 템인지 확인하고 맞으면 for다음 인덱스로 넘어
                    //갑니다
                    if (J_ItemManager.j_Item.items2[slotss.myWhyNub].type == J_Item.ItemType.WEAPON)
                    {
                        continue;
                    }
                    else
                    {
                        J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().MySeilf(
                           slotss.name,
                           slotss.Image.sprite,
                        clickButton.GetComponent<J_SclectButton>().ss +
                        int.Parse(J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().text.text));
                    }
                }
                else
                {
                    continue;
                }

                //이 위까지 창고로 넣어주는 명령어입니다

                //아래는 이제 인벤토리에서 지우는 명령어 입니다

                if (J_ItemManager.j_Item.items2[slotss.myWhyNub].type == J_Item.ItemType.WEAPON)
                {
                    J_ItemManager.j_Item.items2[slotss.myWhyNub] = null;
                    ButtonClicks();
                }
                else
                {
                    J_ItemManager.j_Item.items2[slotss.myWhyNub].auount -= clickButton.GetComponent<J_SclectButton>().ss;
                    if(J_ItemManager.j_Item.items2[slotss.myWhyNub].auount==0)
                    {
                        J_ItemManager.j_Item.items2[slotss.myWhyNub] = null;
                    }
                    ButtonClicks();
                }
                break;
            }
           

        }
        #endregion

    }


    //이 함수는 인벤토리와 동일하게 사용하는 함수입니다 
    // 그러면 인덱스도 인벤토리 및 아이템 메니저랑 동일합니다 
    //그러면 그 번호에 있는 아이를 가지고 오면 됩니다 
    void Actions3()
    {
        playerUseItem = GameObject.FindGameObjectWithTag("PlayerUseItemPos");
        J_Slots a = _Slots.GetComponent<J_Slots>();
        J_Item j_Item = J_ItemManager.j_Item.items2[a.myWhyNub];
        j_Item.my.SetActive(true);
        j_Item.my.GetComponent<Rigidbody>().useGravity = false;
        j_Item.my.GetComponent<Rigidbody>().isKinematic = true;
        if (j_Item.my.GetComponent<MeshCollider>() != null)
        {
            j_Item.my.GetComponent<MeshCollider>().isTrigger = true;
        }
        j_Item.my.transform.position = playerUseItem.transform.position;
        j_Item.my.transform.rotation = playerUseItem.transform.rotation;
        j_Item.my.gameObject.transform.parent = playerUseItem.transform;

        J_Inventory.j_Inventory.gameObject.SetActive(false);
    }
}




/* if(J_LockerInvs.j_LockerInvs.items[slotss.myWhyNub] != null)
                {                   
                    //int.Parse(gameObject.GetComponent<J_Slots>().text.text)
                    //J_LockerInvs.j_LockerInvs.items[i] = gameObject.GetComponent<J_Slots>();
                   J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().mainIamge.SetActive(true);
                    if (J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().text.text == "")
                    {
                        J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().MySeilf(
                            gameObject.GetComponent<J_Slots>().name,
                      gameObject.GetComponent<J_Slots>().Image.sprite,
                      clickButton.GetComponent<J_SclectButton>().ss);
                    }
                    else if(J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().text.text != "" && slotss.name ==
                        J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().name)
                    {
                        J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().MySeilf(
                            gameObject.GetComponent<J_Slots>().name,
                      gameObject.GetComponent<J_Slots>().Image.sprite,
                      clickButton.GetComponent<J_SclectButton>().ss + 
                      int.Parse(J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>().text.text));
                    }
                    else
                    {
                        continue;
                    }*/
    
    
