using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class J_Slots : J_SlotButtons
{
    //이 스크립트는 슬롯을 관리 해주는 스크립트입니다 
    //이 스크립트와 인벤토리 스크립트는 같이 사용 됩니다

    public J_Item _Item;

    

    public GameObject mainIamge;
    public Image Image;
    public Text text;
    public string names;

    //현제 자신이 클릭 되엇다는걸 알리는 명령문입니다 
    public GameObject itemMy;


    Button buttons;
    private void Awake()
    {
        text.text = null;
        buttons = GetComponent<Button>();
        buttons.onClick.AddListener(ButtonClick);

    }
    private void Start()
    {
    }
    public void MySeilf(string IName, Sprite IIamge, int sum)
    {
        names = IName;
        Image.sprite = IIamge;

        text.text = sum.ToString();
        if (sum ==0)
        {
            text.text ="";
        }
    }


    //이 함수는 버튼을 클릭하면 나오는 이미지나 사용 법등을 출력해줄 ui사용 버튼입니다

    public void ButtonClick()
    {
        //현제 자기 자신이 클릭된걸 알려준다 
        infoItem = GameObject.Find("ItemInformationManager");
        _Slots = gameObject;
        ButtonClicks();

    }

    [PunRPC]
    void InvOutAndLockerGO()
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
           
            break;
          
            
        }
    }
}
