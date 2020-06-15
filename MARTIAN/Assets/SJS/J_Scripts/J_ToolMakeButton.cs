using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_ToolMakeButton : MonoBehaviour
{
    //이 스크립트도 싱글턴으로 만들어 줍니다
    public static J_ToolMakeButton j_ToolMakeButton;
    //이 스크립트는 재료가 만족할 시 클릭하면 그 완성품을 만들어서 밷어준다

    //자기자신의 버튼 컨퍼넌트를 받아옵니다
    Button button;

    private void Awake()
    {
        j_ToolMakeButton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        //툴창을 열고 닫을때 시작은 무조건 비활성화입니다
        button.interactable = false;
    }

    int clicks;
    //전체 조건이 만족하는지 확인하기 위한 bool문입니다
    bool[] makeClicks;

    //각 재료의 수량을 저장해줄 변수입니다
    int[] subAount;

    Transform player;
    J_ItemInformationManager allItem;
    public bool s = true;
    // Update is called once per frame
    void Update()
    {
        if(J_Mune.mune.buttonNmb != null)
        {
            //무슨 버튼이 클릭되어있으면 제작버튼을 활성화 시켜줍니다
            makeClicks = new bool[J_Mune.mune.buttonNmb.GetComponent<J_ToolButtonInfo>
                ().aountMat];
            subAount = new int[J_Mune.mune.buttonNmb.GetComponent<J_ToolButtonInfo>
                ().aountMat];
        }

        //메뉴에 선택되어 있는 버튼이 존재하면 다음 명령어를 실행하시오
        if(J_Mune.mune.buttonNmb != null)
        {
            ButtonDClicks();
            for (int i = 0; i < makeClicks.Length; i++)
            {
                //논리적 계산을 통한 true검사입니다
                s &= makeClicks[i];
            }

            if(s)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        //여기는 만들수 있는지 채크해주는 for문입니다 
        
    }

    public void Comelpete()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        allItem = GameObject.Find("ItemInformationManager").GetComponent<J_ItemInformationManager>();
        for (int i = 0; i < allItem.allItems.Length; i++)
        {
            J_Item _Item = allItem.allItems[i].GetComponent<J_Item>();
            if(_Item.itemName == J_Mune.mune.buttonNmb.GetComponent<J_ToolButtonInfo>().completeName)
            {
                print("완성품입니다");
                GameObject a = Instantiate(allItem.allItems[i]);
                a.transform.position = player.transform.position;
            }
        }
        
    }




    //버튼을 클릭하면 실행합니다
    public void ButtonDClicks()
    {
        //클릭한 버튼의 재료 정보를 할당해줄 변수 x입니다
        J_ToolButtonInfo x = J_Mune.mune.buttonNmb.GetComponent<J_ToolButtonInfo>();
        
        
        for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            if(J_ItemManager.j_Item.items2[i] != null)
            {
                for (int j = 0; j < x.names.Length; j++)
                {
                    //서로의 아이템 정보중 이름을 검사합니다
                    if (J_ItemManager.j_Item.items2[i].itemName == x.names[j])
                    {
                        indxNmb(x, i, j);
                    }
                }
            }           
        }
       
    }
    void indxNmb(J_ToolButtonInfo x, int i, int j)
    {
        if (J_ItemManager.j_Item.items2[i].auount >= x.spriteAount[j])
        {
            //확인한 배열의 값을 true로 만들어줍니다
            makeClicks[j] = true;
            subAount[j] = x.spriteAount[j];
        }
    }
    //위에 코드로만은 딱 하나만을 확인하기 대문 복수 계산을 위한 다른 함수가 필요합니다 

    //이제 여기서는 실제로 계산하는 곳입니다 
    public void ButtonActionSet()
    {
        clicks = makeClicks.Length;
        J_ToolButtonInfo x = J_Mune.mune.buttonNmb.GetComponent<J_ToolButtonInfo>();
        if (clicks == makeClicks.Length)
        {
            for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
            {
                if (J_ItemManager.j_Item.items2[i] != null)
                {
                    for (int j = 0; j < x.names.Length; j++)
                    {
                        //서로의 아이템 정보중 이름을 검사합니다
                        if (J_ItemManager.j_Item.items2[i].itemName == x.names[j])
                        {
                         
                            J_ItemManager.j_Item.items2[i].auount -= x.spriteAount[j];
                            x.OnButtons();
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    
}