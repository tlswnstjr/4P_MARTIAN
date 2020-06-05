using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FilImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public float m_CurrentHealth;

    #region ----------========= 배고픔 관련 변수=========----------
    public float h_StartHunger = 100f;
    public Image h_FillImage;
    public Slider h_Slider;
    public Color h_FullHungerColor = new Color();
    public Color h_ZeroHungerColor = new Color();

    public float h_CurrentHunger;
    //배고픔 게이지가 줄어드는 속도를 조절할 변수
    public float h_CaloriesBurned;

    #endregion

    //=======================

    #region -----====== 목마름 관련 변수 =======--------
    public float t_StartThirsty = 100f;
    public Image t_FillImage;
    public Slider t_Slider;
    public Color t_FullThirstyColor = new Color();
    public Color t_ZeroThirstyColor = new Color();

    public float t_CurrentThirsty;
    //목마름 게이지가 줄어들 속도를 조절할 변수
    public float t_ConsumptionMoisture;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_StartingHealth;
    }

    private void OnEnable()
    {
        h_CurrentHunger = h_StartHunger;
        t_CurrentThirsty = t_StartThirsty;
    }

    // Update is called once per frame
    void Update()
    {
        //H버튼을 누르면 힐을 하고 싶다.
            //내 HP가 최대치보다 작을때만
            //아이템매니져의 힐팩카운트가 0보다 클때만
        if (Input.GetKeyDown(KeyCode.H) && m_CurrentHealth < m_StartingHealth && ItemManager.instance.healPackCount > 0)
        {
                m_CurrentHealth += 50f;
        }

        //데미지를 주는 임시 조건문
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    TakeDamage(50f);
        //}
        SetHealthUI();
    }

    private void FixedUpdate()
    {
        //배고픔 게이지가 줄어들고 싶다
        //일정 속도로
        h_CurrentHunger -= Time.deltaTime / h_CaloriesBurned;
        //목마름 게이지가 줄어들고 싶다 일정한 속도로
        t_CurrentThirsty -= Time.deltaTime / t_ConsumptionMoisture;

        SetUI();
    }

    private void SetUI()
    {
        //배고픔 지수가 닳는것을 표기하는 부분
        h_FillImage.color = Color.Lerp(h_ZeroHungerColor, h_FullHungerColor, h_CurrentHunger / h_StartHunger);
        h_FillImage.fillAmount = h_CurrentHunger / h_StartHunger;
        //만약 배고픔 지수가 0에 도달하면 디버프를 주고싶다.
        if (h_CurrentHunger <= 0f) ItemManager.instance.h_Drained = true;

        //목마름 지수가 닳는것을 표기하는 부분
        t_FillImage.color = Color.Lerp(t_ZeroThirstyColor, t_FullThirstyColor, t_CurrentThirsty / t_StartThirsty);
        t_FillImage.fillAmount = t_CurrentThirsty / t_StartThirsty;
        //만약 목마름 지수가 0에 도달하면 디버프를 주고싶다.
        if (t_CurrentThirsty <= 0f) ItemManager.instance.t_Dehydration = true;
    }

    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        m_CurrentHealth -= amount;

        SetHealthUI();

        //if (m_CurrentHealth <= 0f && !m_Dead)
        //{
        //    OnDeath();
        //}
    }

    private void SetHealthUI()
    {
        //m_Slider.value = m_CurrentHealth;
        m_FilImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
        m_FilImage.fillAmount = m_CurrentHealth / m_StartingHealth;
    }
}
