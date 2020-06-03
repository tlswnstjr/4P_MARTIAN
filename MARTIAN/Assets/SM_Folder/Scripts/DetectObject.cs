using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    private Vector3 m_SetMyPosition;
    Transform childTransform;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        childTransform = transform.gameObject.GetComponentInChildren<Transform>();
        parent = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
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
            //줍기위해 F키를 누르자
            if (Input.GetKeyDown(KeyCode.F))
            {
                //나를 지우고 아이템 매니져에게 힐팩카운트를 1 한다.
                ItemManager.instance.healPackCount += 1;
                Destroy(parent);
            }
        }

    }
}
