using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_ItemInformationManager : MonoBehaviour
{
    public static J_ItemInformationManager alls;
    private void Awake()
    {
        alls = this;
    }


    //이 스크립트는 단지 아이템의 정보를 가지고 있습니다 
    public GameObject[] allItems;
}
