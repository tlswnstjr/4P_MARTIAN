using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    public bool m_UseRelativeRotation = true;

    private Quaternion m_RelativeRotation;

    // Start is called before the first frame update
    void Start()
    {
        //부모의 로컬 로테이션을 받아와서
        m_RelativeRotation = transform.parent.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_UseRelativeRotation)
        {
            //매 프레임 부모랑 똑같게 설정해주겠다.
            transform.rotation = m_RelativeRotation;
        }
    }
}
