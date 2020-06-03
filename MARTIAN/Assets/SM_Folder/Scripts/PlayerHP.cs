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
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_StartingHealth;
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(50f);
        }
        SetHealthUI();
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
