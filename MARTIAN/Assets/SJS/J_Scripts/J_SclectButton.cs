using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class J_SclectButton : MonoBehaviour
{
    //이 스크립트는 몇개를 버릴지 알려주는 스크립트입니다
    //몇개를 넣을지 몇개를 꺼낼지 
    public GameObject scls;
    public Text text1;

    //public static J_SclectButton sclButton;
    private void Awake()
    {
       // sclButton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ss = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    string a;
   public int ss;
    public void OnLeftB()
    {
        //만약에 지금 텍스트가 0이면 그 아래로 내려 갈 수업삳
        if(int.Parse(text1.text) == 0)
        {
            return;
        }
        a = text1.text;
        ss = int.Parse(a) - 1;
        text1.text = ss.ToString();
    }

    public void OnRightB()
    {
        if (int.Parse(text1.text) == int.Parse(scls.GetComponent<J_Slots>().text.text))
        {
            return;
        }
        a = text1.text;
        ss = int.Parse(a) +1;
        text1.text = ss.ToString();
    }
}
