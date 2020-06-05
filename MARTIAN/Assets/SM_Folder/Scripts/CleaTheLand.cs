using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaTheLand : MonoBehaviour
{
    public enum WhereIAm
    {
        OUTSIDE,
        INSIDE
    }
    public WhereIAm state;

    public GameObject rayStart;
    public float rayDis = 2f;
    public float angle = -2.5f;
    RaycastHit rayHit;

    public bool m_AmIOutSide = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Switch();
    }

    public void Switch()
    {
        switch (state)
        {
            case WhereIAm.OUTSIDE:
               
                break;
            case WhereIAm.INSIDE:
                
                break;
        }
    }

    //외부일때 사용할 함수
    private void OutSide_RayFiring()
    {
        Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
        Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.red);

        if (Physics.Raycast(ray, out rayHit, rayDis, (1 << 12)))
        {
            MeshRenderer mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
            if (mr.enabled == true) mr.enabled = false;
        }
    }

    //내부일때 사용할 함수
    private void InSide_Rayfiring()
    {
        Ray ray = new Ray(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle);
        Debug.DrawRay(rayStart.transform.position, rayStart.transform.forward * rayDis + rayStart.transform.up * angle, Color.red);

        if (Physics.Raycast(ray, out rayHit,rayDis,(1<<12)))
        {
            MeshRenderer mr = rayHit.transform.gameObject.GetComponent<MeshRenderer>();
            if (mr.enabled == false) mr.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            if (m_AmIOutSide == false)
            {
                m_AmIOutSide = true;
                state = WhereIAm.OUTSIDE;
            }
            else
            {
                m_AmIOutSide = false;
                state = WhereIAm.INSIDE;
            }
        }
    }
}
