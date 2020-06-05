using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond;

    #region ---===== 검은안개 =====-----
    //private bool isNight = false;

    //[SerializeField] private float fogDensityCalc;

    //[SerializeField] private float nightFogDensity;
    //private float dayFogDensity;
    //private float currentFogDensity;
    #endregion

    public static DayAndNight sun;

    public float myRotX;

    private void Awake()
    {
        if (sun == null) sun = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //이것도 검은안개
        //dayFogDensity = RenderSettings.fogDensity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);
        myRotX = transform.eulerAngles.x;

        #region ---======검은안개======----
        ////안개를 씌우고 걷히게 하는 부분
        //if (isNight)
        //{
        //    if(currentFogDensity <= nightFogDensity)
        //    {
        //        currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
        //        RenderSettings.fogDensity = currentFogDensity;
        //    }
        //}
        //else
        //{
        //    if (currentFogDensity >= dayFogDensity)
        //    {
        //        currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
        //        RenderSettings.fogDensity = currentFogDensity;
        //    }
        //}
        #endregion
    }
}
