using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    //힐팩을 저장할 갯수
    public int healPackCount;
    //산소팩을 저장할 갯수
    public int oxygenPackCount;
    //체온팩을 저장할 갯수
    public int bodyTemperaturePackCount;
    //감자를 저장할 갯수
    public int potatoCount = 6;
    //당근을 저장할 함수
    public int carrotCount = 6;
    //무를 저장할 함수
    public int radishCount = 6;

    //물을 저장할 함수
    public int waterCount = 30;

    CleaTheLand ctl;


    //디버프
    //탈진
    public bool h_Drained;
    //탈수
    public bool t_Dehydration;

    private void Awake()
    {
        if (instance == null) instance = this;
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
