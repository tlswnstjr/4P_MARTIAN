using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class J_Mune : MonoBehaviour
{
    public static J_Mune mune;
    //이 스크립트에서는 현제 무슨 버튼이 선택 되어 있는지 확인해주는 스크립트입니다 
    //그래야 같은 버튼을 연속적으로 클릭해도 사용을 못합니다
    //결국 현제 클릭 되어있는 친구와 다음에 클릭 되어질 친구이 두개를 비교해서 온/오프를 돌려가면서 
    //해주ㅕ야합니다 
    public GameObject buttonNmb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mune = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
