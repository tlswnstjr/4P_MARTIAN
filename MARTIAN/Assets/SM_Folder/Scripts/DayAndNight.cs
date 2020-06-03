using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond;

    private bool isNight = false;

    [SerializeField] private float fogDensityCalc;

    [SerializeField] private float nightFogDensity;
    private float dayFogDensity;
    private float currentFogDensity;

    Test_PlayerMovement tpm;

    // Start is called before the first frame update
    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
        tpm = GameObject.Find("Player_Body").GetComponent<Test_PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);
        if (transform.eulerAngles.x >= 170)
        {
            isNight = true;
            tpm.switchOfTheLight = true;
            tpm.TurnONOFF();
        }
        else if (transform.eulerAngles.x >= -10)
        {
            isNight = false;
            tpm.switchOfTheLight = false;
            tpm.TurnONOFF();
        }

        if (isNight)
        {
            if(currentFogDensity <= nightFogDensity)
            {
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
        else
        {
            if (currentFogDensity >= dayFogDensity)
            {
                currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }
}
