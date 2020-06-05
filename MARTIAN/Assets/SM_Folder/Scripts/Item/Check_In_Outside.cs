using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region -----------======= Tag 를 따지는 Enum ========------
//외부에서 사용할 Enum
public enum OUTside
{
    Shovel,
    Drill,
    OxygenPack,
    BodyTemperaturePack,
    HeelPack,
    OUTsideEnd
}

//==============================

//내부에서 사용할 Enum
public enum INside
{
    Potato,
    Carrot,
    Turnip,
    Shovel,
    HeelPack,
    INsideEnd
}
#endregion

public class Check_In_Outside : MonoBehaviour
{
    #region -------===== IN/OUT 을 따지는 Enum ======-------
    public enum WhereIAm
    {
        OUTSIDE,
        INSIDE
    }

    public Transform hand;

    //차일드 카운트를 체크할 변수
    public bool isChecked;

    //차일드를 저장할 변수
    Transform childtr;
    #endregion

    //=======================================

    public WhereIAm state_INOUT;

    public OUTside state_I_AM_OUT;
    OUTside[] out_allState = new OUTside[(int)OUTside.OUTsideEnd];
    public INside state_I_AM_IN;
    INside[] in_allState = new INside[(int)INside.INsideEnd];

    // Start is called before the first frame update
    void Start()
    {
        out_allState[0] = OUTside.Shovel;
        out_allState[1] = OUTside.Drill;
        out_allState[2] = OUTside.OxygenPack;
        out_allState[3] = OUTside.BodyTemperaturePack;
        out_allState[4] = OUTside.HeelPack;

        in_allState[0] = INside.Potato;
        in_allState[1] = INside.Carrot;
        in_allState[2] = INside.Turnip;
        in_allState[3] = INside.Shovel;
        in_allState[4] = INside.HeelPack;

    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Test_OUTSIDE") state_INOUT = WhereIAm.OUTSIDE;
        if (SceneManager.GetActiveScene().name == "Test_INSIDE") state_INOUT = WhereIAm.INSIDE;
    }

    // Update is called once per frame
    void Update()
    {
        Switch_INOUT();
    }

    public void Switch_INOUT()
    {
        switch (state_INOUT)
        {
            case WhereIAm.OUTSIDE:
                //손의 차일드가 0보다 크다면
                if (hand.childCount >= 0 && isChecked == false)
                {
                    //차일드의 타입을 검사하는 함수
                    WHAT_KIND_OF_ARE_YOU_OUT();
                    //내 차일드를 저장하겠어
                    childtr = hand.GetChild(0).transform;
                    isChecked = true;
                }
                break;
            case WhereIAm.INSIDE:
                //손의 차일드가 0보다 크다면
                if (hand.childCount >= 0 && isChecked == false)
                {
                    //차일드의 타입을 검사하는 함수
                    WHAT_KIND_OF_ARE_YOU_IN();
                    isChecked = true;
                }
                break;
        }
    }

    public void Switch_OUTSIDE()
    {
        switch (state_I_AM_OUT)
        {
            case OUTside.Shovel:
                break;
            case OUTside.Drill:
                break;
            case OUTside.BodyTemperaturePack:
                break;
            case OUTside.HeelPack:
                break;
            case OUTside.OxygenPack:
                break;
        }
    }

    public void Switch_INSIDE()
    {
        switch (state_I_AM_IN)
        {
            case INside.Potato:
                break;
            case INside.Carrot:
                break;
            case INside.Turnip:
                break;
            case INside.Shovel:
                break;
            case INside.HeelPack:
                break;
        }
    }

    private void WHAT_KIND_OF_ARE_YOU_OUT()
    {
        //차일드의 태그에 따라 스테이트를 분류하고 싶다.
        for (int i = 0; i < out_allState.Length; i++)
        {
            if (gameObject.tag == out_allState[i].ToString())
            {

            }
        }
    }

    void WHAT_KIND_OF_ARE_YOU_IN()
    {

    }
}
