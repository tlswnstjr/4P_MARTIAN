using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenLevle : MonoBehaviour
{
    PlayerHP ph;

    #region ---====== 산소UI=====----
    public float m_StartOxygen = 100f;
    public Image m_FillImage;
    public Slider m_Slider;
    public Color m_FullOxygenColor = new Color();
    public Color m_ZeroOxygenColor = new Color();

    public float m_CurrentOxygen;
    private bool m_Depletion;
    #endregion

    //=======================================

    #region ----======체온UI======-----
    public float b_StartTemperature = 100f;
    public Image b_FillImage;
    public Slider b_Slider;

    public float b_CurrentTemperature;
    //줄어들 속도를 결정할 변수
    public float b_TimeLap = 30f;
    #endregion

    private void Start()
    {
    }

    private void OnEnable()
    {
        m_CurrentOxygen = m_StartOxygen;
        m_Depletion = false;

        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();

        Invoke("SetOxygenUI", 1);
    }

    private void FixedUpdate()
    {
        //산소UI와 체온UI는 외부씬이 아니라면 꺼놓는다.
        if (SceneManager.GetActiveScene().name == "Test_INSIDE")
        {
            m_Slider.gameObject.SetActive(false);
            b_Slider.gameObject.SetActive(false);
        }
        //산소가 줄어든다
        m_CurrentOxygen -= Time.deltaTime / 230f;
        //UI를 표시하겠다(체온,산소 둘다)
        SetOxygenUI();
        //지속적으로 HP를 달게하는 코루틴을 멈추게할 임시 조건문
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_Depletion = false;
            coroutineIsTrue = false;
        }
    }

    public float b_Hypothermia = 0.3f;
    private void SetOxygenUI()
    {
        m_FillImage.color = Color.Lerp(m_ZeroOxygenColor, m_FullOxygenColor, m_CurrentOxygen / m_StartOxygen);
        m_FillImage.fillAmount = m_CurrentOxygen / m_StartOxygen;
        if (m_CurrentOxygen <= 0f && !m_Depletion)
        {
            StartCoroutine(OnChoke(chokeTime));
        }

        //밤이되면
        if (DayAndNight.sun.myRotX >= 170)
        {
            //체온 UI를 표시하고
            b_Slider.gameObject.SetActive(true);
            //체온을 떨어뜨리자.
            //체온이 떨어진다
            b_CurrentTemperature -= Time.deltaTime / b_TimeLap;
            //떨어지는 체온을 UI에 표시하자
            b_FillImage.fillAmount = b_CurrentTemperature / b_StartTemperature;
            //체온이 0이되고 아직 안죽고 살아있다면
            if (b_CurrentTemperature <= 0f && !m_Depletion)
            {
                m_Depletion = true;
                StartCoroutine(OnChoke(b_Hypothermia));
            }
        }
        //낮이되면
        else if (DayAndNight.sun.myRotX >= -10)
        {
            //체온UI도 끄고
            //체온을 다시 만땅으로 채우고
            b_CurrentTemperature = b_StartTemperature;
        }
    }

    //플레이어의 HP를 몇초에 한번씩 줄이고 싶다.
    //몇초?
    public float chokeTime;
    //코루틴의 반복을 관리할 불값
    public bool coroutineIsTrue = true;
    IEnumerator OnChoke(float t)
    {
        m_Depletion = true;
        while (coroutineIsTrue)
        {
            yield return new WaitForSeconds(t);
            ph.TakeDamage(10f);
            //m_CurrentOxygen = 0f;
        }
    }
}
