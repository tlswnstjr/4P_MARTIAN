using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class J_ItemManager : MonoBehaviourPun
{
   public static J_ItemManager j_Item;

    //2차원 배열입니다 
    //앞의 숫자는 플레이어 수 
    // 뒤의 자리수는 아이템 보관가능수입니다
    public J_Item[] items2 = new J_Item[ 21];
    //아이템의 정보를 관리해 줄 변수배열입니다
    //public GameObject[] items;

    public GameObject inv;
    private void Awake()
    {
        //inv = J_Inventory.j_Inventory.gameObject;

    }


    // Start is called before the first frame update
    void Start()
    {
       /* if (photonView.IsMine)
        {
            j_Item = this;
        }
        else
        {

        }*/
            j_Item = this;

    }

    // Update is called once per frame
    void Update()
    {
       
        if (inv.activeSelf)
        {
            for (int i = 0; i < items2.Length; i++)
            {
                J_Slots s = J_Inventory.j_Inventory.items[i].GetComponent<J_Slots>();
                if(items2[i] !=null)
                {
                    s.itemMy = items2[i].my;        
                    s.mainIamge.SetActive(true);
                    s.Image.sprite = items2[i].itemImage;
                    s.text.text = items2[i].auount.ToString();
                    s.names = items2[i].itemName;
                }   
                else
                {
                    s.MySeilf(null, null, 0);
                    s.mainIamge.SetActive(false);
                }
            }
        }  
    }


    //눈에 보이지는 않지만 내부적으로 인ㅂ넨토리 안을 채워줍니다 
    //[PunRPC]
    public void ClicksItem(GameObject x)
    {
        for (int i = 0; i < items2.Length; i++)
        {
            J_Item j_Item = x.GetComponent<J_Item>();
            //가장 먼저 배열 클래스에 자신과 동일한 것이 있는지 확인해줍니다 
            //있으면 갯수만 늘려주고 탈출합니다
            if (items2[i] != null)
            {
                if (items2[i].itemName == j_Item.itemName)
                {
                    //이름이 동일하지만 타입이 만약에 무기이면
                    if(j_Item.type == J_Item.ItemType.WEAPON)
                    {
                        continue;
                    }
                    items2[i].auount += j_Item.auount;
                    Destroy(x);
                    break;
                }
            }
            //해당 배열클래스의이름이 비어있으면 그 배열 클래스로 정해줍니다
            else if (items2[i] == null)
            {
                items2[i] = j_Item;
                items2[i].auount = x.GetComponent<J_Item>().auount;
                items2[i].itemImage = j_Item.itemImage;
                items2[i].my = x;
                //받아드려오는 오브젝트의 내부속 이름
                // items2[i].itemName = j_Item.itemName;
                //오브젝트의 이미지 스프라이트 설정해줍니다
                // items2[i].itemImage = j_Item.itemImage;
                // items2[i].auount++;
                break;
            }
        }
    }
}
