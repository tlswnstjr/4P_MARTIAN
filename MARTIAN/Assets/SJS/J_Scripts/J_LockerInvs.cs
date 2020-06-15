using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class J_LockerInvs : MonoBehaviourPun
{

    public static J_LockerInvs j_LockerInvs;
    //이 리스트 아이템 보관 및 정보를 담을 변수리스트입니다
    public List<J_Slots> items = new List<J_Slots>();

    private void Awake()
    {
        j_LockerInvs = this;
    }

    [PunRPC]
    void InvOutAndLockerGO(int slotNmb, int amount)
    {
        J_Slots slotss = 
            J_Inventory.j_Inventory.items[slotNmb].GetComponent<J_Slots>();

        J_SlotButtons j_SlotButtons = slotss.GetComponent<J_SlotButtons>();
        for (int i = 0; i < J_LockerInvs.j_LockerInvs.items.Count; i++)
        {
            J_Slots js = J_LockerInvs.j_LockerInvs.items[i].GetComponent<J_Slots>();
            if (js.names == "")
            {
                
                if (J_ItemManager.j_Item.items2[slotss.myWhyNub].type != J_Item.ItemType.WEAPON)
                {
                    js.mainIamge.SetActive(true);
                    js.MySeilf(slotss.name, slotss.Image.sprite, amount +
                    int.Parse(js.text.text));

                    J_ItemManager.j_Item.items2[slotss.myWhyNub].auount -=
                     j_SlotButtons.clickButton.GetComponent<J_SclectButton>().ss;
                }
                else
                {
                    js.MySeilf(slotss.names, slotss.Image.sprite, amount);
                    js.mainIamge.SetActive(true);
                    J_ItemManager.j_Item.items2[slotss.myWhyNub] = null;
                }
                
                break;
            }
            else if ( J_LockerInvs.j_LockerInvs.items[i].names == slotss.names)
            {
                //아이템 메니저의 정보에 접근하여 지금 인벤토리에 있는 아이템의 타입이 장비 템인지 확인하고 맞으면 for다음 인덱스로 넘어
                //갑니다
                if (J_ItemManager.j_Item.items2[slotss.myWhyNub].type != J_Item.ItemType.WEAPON)
       
                {
                    js.MySeilf(
                       slotss.name,
                       slotss.Image.sprite,
                    amount +
                    int.Parse(js.text.text));

                    J_ItemManager.j_Item.items2[slotss.myWhyNub].auount -=
                     j_SlotButtons.clickButton.GetComponent<J_SclectButton>().ss;
              
                }
                else
                {
                    J_ItemManager.j_Item.items2[slotss.myWhyNub] = null;
               
                }
                j_SlotButtons.ButtonClicks();
                break;
            }

         //   break;


        }
    }
}
