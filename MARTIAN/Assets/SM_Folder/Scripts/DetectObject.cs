using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MYTAG
{
    HeelPack,
    BodyTemperaturePack,
    OxygenPack,
    TheEnd
}
public class DetectObject : J_Item
{
    public MYTAG mytag;
    MYTAG[] all_mytag = new MYTAG[(int)MYTAG.TheEnd];

    private Vector3 m_SetMyPosition;
    Transform childTransform;
    GameObject parent;

    bool isPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        childTransform = transform.gameObject.GetComponentInChildren<Transform>();
        //parent = gameObject.transform.parent.gameObject;

        all_mytag[0] = MYTAG.HeelPack;
        all_mytag[1] = MYTAG.BodyTemperaturePack;
        all_mytag[2] = MYTAG.OxygenPack;

        WhichOneMyTag();
    }

    private void WhichOneMyTag()
    {
        for (int i = 0; i < all_mytag.Length; i++)
        {
            if (gameObject.tag == all_mytag[i].ToString())
            {
                mytag = all_mytag[i];
            }
        }
    }

    void SWITCH()
    {
        switch (mytag)
        {
            case MYTAG.HeelPack:
                if (isPlayer)
                {
                    //줍기위해 F키를 누르자
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //나를 지우고 아이템 매니져에게 힐팩카운트를 1 한다.
                        ItemManager.instance.healPackCount += 1;
                        Destroy(parent);
                    }
                }
                break;
            case MYTAG.BodyTemperaturePack:
                if (isPlayer)
                {
                    //줍기위해 F키를 누르자
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //나를 지우고 아이템 매니져에게 힐팩카운트를 1 한다.
                        ItemManager.instance.bodyTemperaturePackCount += 1;
                        Destroy(parent);
                    }
                }
                break;
            case MYTAG.OxygenPack:
                if (isPlayer)
                {
                    //줍기위해 F키를 누르자
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //나를 지우고 아이템 매니져에게 힐팩카운트를 1 한다.
                        ItemManager.instance.oxygenPackCount += 1;
                        Destroy(parent);
                    }
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();

        SWITCH();

        aaa();
    }

    //내 위치를 차일드의 위치로 한다.
    private void SetPosition()
    {
        m_SetMyPosition = childTransform.localPosition;

        transform.localPosition = m_SetMyPosition;
    }
    //콜라이더에 닿은게 플레이어라면 나를 줍게하겠다.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayer = true;
        }

    }
}
