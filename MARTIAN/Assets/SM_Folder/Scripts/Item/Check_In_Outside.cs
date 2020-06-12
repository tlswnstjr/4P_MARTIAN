using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

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
    WATER,
    INsideEnd
}
#endregion

public class Check_In_Outside : MonoBehaviourPun
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
    public Transform childtr;
    #endregion

    //=======================================

    public WhereIAm state_INOUT;

    public OUTside state_I_AM_OUT;
    OUTside[] out_allState = new OUTside[(int)OUTside.OUTsideEnd];
    public INside state_I_AM_IN;
    INside[] in_allState = new INside[(int)INside.INsideEnd];

    //=============

        //레이저
    public GameObject rayStart;
    public float rayDis;
    public float angle;
    RaycastHit rayHit;

    // 공장주소
    public GameObject potatoFactory;
    public GameObject carrotFactory;
    public GameObject radishFactory;

    //인벤토리를 열었는지 체크할 불변수
    public bool isInven;
    //애니메이션중일때 입력을 막을 불변수
    public bool doingAnim;
    //드릴링이 가능한지 여부를 판단해줄 불값
    public bool can_I_Drilling = false;

    //외부 스크립트를 저장할 변수
    public ItemManager im;
    public PlayerHP ph;
    public OxygenLevle ol;
    public J_Coppers jacob;
    Animator anim;

    float currntT;


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


        im = gameObject.GetComponent<ItemManager>();
        ph = gameObject.GetComponent<PlayerHP>();
        ol = gameObject.GetComponent<OxygenLevle>();
        anim = GetComponentInChildren<Animator>();

        state_I_AM_IN = INside.INsideEnd;
        state_I_AM_OUT = OUTside.OUTsideEnd;
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "SM_Map")
        {
            state_INOUT = WhereIAm.OUTSIDE;
        }
        if (SceneManager.GetActiveScene().name == "Test_INSIDE")
        {
            state_INOUT = WhereIAm.INSIDE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //안과 밖에따라서 사용할 Enum을 분류해줄 함수
        Switch_INOUT();
        if (nope)
        {
            //밖에 있을때 사용할 아이템들을 정리해놓은 함수
            Switch_OUTSIDE();
        }
        if (yea)
        {
            //안에 있을때 사용할 아이템들을 정리해놓은 함수
            Switch_INSIDE();

        }
        //if (photonView.IsMine)
        //{
        if (hand.childCount > 0 && Input.GetKeyDown(KeyCode.O))
        {
            Destroy(childtr.gameObject);
            isChecked = false;
            anim.SetTrigger("Tuck tool");
        }
        //}
    }

    bool nope = false;
    bool yea = false;
    public void Switch_INOUT()
    {
        switch (state_INOUT)
        {
            case WhereIAm.OUTSIDE:
                nope = true;
                //손의 차일드가 0보다 크다면
                if (hand.childCount > 0 && isChecked == false)
                {

                    //내 차일드를 저장하겠어
                    childtr = hand.GetChild(0).transform;
                    //내 손의 태그를 차일드의 태그와 똑같게 바꾼다
                    hand.tag = childtr.tag;
                    //차일드의 타입을 검사하는 함수
                    WHAT_KIND_OF_ARE_YOU_OUT();

                    isChecked = true;
                }
                break;
            case WhereIAm.INSIDE:
                yea = true;
                //손의 차일드가 0보다 크다면
                if (hand.childCount > 0 && isChecked == false)
                {
                    //내 차일드를 저장하겠어
                    childtr = hand.GetChild(0).transform;
                    //내 손의 태그를 차일드의 태그와 똑같게 바꾼다
                    hand.tag = childtr.tag;
                    //차일드의 타입을 검사하는 함수
                    WHAT_KIND_OF_ARE_YOU_IN();

                    isChecked = true;
                }
                break;
        }
    }

    //밖에꺼
    private void WHAT_KIND_OF_ARE_YOU_OUT()
    {
        //차일드의 태그에 따라 스테이트를 분류하고 싶다.
        for (int i = 0; i < out_allState.Length; i++)
        {
            if (hand.tag == out_allState[i].ToString())
            {
                state_I_AM_OUT = out_allState[i];
            }
        }
    }

    //안에꺼
    void WHAT_KIND_OF_ARE_YOU_IN()
    {
        for (int i = 0; i < in_allState.Length; i++)
        {
            if (hand.tag == in_allState[i].ToString())
            {
                state_I_AM_IN = in_allState[i];

            }
        }
    }
    bool a;
    #region -----------====== 밖에서 사용할 아이템들의 함수 ========----------
    public void Switch_OUTSIDE()
    {
        switch (state_I_AM_OUT)
        {
            case OUTside.Shovel:
                if (isInven || doingAnim) return;
                //삽의 바깥에서의 기능
                if (Input.GetButtonDown("Fire1")) {
                    /*Shovel_OUTSIDE();*/
                    anim.SetTrigger("Outside_Shovel");
                    doingAnim = true;
                } 
                break;
            case OUTside.Drill:
                {
                    if (can_I_Drilling)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            currntT += Time.deltaTime;
                            if (a == false)
                            {
                                anim.SetBool("Drilling", true);
                                a = true;
                            }

                            if (currntT > 2)
                            {
                                anim.SetBool("Drilling", false);

                                PhotonView pr = jacob.gameObject.GetComponent<PhotonView>();
                                pr.RPC("explode", RpcTarget.All);
                                currntT = 0;
                                a = false;
                                can_I_Drilling = false;
                            }
                        }
                    }
                    else
                    {
                        anim.SetBool("Drilling", false);
                        currntT = 0;
                        a = false;
                    }
                  
                }
                //if (isInven || doingAnim)
                //    return;
                ////콜리전엔터된 녀석에게서 J_Coppers를 찾아올 수 있다면
                //if (can_I_Drilling)
                //{
                //    if (Input.GetKeyDown(KeyCode.E)) { 
                //   
                //        doingAnim = true;
                //    }
                //   if (Input.GetKey(KeyCode.E))
                //    {
                //        currntT += Time.deltaTime;
                //        if (currntT > 1)
                //        {
                //            anim.SetBool("Drilling", false);
                //            jacob.Ore();
                //            doingAnim = false;
                //        }
                //    }
                //}
                break;
            case OUTside.BodyTemperaturePack:
                if (isInven || doingAnim) return;
                if (Input.GetKeyDown(KeyCode.E) && ol.b_CurrentTemperature < ol.b_StartTemperature && ItemManager.instance.bodyTemperaturePackCount > 0)
                {
                    //ol.b_CurrentTemperature = ol.b_StartTemperature;
                    anim.SetTrigger("Use_TemperaturePack");
                    doingAnim = true;
                }
                break;
            case OUTside.HeelPack:
                if (isInven || doingAnim) return;
                //E버튼을 누르면 ItemManger의 힐팩 카운트가 0보다 클때만 피를 회복하겠다.
                if (Input.GetKeyDown(KeyCode.E) && ph.m_CurrentHealth < ph.m_StartingHealth && ItemManager.instance.healPackCount > 0)
                {
                    anim.SetTrigger("Use_HealPack");
                    doingAnim = true;
                }
                break;
            case OUTside.OxygenPack:
                if (isInven || doingAnim) return;
                if (Input.GetKeyDown(KeyCode.E) && ol.m_CurrentOxygen < ol.m_StartOxygen && ItemManager.instance.oxygenPackCount > 0)
                {
                    anim.SetTrigger("Use_OxygerPack");
                    doingAnim = true;
                }
                break;
            case OUTside.OUTsideEnd:
                break;
        }
    }

    //밖에서 사용할 삽
    public void Shovel_OUTSIDE()
    {
        Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
        Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.red);

        if (Physics.Raycast(ray, out rayHit, rayDis, (1 << 12)))
        {
            MeshRenderer mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
            if (mr.enabled == true) mr.enabled = false;
        }
    }

    #endregion
    //===============================
    #region -----------=========== 내부에서 사용할 아이템 함수 =========-------------

    public void Switch_INSIDE()
    {
        switch (state_I_AM_IN)
        {
            case INside.Potato:
                if (isInven || doingAnim) return;
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("Plant_Potato");/*PlantCrops(potatoFactory, ref im.potatoCount);*/
                    doingAnim = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetTrigger("Eat_Potato"); /*cl.h_CurrentHunger += 5f;*/
                    doingAnim = true;
                }
                break;
            case INside.Carrot:
                if (isInven || doingAnim) return;
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("Plant_Carrot");/*PlantCrops(carrotFactory, ref im.carrotCount);*/
                    doingAnim = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetTrigger("Eat_Carrot");/*cl.h_CurrentHunger += 7f;*/
                    doingAnim = true;
                }
                break;
            case INside.Turnip:
                if (isInven || doingAnim) return;
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("Plant_Turnip"); /*PlantCrops(radishFactory, ref im.radishCount);*/
                    doingAnim = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetTrigger("Eat_Turnip");/*cl.h_CurrentHunger += 3f;*/
                    doingAnim = true;
                }
                break;
            case INside.Shovel:
                if (isInven || doingAnim) return;
                //삽의 내부에서의 기능
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("Inside_Shovel");/*Shovel_INSIDE();*/
                    doingAnim = true;
                }
                break;
            case INside.HeelPack:
                if (isInven || doingAnim) return;
                //E버튼을 누르면 ItemManger의 힐팩 카운트가 0보다 클때만 피를 회복하겠다.
                if (Input.GetKeyDown(KeyCode.E) && ph.m_CurrentHealth < ph.m_StartingHealth && ItemManager.instance.healPackCount > 0)
                {
                    Debug.Log("111111111111111111111");
                    anim.SetTrigger("Use_HealPack");
                    doingAnim = true;
                }
                break;
            case INside.WATER:
                if (isInven || doingAnim) return;
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("SprinkleWater");/*SprinkleWater(ref im.waterCount);*/
                    doingAnim = true;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetTrigger("Drink_Water");/*cl.t_CurrentThirsty += 10f;*/
                    doingAnim = true;
                }
                break;
            case INside.INsideEnd:
                break;
        }
    }

    ////감자심기
    //public void PlantCrops(GameObject plantCrops, ref int crops)
    //{
    //    Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
    //    Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.green);

    //    if (Physics.Raycast(ray, out rayHit, rayDis, (1 << 12)))
    //    {
    //        GameObject PreparedLand = rayHit.transform.gameObject;
    //        mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
    //        if (PreparedLand.transform.childCount <= 0 && mr.enabled == true && crops > 0)
    //        {
    //            GameObject plant = Instantiate(plantCrops);
    //            plant.transform.position = PreparedLand.transform.position;
    //            plant.transform.parent = PreparedLand.transform;
    //            //소지한 작물 갯수를 줄인다.
    //            crops--;
    //        }
    //        else return;
    //        //plant.transform.position = PreparedLand.transform.position;
    //    }
    //}

    ////땅에 물주기
    //public void SprinkleWater(ref int waterCount)
    //{
    //    Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
    //    Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.blue);

    //    if (Physics.Raycast(ray, out rayHit, rayDis, (1 << 12)) && waterCount > 0)
    //    {
    //        MeshRenderer mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
    //        mr.material.color = new Color(0.3867925f, 0.1446878f, 0.1112941f);

    //        //물 카운트를 줄인다.
    //        waterCount--;
    //        //흙의 상태를 젖음으로 바꿔준다
    //        SoilCondition sc = rayHit.transform.gameObject.GetComponent<SoilCondition>();
    //        sc.state = SoilCondition.AmIWet.Yes;
    //    }
    //}
    MeshRenderer mr;
    //public void Shovel_INSIDE()
    //{
    //    Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
    //    Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.red);

    //    if (Physics.Raycast(ray, out rayHit, rayDis, (1 << 12)))
    //    {
    //        mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
    //        if (mr.enabled == false) mr.enabled = true;
    //    }
    //}
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        //충돌한녀석한테서 J_Coppers의 향기가 느껴진다면?
        if (collision.transform.GetComponent<J_Coppers>())
        {
            can_I_Drilling = true;
            jacob = collision.transform.GetComponent<J_Coppers>();
        }
    }

    CropsGrowUP cgu;
    private void OnCollisionStay(Collision collision)
    {
        //충돌하는 녀석한테서 CropseGrowUP의 향기가 느껴진다면?
        if (collision.transform.GetComponentInChildren<CropsGrowUP>())
        {
            cgu = collision.transform.GetComponentInChildren<CropsGrowUP>();
            if (Input.GetKeyDown(KeyCode.C)) anim.SetTrigger("Pull UP");
        }
    }
    public void Spawn()
    {
        cgu.SpawnCrops(cgu.cropFactory);
    }

    private void OnCollisionExit(Collision collision)
    {
        //떨어져나간게 광물이면
        if (collision.gameObject.tag == "Mineral") can_I_Drilling = false;
    }
}
