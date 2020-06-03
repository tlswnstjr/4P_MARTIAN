using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenLevle : MonoBehaviour
{
    PlayerHP ph;

    public float m_StartOxygen = 100f;
    public Image m_FillImage;
    public Slider m_Slider;
    public Color m_FullOxygenColor = new Color();
    public Color m_ZeroOxygenColor = new Color();

    public float m_CurrentOxygen;
    private bool m_Depletion;

    private void OnEnable()
    {
        m_CurrentOxygen = m_StartOxygen;
        m_Depletion = false;

        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();

        SetOxygenUI();
    }

    private void FixedUpdate()
    {
        m_CurrentOxygen -= Time.deltaTime / 10f;
        SetOxygenUI();
        //지속적으로 HP를 달게하는 코루틴을 멈추게할 임시 조건문
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_Depletion = false;
            coroutineIsTrue = false;
        }
    }

    private void SetOxygenUI()
    {
        m_FillImage.color = Color.Lerp(m_ZeroOxygenColor, m_FullOxygenColor, m_CurrentOxygen / m_StartOxygen);
        m_FillImage.fillAmount = m_CurrentOxygen / m_StartOxygen;
        if (m_CurrentOxygen <= 0f && !m_Depletion)
        {
            StartCoroutine(OnChoke());
        }
    }

    //플레이어의 HP를 몇초에 한번씩 줄이고 싶다.
    //몇초?
    public float chokeTime;
    //코루틴의 반복을 관리할 불값
    public bool coroutineIsTrue = true;
    IEnumerator OnChoke()
    {
        m_Depletion = true;
        while (coroutineIsTrue)
        {
            yield return new WaitForSeconds(chokeTime);
            ph.TakeDamage(1f);
            m_CurrentOxygen = 0f;
        }
    }
}
