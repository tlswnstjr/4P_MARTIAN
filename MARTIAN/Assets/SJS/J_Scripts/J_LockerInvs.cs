using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_LockerInvs : MonoBehaviour
{

    public static J_LockerInvs j_LockerInvs;
    //이 리스트 아이템 보관 및 정보를 담을 변수리스트입니다
    public List<J_Slots> items = new List<J_Slots>();

    private void Awake()
    {
        j_LockerInvs = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
