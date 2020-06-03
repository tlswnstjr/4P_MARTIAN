using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_LockerInventroy : MonoBehaviour
{
    //이 리스트 아이템 보관 및 정보를 담을 변수리스트입니다
    public List<GameObject> items = new List<GameObject>();

    //이 스크립트는 인벤토리 스크립트 입니다 
    //즉 플레이어가 확득한 아이템을 관리 해줍니다.



    // Update is called once per frame
    void Update()
    {
        //아이템 메니저에서 데이터를 받아옵니다
        ItemInformationInfo();
        //이 함수는 아이템이 사라지면 실행됩니다
        ClicksItemManagers();
        //각 슬롯의 버튼을 활성화시켜줍니다
        ButtonAction();
    }

    //아이템 매니저한테 정보를 받아와서 그걸 자기 리스트 애들한테 할당해줍니다
    void ItemInformationInfo()
    {
        
        for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (J_ItemManager.j_Item.items2[i] != null && items[j].GetComponent<J_Slots>().name == null)
                {
                    items[i].GetComponent<J_Slots>().mainIamge.SetActive(true);
                    items[i].GetComponent<J_Slots>().MySeilf(J_ItemManager.j_Item.items2[i].itemName,
                        J_ItemManager.j_Item.items2[i].itemImage,
                        J_ItemManager.j_Item.items2[i].auount);
                }
            }
        }

    }
    //이 함수는 아이템 메니저에서 변경된 사항을 슬롯에게 적용해주는 역활을 해줍니다
    void ClicksItemManagers()
    {
        for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            if (J_ItemManager.j_Item.items2[i] == null)
            {
                items[i].GetComponent<J_Slots>().MySeilf(null, null, 0);
                items[i].GetComponent<J_Slots>().mainIamge.SetActive(false);
            }
        }
    }

    void ButtonAction()
    {
        for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            if (J_ItemManager.j_Item.items2[i] != null)
            {
                items[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                items[i].GetComponent<Button>().interactable = false;

            }
        }
    }

}
