using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anims : MonoBehaviour
{
    CalorieLevle cl;
    PlayerHP ph;
    ItemManager im;
    OxygenLevle ol;
    Check_In_Outside cio;

    public GameObject rayStart;
    public float rayDis;
    public float angle;

    public J_Item item;

    RaycastHit rayHit;

    MeshRenderer mr;

    // 공장주소
    public GameObject potatoFactory;
    public GameObject carrotFactory;
    public GameObject turnipFactory;
    //Check_IN_OUT 스크립트에서 애니메이션 트리거를 발동시키면 애니메이션 동작을 하고,
    //동작할동안 그안에서 이벤트로 기능을 하는 함수를 호출하고 싶다.

    private void Start()
    {
        cl = gameObject.GetComponentInParent<CalorieLevle>();
        ph = gameObject.GetComponentInParent<PlayerHP>();
        im = gameObject.GetComponentInParent<ItemManager>();
        ol = gameObject.GetComponentInParent<OxygenLevle>();
        cio = gameObject.GetComponentInParent<Check_In_Outside>();
    }

    //감자심는 함수
    void Plant_Potato() => PlantCrops(potatoFactory, ref im.potatoCount);
    //감자먹을때
    void Eat_Potato()
    {
        cl.h_CurrentHunger += 5f;
        for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            if (item.itemName == J_ItemManager.j_Item.items2[i].itemName)
            {
                J_ItemManager.j_Item.items2[i].auount--;
            }
        }
    }

    //당근심는 함수
    void Plant_Carrot() { 
    PlantCrops(carrotFactory, ref im.carrotCount);
    }
    //당근 먹을때
    void Eat_Carrot()
    {
        cl.h_CurrentHunger += 7f;
        for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            if (item.itemName == J_ItemManager.j_Item.items2[i].itemName)
            {
                J_ItemManager.j_Item.items2[i].auount--;
            }
        }
    }

    //순무
    void Plant_Turnip() => PlantCrops(turnipFactory, ref im.carrotCount);
    //순무 먹을때
    void Eat_Turnip()
    {
        cl.h_CurrentHunger += 3f;
        for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            if (item.itemName == J_ItemManager.j_Item.items2[i].itemName)
            {
                J_ItemManager.j_Item.items2[i].auount--;
            }
        }
    }

    //물 마실때
    void Drink_Water()
    {
        cl.t_CurrentThirsty += 10f;
        for (int i = 0; i < J_ItemManager.j_Item.items2.Length; i++)
        {
            if (item.itemName == J_ItemManager.j_Item.items2[i].itemName)
            {
                J_ItemManager.j_Item.items2[i].auount--;
            }
        }
    }

    //힐팩 쓸때
    void Healling() => ph.m_CurrentHealth += 50f;

    //산소팩 쓸때
    void Oxygen() => ol.m_CurrentOxygen = ol.m_StartOxygen;

    //체온팩 쓸때
    void Temperature() => ol.b_CurrentTemperature = ol.b_StartTemperature;

    //밖에서 삽 쓸때
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

    //안에서 삽쓸때
    public void Shovel_INSIDE()
    {
        Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
        Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.red);

        if (Physics.Raycast(ray, out rayHit, rayDis, (1 << 12)))
        {
            mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
            if (mr.enabled == false) mr.enabled = true;
        }
    }
    
    //물 뿌릴때
    public void SprinkleWater()
    {
        
        Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
        Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.blue);

        if (Physics.Raycast(ray, out rayHit, rayDis, (1 << 12)) && ItemManager.instance.waterCount > 0)
        {
            MeshRenderer mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
            mr.material.color = new Color(0.3867925f, 0.1446878f, 0.1112941f);

            //물 카운트를 줄인다.
            ItemManager.instance.waterCount--;
            
            //흙의 상태를 젖음으로 바꿔준다
            SoilCondition sc = rayHit.transform.gameObject.GetComponent<SoilCondition>();
            sc.state = SoilCondition.AmIWet.Yes;
        }
    }

    //작물심기
    public void PlantCrops(GameObject plantCrops, ref int crops)
    {
        Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
        Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.green);

        if (Physics.Raycast(ray, out rayHit, rayDis, (1 << 12)))
        {
            GameObject PreparedLand = rayHit.transform.gameObject;
            mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
            if (PreparedLand.transform.childCount <= 0 && mr.enabled == true && crops > 0)
            {
                GameObject plant = Instantiate(plantCrops);
                plant.transform.position = PreparedLand.transform.position;
                plant.transform.parent = PreparedLand.transform;
                //소지한 작물 갯수를 줄인다.
                crops--;
            }
            else return;
            //plant.transform.position = PreparedLand.transform.position;
        }
    }

    //다시 움직일 수 있게 불값을 초기화해준다.
    void ReturnBool()
    {
        cio.doingAnim = false;
    }
}
