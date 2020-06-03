using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_Inventory : MonoBehaviour
{
    public static J_Inventory j_Inventory;
    //이 리스트 아이템 보관 및 정보를 담을 변수리스트입니다
    public List<GameObject> items = new List<GameObject>();
    
    //이 스크립트는 인벤토리 스크립트 입니다 
    //즉 플레이어가 확득한 아이템을 관리 해줍니다.
    // Start is called before the first frame update
    private void Awake()
    {
        j_Inventory = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClicksItemManagers();
        ButtonAction();
    }
   

    //이 함수는 아이템 메니저에서 변경된 사항을 슬롯에게 적용해주는 역활을 해줍니다
    void ClicksItemManagers()
    {
        for(int i =0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            if(J_ItemManager.j_Item.items2[i]== null)
            {
                items[i].GetComponent<J_Slots>().MySeilf(null, null, 0);
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


    //이 함수는 리스트의 모든 내용 물을 검사하여 중복 되는 것이 있으면 합쳐 줍니다

}