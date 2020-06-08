using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilCondition : MonoBehaviour
{
    //내 상태가 물에 젖은 상태라면 시간을 세고싶다.
    //상태부터 만들어야겠네
    public enum AmIWet
    {
        NO,
        Yes
    }
    public AmIWet state;

    float currentTime;
    //실제시간 20분을 초로 환산하면..?
    public float maxWetTime = 1200f;


    CropsGrowUP cgu;

    //코루틴을 한번만 호출하도록 해줄 불변수
    bool isGrowing = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SWITCH();
    }

    private void SWITCH()
    {
        switch (state)
        {
            case AmIWet.NO:
                break;
            case AmIWet.Yes:
                if (transform.childCount > 0)
                {
                    cgu = GetComponentInChildren<CropsGrowUP>();
                }
                //시간을 세고싶다
                currentTime += Time.deltaTime;
                //얼마까지셀거냐
                if (currentTime <= maxWetTime && transform.childCount > 0 && isGrowing == true)
                {
                    cgu.Growup();
                    isGrowing = false;
                }
                break;
        }
    }
}
